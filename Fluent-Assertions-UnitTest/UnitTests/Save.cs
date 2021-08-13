using Fluent_Assertions_Library.DTOs;
using Fluent_Assertions_Library.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fluent_Assertions_UnitTest.UnitTests
{
    public class Save
    {
        [Fact]
        public void Saving_DTO_Working_Check()
        {           
            var mockrepository = new Mock<IUserRepository<User>>();
            mockrepository.Setup(a => a.Save(new User { Emailadress = "test@gmail.com", Id = 1, LastName = "LastName", Name = "Name" })).Verifiable();
        }

        [Fact]

        public void Amount_Of_DTOs_Input_Equals_Output_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            mockrepository.Setup(a => a.GetAll()).Returns(new List<Fluent_Assertions_Library.DTOs.User> {
            new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "testa@hotmail.com", LastName = "lastname", Name = "Name" },
            new Fluent_Assertions_Library.DTOs.User { Id = 2, Emailadress = "testc@hotmail.com", LastName = "lastname", Name = "Name" },
            new Fluent_Assertions_Library.DTOs.User { Id = 3, Emailadress = "testb@hotmail.com", LastName = "lastname", Name = "Name" },
            new Fluent_Assertions_Library.DTOs.User { Id = 4, Emailadress = "testx@hotmail.com", LastName = "lastname", Name = "Name" }
            });

            mockrepository.Object.GetAll().Should().HaveCount(4);
        }

        [Theory]
        [InlineData(0, "testa@hotmail.com")]
        [InlineData(1, "testb@hotmail.com")]
        [InlineData(2, "testc@hotmail.com")]
        [InlineData(3, "testx@hotmail.com")]
        public void Does_Sorting_Email_Alphabetically_Work(int index, string email)
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            mockrepository.Setup(a => a.GetAll()).Returns(new List<Fluent_Assertions_Library.DTOs.User> {
            new Fluent_Assertions_Library.DTOs.User { Id = 1, Emailadress = "testa@hotmail.com", LastName = "lastname", Name = "Name" },
            new Fluent_Assertions_Library.DTOs.User { Id = 2, Emailadress = "testc@hotmail.com", LastName = "lastname", Name = "Name" },
            new Fluent_Assertions_Library.DTOs.User { Id = 3, Emailadress = "testb@hotmail.com", LastName = "lastname", Name = "Name" },
            new Fluent_Assertions_Library.DTOs.User { Id = 4, Emailadress = "testx@hotmail.com", LastName = "lastname", Name = "Name" }
            }.OrderBy(a => a.Emailadress).ToList());
            mockrepository.Object.GetAll()[index].Emailadress.Should().Be(email);

        }

        [Fact]

        public void Save_And_Return_All_Method_Check()
        {
            var mockrepository = new Mock<IUserRepository<User>>();
            Action<User> SaveInputACT = (User user) => mockrepository.Object.Save(new User { });
            Func<List<User>> GetAllFUNC = () => mockrepository.Object.GetAll();
            var ex = Record.Exception(() => mockrepository.Object.SaveAndReturnAll(ref SaveInputACT, ref GetAllFUNC, (new User { Id = 1, Emailadress = "test@gmail.com", LastName = "Lastname", Name = "Name" })));
            ex.Should().BeNull();
        }
    }
}
