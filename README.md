Introduction 
This mini project is built for asp.net core CRUD practice, from scratch to testing api in API endpoint.

Get started

1. clone this project
$ git clone https://github.com/kucmoving/StudentParent-WebApi

2. Go into the repository (there is a dash in repository name but you can simply using tab to finish it, like $cd St"tab")
$ cd StudentParent-WebApi 

3. Remove current origin repository
$ git remote remove origin

4. Create a local database

5. Add a connection string to appsetting.json (please change your own location code)
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=LAPTOP-NCT36AOV\\SQLEXPRESS;Initial Catalog=ParentStudentWebAPI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  },
  
6. data seeding 
$ dotnet run seeddata
$ cd StudentParent WebApI

7. turn on the backend server
$ cd StudentParent WebApI
$ dotnet watch run

8. You should now turn on the backend server successfully. You can test api base on the data in database.

![未命名](https://user-images.githubusercontent.com/92262463/179341772-6f2077d0-11da-4adc-919c-bd0550a77f51.jpg)


