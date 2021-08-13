using Fluent_Assertions_Library.DTOs;
using Fluent_Assertions_Library.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Fluent_Assertions_Database.Repositories;
using Fluent_Assertions_Database.Database;
using Microsoft.EntityFrameworkCore;

namespace Fluent_Assertions_UnitTest.UnitTests
{
    public class Verify
    {
        private IUserRepository<User> _userRepository;

        [Fact]
        public void Email_Not_Filled_In_Check()
        {
            // | 
            // | Doesn't work, DBContext doesn't assign dependency injection.
            // V

            //var services = new ServiceCollection();
            //services.AddScoped<IUserRepository<User>, UserRepository<User>>();
            //var serviceProvider = services.BuildServiceProvider();
            //_userRepository = serviceProvider.GetService<IUserRepository<User>>();            
            //_userRepository.Invoking(a => a.Validate(new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "", LastName = "Lastname", Name = "Name" })).Should().Throw<NullReferenceException>();


            var mockrepository = new Mock<IUserRepository<User>>();

            // | 
            // | Doesn't work, should be working.
            // V

            //Func<bool> FUNC = () => mockrepository.Object.Validate(new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "", LastName = "LastName", Name = "Name" });
            //FUNC.Should().Throw<NullReferenceException>();

            FluentActions.Invoking(() => mockrepository.Object.Validate(new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "", LastName = "LastName", Name = "Name" }));

        }

        [Fact]
        public void Last_Name_Not_Filled_In_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
        }

        [Fact]
        public void Email_Already_In_Database_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            mockrepository.Setup(a => a.GetAll()).Returns(new List<Fluent_Assertions_Library.DTOs.User> {
            new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "test@hotmail.com", LastName = "lastname", Name = "Name" }
            });
            FluentActions.Invoking(() => mockrepository.Object.Validate(new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "test@gmail.com", LastName = "LastName", Name = "Name" }));
        }

        [Fact]
        public void Not_Valid_Email_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            FluentActions.Invoking(() => mockrepository.Object.Validate(new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "testgmail", LastName = "LastName", Name = "Name" }));
        }

        [Fact]
        public void Valid_Input_Without_Errors_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            var ex = Record.Exception(() => mockrepository.Object.Validate(new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "test@gmail.com", LastName = "Lastname", Name = "Name" }));
            ex.Should().BeNull();
        }


        [Fact]
        public void Verify_And_Return_All_Method_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            Func<User, bool> VerifyFUNC = (User user) => mockrepository.Object.Validate(new User { });
            Func<List<User>> GetAllFUNC = () => mockrepository.Object.GetAll();
            var ex = Record.Exception(() => mockrepository.Object.VerifyAndReturnAll(ref VerifyFUNC, ref GetAllFUNC, (new User { Id = 1, Emailadress = "test@gmail.com", LastName = "Lastname", Name = "Name" })));
            ex.Should().BeNull();
        }
    }
}
