# Social Network Application

## Backend

Backend developed with ASP.NET Core 6.0

**User API with 3 EndPoints**
- In `post: users/register` for registering a new User
- In `post: users/login` for authenticating and generating JWT token
- In `get: users/search` for searching users with part of a name

**Friendship API EndPoints**
- In `post : friendships` for adding a request to friend
- In `put: friendships` for confirming or rejecting a request by a user
- In `get: friendships/requests` for updating one item
- In `get: friendships` for retrieving a request
- In `get: friendships/followers` for retrieving list of followers
- In `get: friendships/followings` for retrieving list of followings

for launching backend
execute command prompt
set current path to C:\Code\SocialNetwork\src\Service\Infrastructure\SocialNetwork.Infrastructure.Persistance.Migrations
run: dotnet ef database update
after database creating
run: dotnet run

Swagger docimentation 

![alt text](http://url/to/img.png)

## Frontend

Frontend developed with Angular 13 and Material Components

```