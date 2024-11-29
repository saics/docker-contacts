# Docker Contacts Application

  

This application consists of:

  

-  **Frontend**: Vue.js application

-  **API**: ASP.NET Core API

-  **Database**: SQL Server

## Introduction to Docker


Docker is a platform that allows you to run applications inside isolated environments called **containers**. Containers include everything needed to run the application, such as code, system libraries, and settings.


By using Docker, we can ensure that the application will work the same on any system without additional configurations.
  
## Steps to Set Up the Application

  

### 1. Install Docker

  

[Docker Desktop - Download](https://www.docker.com/products/docker-desktop)

  

### 2. Clone the Repository

- If you have Git installed
```bash
git clone https://github.com/saics/docker-contacts
```
- If you don't have Git
	- Visit the [GitHub repository](https://github.com/saics/docker-contacts)
	- Click **Code** and then **Download ZIP**
	- Extract the ZIP file to the desired location
### 3. Navigate to Project Directory
Open terminal or cmd and navigate to the project directory:
```bash
cd docker-contacts
```
### 4. Build and Run Docker Containers
Command to build images and start the containers:
```bash
docker-compose up --build
```
### 5. Verify Running Containers
Command to check if all containers are running:
```bash
docker ps
```
You should see **3** running containers:
	- **docker-contacts-frontend**
	- **docker-contacts-api**
	- **docker-contacts-db**
### 6. Access the Application
-  **Frontend**: Open [http://localhost](http://localhost) in your web browser
- **API**: Open [http://localhost:8080/swagger](http://localhost:8080/swagger) to access the Swagger API documentation