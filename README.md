# Fetch Backend Challege

This repo serves as my submission for the fetch backend challenge. This project is a web api which provides a implmentation for the receipt processor. 

## Important Notes

You can run this project without docker, however it would be preferred that you use docker desktop and postman (or similar interface) for testing. This is most effective and failsafe way.
Please also note that the application is not enabled to use https. TLS and SSL errors required me to add certificates so I opted to configure the solution to only use http. 

## Getting Started

### Running the project without docker

* You need have Visual Studio Installed. (Note this should be at least version 17.6 or higher)
* You also need the .Net6 SDK. This project uses .Net6.
* Clone the project.
* Once you start the project, you can test the API using the swagger endpoint : https://localhost:8080/swagger
* You can also test it using Postman with the following endpoints: 
      -http://localhost:8080/receipts (POST)
      -http://localhost:8080/receipts/{id}/points (GET)

### Running the project using docker (preferred)

* Clone the repo
* Start docker desktop
* Navigate to the folder that has the Dokerfile. (This is your local folder containing the project)
* The API is set to listen to port 8080. If you wish to change this, you have to change the 'EXPOSE' port in the docker file, and the Urls.Add() port in program.cs.
* If the ports are free in your system, you can leave 8080 as the port.
* Now you can build the image, and run the image using the following commands :
```
 docker build -t fecthapiv1 . 
```
```
 docker run -p 8080:8080 fecthapiv1
```
* There should be a container running this api on your docker desktop
* Note that the api is running in a release profile, so the swagger UI is not available for testing this deployment.
* You can test it using Postman with the following endpoints: 
      -http://localhost:8080/receipts (POST)
      -http://localhost:8080/receipts/{id}/points (GET)

