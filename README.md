# 4roomforum Website

## Overview

**4roomforum** a forum website built with ASP.NET, designed to provide users a platform to share and discuss various topics.

## User Scope

### Regular Users:
- Create an account or log in via Google.
- After logging in, users can:
  - Create posts.
  - View post details.
  - Comment on posts.
  - Like posts by other users.

### Admin:
- Delete posts.
- Create and manage categories and threads.
- Write their own posts.
- View statistics on the most-liked posts each month.

## Technologies Used

The project is developed with ASP.NET 8.0 and includes:
- **ASP.NET Core**: For building the web application and APIs.
- **Entity Framework Core**: For database access and ORM (Object-Relational Mapping).
- **SignalR**: For real-time socket communication between server and clients.
- **AJAX Pagination**: For seamless data loading without refreshing the page.
- **Microservices Architecture**: For scalability and modular design.
- **Generic Repository Pattern**: To standardize data access logic.
- **LINQ**: For efficient and readable data queries.
- **Swagger/OpenAPI**: For API documentation and testing.
- **MySQL**: As the primary database.
- **Docker**: For containerizing services and simplifying deployment.

## Project Structure

The project follows a **microservices** architecture, consisting of three main services:
- **CatThreadService**: 
  - Manages categories, hot categories, and threads. 
  - Each category contains multiple threads, and each thread can have multiple posts.
- **PostService**:
  - Handles post management, including likes and comments for each post.
- **UserService**:
  - Manages user accounts, roles, security, registration, and authentication.

## Installing

*  Install the sql file  ```forum_cat_thread.sql```, ```forum_post_reply.sql```, ```forum_user_role.sql```
*  Use ```cd <your_project_directory>/MicroServices/PostService``` and type ```dotnet run``` to run the **PostService**
*  Use ```cd <your_project_directory>/MicroServices/UserServices``` and type ```dotnet run``` to run the **UserService**
*  Use ```cd <your_project_directory>/MicroServices/CatThreadService``` and type ```dotnet run``` to run the **CatThreadService**
*  Use ```cd <your_project_directory>/4roomforum``` and type ```dotnet run``` to run the main project
*  Run 4 services in 4 terminal at the same time to access the website
*  To access the website, please visit the following link:

```
http://localhost:5287
```

Replace <your_project_directory> with the path to your project directory, for example: D:\Hoc\dot_net\Project

## Collaborators
Below are the GitHub profiles of the project team members:

- [Dang Tuan Kiet](https://github.com/KietDang1608)
- [Nguyen Viet Khoi](https://github.com/VieetKhooii)
- [Huynh Duc Huy](https://github.com/baypro089)
- [Huynh Tuan Dat](https://github.com/axy888)
- [Nguyen Minh Huu](https://github.com/kuzo19032003)
- [Do Phuoc Hung](https://github.com/dophuochung2428)
