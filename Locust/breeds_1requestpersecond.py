import time
from locust import HttpUser, task, constant 

class User(HttpUser):
    wait_time = constant(1)

    @task
    def get_breeds_by_id(self):
        self.client.get("/breeds/9C468727-01AC-462D-9392-404CAEC2282E?useIndex=false&useCache=false&devNull=true")

    @task
    def get_breeds_by_popularity(self):
        self.client.get("/breeds?popularity=125&useIndex=false&useCache=false&devNull=true")

    @task
    def get_breeds_by_group_and_dog(self):
        self.client.get("/breeds?group=Working&dog=Baxter&useIndex=false&useCache=false&devNull=true")

    @task
    def get_breeds_by_groupId(self):
        self.client.get("/breeds?groupId=C6860975-68A0-46D3-8B32-F297BA14B070&useIndex=false&useCache=false&devNull=true")