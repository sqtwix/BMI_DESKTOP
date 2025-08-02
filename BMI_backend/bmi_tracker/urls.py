from django.urls import path
from .views import BMIRecordListCreateView

urlpatterns = [
    path('bmi/',BMIRecordListCreateView.as_view(),name='bmi'),
]