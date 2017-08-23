using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WhichCard.Entities;
using WhichCard.Repositories;
using Xunit;

namespace WhichCard.IntegrationTests.Repositories
{
    public class RepositoryTests
    {
        [Fact]
        public async Task Users_CRUD()
        {
            //Arrange
            var user = new User
            {
                Id = "userId",
                Email = "myemail@email.com",
                FirstName = "First",
                LastName = "Last",
                CreditCards = new List<CreditCard> 
                { 
                    new CreditCard { Name = "Venture"},
                    new CreditCard { Name = "Fargo"} 
                }
            };

            var repo = new UserRepository();

            //Create
            await repo.InsertAsync(user);

            //Get
            var fetchedUser = await repo.GetAsync(user.Id);
            fetchedUser.ShouldBeEquivalentTo(user, opts => opts.Excluding(u => u.CreationDate));

            //Delete
            await repo.DeleteAsync(user);

            var getDeleted = await repo.GetAsync(user.Id);
            getDeleted.Should().BeNull();
        }
    }
}
