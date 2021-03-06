"""
Definition of views.
"""

from datetime import datetime
from django.shortcuts import render
from django.http import HttpRequest
from django.http import HttpResponse
from app.ctof_if import *

def home(request):
    """Renders the home page."""
    assert isinstance(request, HttpRequest)
    return render(
        request,
        'app/index.html',
        {
            'title':'Home Page',
            'year':datetime.now().year,
        }
    )

def contact(request):
    """Renders the contact page."""
    assert isinstance(request, HttpRequest)
    return render(
        request,
        'app/contact.html',
        {
            'title':'Contact',
            'message':'Your contact page.',
            'year':datetime.now().year,
        }
    )

def about(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)
    return render(
        request,
        'app/about.html',
        {
            'title':'About',
            'message':'Your application description page.',
            'year':datetime.now().year,
        }
    )

def foofoo(request):   
    values = request.GET["typeReqest"].split("&")

    for dict in values:
        key = dict.split("=")[0]
        value = dict.split("=")[1]
        if(key=="typeReqest"):
            typeReqest = value
        elif(key=="caretPos"):
            caretPos = value

    return HttpResponse(ctof_if_functions[typeReqest](caretPos), content_type='text/html')


