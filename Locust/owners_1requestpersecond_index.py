import time
from locust import HttpUser, task, constant 

class User(HttpUser):
    wait_time = constant(1)

    @task
    def get_owners_by_id(self):
        self.client.get("/owners/8F5983DE-48B8-4557-873D-0002AC1A0131?useIndex=true&useCache=false&devNull=true")

    @task
    def get_owners_by_name(self):
        self.client.get("/owners?firstName=Yoselin&lastName=Gilmore&useIndex=true&useCache=false&devNull=true")

    @task
    def get_owners_by_country(self):
        self.client.get("/owners?countryCode=CA&useIndex=true&useCache=false&devNull=true")

    @task
    def get_owners_by_dogId(self):
        self.client.get("/owners?dogId=8ABF7EEB-EFFC-473F-912A-1293BDEC74A4&useIndex=true&useCache=false&devNull=true")

    @task
    def get_owners_by_dog_breed_and_name(self):
        self.client.get("/owners?breed=Greyhound&dog=Sandy&useIndex=true&useCache=false&devNull=true")