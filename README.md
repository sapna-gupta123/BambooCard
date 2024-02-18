# Bamboo Card API Project

## Overview 
This README document provides an overview of the project's design, implementation details, development tools used, and instructions on running the application.

## Design and Implementation
The project utilizes in-memory caching to store the collection of best stories, minimizing the need for multiple client API calls. This approach significantly improves the API's performance by reducing latency, as subsequent requests fetch data from the cache rather than making additional calls to the client API.

Additionally, parallel programming techniques have been implemented to further enhance the API's latency while loading all story details from the client API. By leveraging parallelism, the application can efficiently retrieve and process data in parallel, leading to improved overall performance.

## Note
To ensure scalability and accommodate potential future expansion, it is recommended to consider using a centralized cache solution such as Redis. By adopting Redis as the caching mechanism, the application can easily scale to handle increased loads and accommodate multiple services within the ecosystem.

Furthermore, implementing a Pub/Sub mechanism with Redis allows for seamless cache updates when any changes occur in the collection of best stories. This ensures that both the in-memory cache and any distributed caches across multiple instances of the application remain synchronized with the latest data.

## Development Tools Used
- Visual Studio 2022: Integrated development environment used for coding, debugging, and building the application.

## Tech Stack
- .NET Core (7.0 .NET Framework): Framework used for developing the API, providing a versatile and cross-platform development environment.
- C#: Primary programming language used for implementing the application logic.
- In-Memory Cache: Used for storing and retrieving the collection of best stories, improving performance by reducing latency.

## How to Run the Application
To run the Bamboo Card API application, follow these steps:
- Clone the project repository to your local machine.
- Open the project in Visual Studio 2022.
- Build the solution to ensure all dependencies are resolved.
- Run the application within Visual Studio.
- Once the application is up and running, access the Swagger page to interact with the API endpoints and test its functionality.

![image](https://github.com/sapna-gupta123/BambooCard/assets/94603978/4314d4ce-a244-4f57-9df3-0bc65fc75f43)

![image](https://github.com/sapna-gupta123/BambooCard/assets/94603978/d9eb1c46-bfda-46ff-a6c8-2e32d431d418)

![image](https://github.com/sapna-gupta123/BambooCard/assets/94603978/1df694f4-4fbc-4737-81c7-2885e6388d12)


## Additional Notes
- Ensure that the necessary dependencies and packages are installed and configured before running the application.
- Monitor application performance and consider implementing additional optimizations or enhancements as needed.

## Contributors
Sapna Gupta


