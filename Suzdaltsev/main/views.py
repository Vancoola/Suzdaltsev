from django.shortcuts import render
from .models import MenuModel


# Create your views here.

def index(request):
    return render(request, 'main/index.html')

def detail(request, name):
    # print(MenuModel.objects.filter(name=name).first())
    return render(request, 'main/detail.html', {'main': MenuModel.objects.filter(name=name).first()})