# Account-Service-Application

## Overview

This project is a demonstration of my skills in C#, .NET, ASP.NET, NUnit as part of my application for Trainee/Junior .NET Developer. It includes a main application and a suite of unit tests.

## Controllers

1. **IncidentController:** is a key component of the application, responsible for handling incidents. It provides an interface for creating and managing incidents associated with accounts. The controller leverages Entity Framework for data access and manipulation, providing robust and efficient operations.
2. **AccountController:** is a crucial part of the application, responsible for handling accounts. It provides an interface for creating and managing accounts associated with incidents and contact info.
3. **ContactController:** is a controller that creates a contact info for different accounts.

## GenerateNewIncidentName Method

The **GenerateNewIncidentName** method is responsible for generating a unique name for each new incident. This is crucial for differentiating and identifying incidents in the system.

Here's how it works:

- The method first retrieves the last incident created in the system using the GetLastIncident query.
- If no incidents exist (i.e., this is the first incident being created), it returns a default incident name: "INC-0001".
- If incidents do exist, it extracts the numeric part of the last incident's name. This is done by splitting the name on the "-" character and taking the last part, which should be a four-digit number.
- It then increments this number by one to generate a new unique number for the next incident.
