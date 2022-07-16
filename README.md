<h2>Introduction</h2> 
This mini project is built for asp.net core CRUD practice, from scratch to testing api in API endpoint.

<h2>Lesson Learned</h2>

Here are some elements i have learnt after this project:<br>
* transfering data between SQL and Asp.net
* creating table and relationship, highly recommand using tool like<br>
  [Lucid](https://lucid.app/)<br>
  [Drawio](https://draw.io/)<br>
* adding migration, seeding data and connecting to database<br>
* building CRUD function in asp.net<br>
* basic concept of unit test


<h2>Get started</h2>

1. Clone this project <br>
```$ git clone https://github.com/kucmoving/StudentParent-WebApi```

2. Go into the repository (there is a dash in repository name but you can simply using tab to finish it, like $cd St"tab")<br>
```$ cd StudentParent-WebApi ```

3. Remove current origin repository<br>
```$ git remote remove origin```

4. Create a local database(Personally i am using MicrosoftSQL 2019)

5. Add a connection string to appsetting.json (Please change your own location code after DefaultConnection)<br>
```{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=LAPTOP-NCT36AOV\\SQLEXPRESS;Initial Catalog=ParentStudentWebAPI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  },
  ```

6. Add migration<br>
```dotnet ef migrations add InitialCreate```

7. Update database<br>
```dotnet ef database update```

8. Data seeding<br>
```$ dotnet run seeddata```<br>
```$ cd StudentParent WebApI```

9. Turn on the backend server<br>
```$ cd StudentParent WebApI```<br>
```$ dotnet watch run```

10. You should now turn on the backend server successfully. You can test api base on the data in database.

![未命名](https://user-images.githubusercontent.com/92262463/179341772-6f2077d0-11da-4adc-919c-bd0550a77f51.jpg)



