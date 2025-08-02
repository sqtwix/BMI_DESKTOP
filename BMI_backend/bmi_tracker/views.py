from django.shortcuts import render
from rest_framework import generics, permissions
from .models import BMIRecord
from .serializers import BMIRecordSerializer

class BMIRecordListCreateView(generics.ListCreateAPIView):
    serializer_class = BMIRecordSerializer
    permission_classes = [permissions.IsAuthenticated]

    def get_queryset(self):
        return BMIRecord.objects.filter(user=self.request.user)

    def perform_create(self, serializer):
        serializer.save(user=self.request.user)
