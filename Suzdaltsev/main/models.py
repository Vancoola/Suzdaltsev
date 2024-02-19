from django.db import models
from django.forms import ValidationError
from django.urls import reverse
from django.urls.exceptions import NoReverseMatch


# Create your models here.

class MenuModel(models.Model):
    name = models.CharField(max_length=255, verbose_name='Название')
    url = models.CharField(verbose_name='URL', max_length=255, null=True, blank=True)
    upper_menu = models.ForeignKey('MenuModel', on_delete=models.CASCADE, verbose_name='Родитель', null=True,
                                   blank=True, related_name='upper')
    named_url = models.BooleanField(verbose_name='URL NAMED', default=False)

    def clean(self):
        if self.named_url:
            if self.url is None:
                raise ValidationError('При URL NAMED, поле url не может быть пустым')
            try:
                reverse(self.url).url_name
            except NoReverseMatch:
                raise ValidationError(
                    'ВНИМАНИЕ! При URL NAMED, поле url должно соответствовать одному из name urlpatterns!')

    def get_upper(self):
        if self.upper_menu is not None:
            return self.upper_menu.name

    get_upper.short_description = 'Родитель'

    def __str__(self):
        return self.name

    class Meta:
        verbose_name = 'Меню'
        verbose_name_plural = 'Меню'
