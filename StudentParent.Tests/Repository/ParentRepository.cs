using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using StudentParent_WebApI.Data;
using StudentParent_WebApI.Models;
using StudentParent_WebApI.Repository;

namespace StudentParent.Tests.Repository
{
    public class ParentRepositoryTest
    {

        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                //using a inmemorydatabase 
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            // to SEED! 
            if (await databaseContext.Parents.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Parents.Add(
                        new Parent()
                        {
                            FirstName = "Amy",
                            LastName = "Takahashi",
                            Phone = "9574 2158",
                            SchoolClub = new SchoolClub()
                            {
                                Name = "Cooking Club"
                            }
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;

        }


        [Fact] //test = true!
        public async void ParentRepository_GetParent_ReturnsParent() //naming : class--> method --> return 
        {
            //Arrange ,setting input 
            var id  = 1;
            var dbContext = await GetDatabaseContext();
            var parentRepository = new ParentRepository(dbContext);

            //Act
            var result = parentRepository.GetParent(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Parent>();   
        }

        [Fact]
        public async void ParentRepository_ParentExists_ReturnsParent()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetDatabaseContext();
            var parentRepository = new ParentRepository(dbContext);

            //Act
            var result = parentRepository.GetParent(id);

            //Assert
            result.Should().NotBeNull();
            //can be much better
            //cannot work with some documentations with from Fluent Assertions
            // https://fluentassertions.com/nullabletypes/
        }
    }
}