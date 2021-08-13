using Fluent_Assertions_Library.DTOs;
using Fluent_Assertions_Library.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fluent_Assertions_UnitTest.UnitTests
{
    public class Verify
    {
        [Fact]
        public void Email_Not_Filled_In_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            mockrepository.Setup(a => a.Validate(new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "", LastName = "Lastname", Name = "Name" })).Throws<NullReferenceException>().ToString();

            // | 
            // | Doesn't work, should be working, check this out.
            // V

            //Assert.Throws<NullReferenceException>(() => mockrepository.Object.Validate(new Test_Driven_Development_Library.DTOs.User { Id = 1, Emailadress = "", LastName = "Lastname", Name = "" }));

        }

        [Fact]
        public void Last_Name_Not_Filled_In_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            mockrepository.Setup(a => a.Validate(new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "test@gmail.com", LastName = "", Name = "Name" })).Throws<NullReferenceException>().ToString();
        }

        [Fact]
        public void Email_Already_In_Database_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            mockrepository.Setup(a => a.GetAll()).Returns(new List<Fluent_Assertions_Library.DTOs.User> {
            new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "test@hotmail.com", LastName = "lastname", Name = "Name" }
            });
            mockrepository.Setup(a => a.Validate(new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "test@gmail.com", LastName = "Lastname", Name = "Name" })).Throws<ArgumentException>().ToString();
        }

        [Fact]
        public void Not_Valid_Email_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            mockrepository.Setup(a => a.Validate(new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "testgmail", LastName = "Lastname", Name = "Name" })).Throws<NullReferenceException>().ToString();
        }

        [Fact]
        public void Valid_Input_Without_Errors_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            var ex = Record.Exception(() => mockrepository.Object.Validate(new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "test@gmail.com", LastName = "Lastname", Name = "Name" }));
            Assert.Null(ex);
        }


        [Fact]
        public void Verify_And_Return_All_Method_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            Func<User, bool> VerifyFUNC = (User user) => mockrepository.Object.Validate(new User { });
            Func<List<User>> GetAllFUNC = () => mockrepository.Object.GetAll();
            var ex = Record.Exception(() => mockrepository.Object.VerifyAndReturnAll(ref VerifyFUNC, ref GetAllFUNC, (new User { Id = 1, Emailadress = "test@gmail.com", LastName = "Lastname", Name = "Name" })));
            Assert.Null(ex);
        }
    }
}
