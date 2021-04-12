import time
from locust import HttpUser, task, between

class User(HttpUser):
    @task
    def get_dogs(self):
        self.client.get("/dogs?name=Zeus&useIndex=true&useCache=true&breed=Greyhound")
