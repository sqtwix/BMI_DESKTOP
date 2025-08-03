from django.shortcuts import render
from rest_framework import generics, permissions
from rest_framework.response import Response
from .models import BMIRecord
from .serializers import BMIRecordSerializer

class BMIRecordListCreateView(generics.ListCreateAPIView):
    serializer_class = BMIRecordSerializer
    permission_classes = [permissions.IsAuthenticated]

    def perform_create(self, serializer):
        serializer.save(user=self.request.user)

class BMICreatList(generics.ListAPIView):
    serializer_class = BMIRecordSerializer
    permission_classes = [permissions.IsAuthenticated]

    def get(self, request):
        user_bmis = BMIRecord.objects.filter(user=request.user).order_by('-date')
        serializer = BMIRecordSerializer(user_bmis, many=True)
        return Response(serializer.data)
