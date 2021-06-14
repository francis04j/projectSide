# projectSide
Practice side project for backend development using node.js, javascript, typescript, cloud, microservices
 
Project is based on a Car Rental Platform that allows users to 
 - rent a car
 - view all cars

 
This project should conform to SOLID principle, Automated Testing, Continuous deployment and Cloud support.

The API that we will be creating can be programmed in any language that you desire (typescript, javascript, java, c#, GO, Python)
TO ensure efficiency and availability, we will also support CAP theorem

We will be introducing 
 - a relational database
 - a no-sql database
 - a caching layer
 - a message queue
 - a nofitication system
 - a frontend service, i.e. gateway
 - SAST (static testing involves stripping off non-functional code like node_modules,tests,docs,confgiration file and send them to veracode)
 - DAST (dynamic applicaton secuirty testing via BURP suite pro)

API endpoint: 
P/rent

Models
  -Car {
   type:
   speed
   year
   price charge
   availabilty
   last-rented
  }
  
  Customer{
   Age
   Credit card allowance
   ID
   Name
   Address
   ActiveCar
  }
  
  Location
  {
   PostCode
   Contact Number
   NUmber of cars available
  }
  

  
End-2-end system design

Tools: GCP, Kubernetes, Github action (CI/CD), Docker for service and database
