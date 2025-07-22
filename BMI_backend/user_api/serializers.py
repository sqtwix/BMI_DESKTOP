from rest_framework import serializers
from .models import CustomUser

class UserSerializer(serializers.ModelSerializer):
    class Meta:
        model = CustomUser
        fields = ('id', 'user_name', 'password')
        extra_kwargs = {'password' : {'write-only' : True}}

    def create(self, validated_data):
        user = CustomUser.objects.create_user(**validated_data)
        return user