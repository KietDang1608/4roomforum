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

## Collaborators
Below are the GitHub profiles of the project team members:

- [Dang Tuan Kiet](https://github.com/contributor1)
- [Nguyen Viet Khoi](https://github.com/contributor2)
- [Huynh Duc Huy](https://github.com/contributor3)
- [Huynh Tuan Dat](https://github.com/contributor1)
- [Nguyen Minh Huu](https://github.com/contributor2)
- [Do Phuoc Hung](https://github.com/contributor3)
