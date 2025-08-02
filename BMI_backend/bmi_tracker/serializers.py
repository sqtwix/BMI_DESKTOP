from rest_framework import serializers
from .models import BMIRecord

class BMIRecordSerializer(serializers.ModelSerializer):
    class Meta:
        model = BMIRecord
        fields = ['id', 'user', 'weight', 'height', 'bmi', 'date']
        read_only_fields = ['id', 'created_at', 'user']
