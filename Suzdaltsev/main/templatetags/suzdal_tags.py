from django import template
from django.shortcuts import get_object_or_404
from django.utils.safestring import mark_safe

from ..models import MenuModel
from ..urls import urlpatterns
from django.urls import reverse
register = template.Library()


@register.simple_tag()
def draw_menu(name=None):
    main = get_object_or_404(MenuModel, name=name)
    global page
    if main.named_url:
        for i in urlpatterns:
            if i.name == main.url:
                f'<ul><li><a href="{reverse(i.name)}">{main.name}</a></li><ul>'
    else:
        page = f'<ul><li><a href="{main.url}">{main.name}</a></li><ul>'

    def painter(obj, revers=False):
        global page
        if revers:
            page += '<ul>'
        if obj.upper is not None:
            for obj2 in obj.upper.all():
                if obj2.named_url:
                    for i in urlpatterns:
                        if i.name == obj2.url:
                            page += f'<li><a href="{reverse(i.name)}">{obj2.name}</a></li>'
                else:
                    page += f'<li><a href="{obj2.url}">{obj2.name}</a></li>'
                if obj2.upper is not None:
                    painter(obj2, True)
        if revers:
            page += '</ul>'

    painter(main)
    page += '</ul>'
    page += '</ul>'
    return mark_safe(page)
