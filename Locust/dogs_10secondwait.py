import time
from locust import HttpUser, task, constant 

class User(HttpUser):
    wait_time = constant(10)

    @task
    def get_dogs_by_id(self):
        self.client.get("/dogs/64F67DAB-5E32-4A7F-9655-0000210A4708?useIndex=false&useCache=false&devNull=true")

    @task
    def get_dogs_by_group_and_code(self):
        self.client.get("/dogs?group=Sporting&countryCode=CA&useIndex=false&useCache=false&devNull=true")

    @task
    def get_dogs_by_name_and_breed(self):
        self.client.get("/dogs?name=Tucker&breed=Labrador Retriever&useIndex=false&useCache=false&devNull=true")

    @task
    def get_dogs_by_owner_name(self):
        self.client.get("/dogs?ownerFirstName=Dax&ownerLastName=Christian&useIndex=false&useCache=false&devNull=true")
