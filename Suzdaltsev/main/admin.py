from django.contrib import admin
from .models import MenuModel


# Register your models here.

@admin.register(MenuModel)
class MenuModelAdmin(admin.ModelAdmin):
    list_display = ('name', 'get_upper')
    search_fields = ('name', 'url')
