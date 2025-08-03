from django.urls import path
from .views import BMIRecordListCreateView, BMICreatList

urlpatterns = [
    path('bmi/',BMIRecordListCreateView.as_view(),name='bmi'),
    path('bmi/getList/', BMICreatList.as_view(), name='user-bmi-list')
]