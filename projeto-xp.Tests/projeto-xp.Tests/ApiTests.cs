using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using projeto_xp.Api.Models;
using projeto_xp.Api.Controllers;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;

namespace projeto_xp.Tests
{
    public class ApiTests
    {
        private static List<UserItemCreate> GetTestUsers()
        {
            var userList = new List<UserItemCreate>();
            userList.Add(new UserItemCreate()
            {
                Id = "48a88c28-24c5-4be0-997b-7a9cf63406e9",
                Name = "John",
                Surname = "Doe",
                Age = 18
            });
            userList.Add(new UserItemCreate()
            {
                Id = "eacf3a7b-8eb7-4180-8bd7-f5f4cb152913",
                Name = "Jane",
                Surname = "Doe",
                Age = 19
            });
            userList.Add(new UserItemCreate()
            {
                Id = "665fef40-f576-4897-9502-e252cd7cbea3",
                Name = "Mack",
                Surname = "The Knife",
                Age = 94
            });
            return userList;
        }

        [Fact]
        public async Task GetUserItems()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.GetAllUserItems())
                .ReturnsAsync(GetTestUsers());
            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);
            /// Act
            var actionResult = await controller.GetUserItems();

            /// Assert
            repositoryMock.Verify(r => r.GetAllUserItems());

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var userItems = Assert.IsType<List<UserItemCreate>>(objectResult.Value);

            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(3, userItems.Count);
        }
        [Fact]
        public async Task GetUserItemsEmpty()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.GetAllUserItems())
                .ReturnsAsync(new List<UserItemCreate>());
            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);
            /// Act
            var actionResult = await controller.GetUserItems();

            /// Assert
            repositoryMock.Verify(r => r.GetAllUserItems());

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var userItems = Assert.IsType<List<UserItemCreate>>(objectResult.Value);

            Assert.Equal(200, objectResult.StatusCode);
            Assert.Empty(userItems);
        }
        [Fact]
        public async Task GetUserItem()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.GetUserItemById("a635ffb0-613c-482e-9e05-25ef055d19a7"))
                .ReturnsAsync(new UserItemCreate { Id = "a635ffb0-613c-482e-9e05-25ef055d19a7", Name = "Arthur", Surname = "Morgan", Age = 36 });
            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.GetUserItem("a635ffb0-613c-482e-9e05-25ef055d19a7");

            /// Assert
            repositoryMock.Verify(r => r.GetUserItemById("a635ffb0-613c-482e-9e05-25ef055d19a7"));

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var userItem = Assert.IsType<UserItemCreate>(objectResult.Value);

            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal("Arthur", userItem.Name);
            Assert.Equal("Morgan", userItem.Surname);
            Assert.Equal((ushort) 36, userItem.Age);
        }
        [Fact]
        public async Task GetUserItemNoId()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.GetUserItemById("a635ffb0-613c-482e-9e05-25ef055d19a7"))
                .ReturnsAsync(new UserItemCreate { Id = "a635ffb0-613c-482e-9e05-25ef055d19a7", Name = "Arthur", Surname = "Morgan", Age = 36 });
            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.GetUserItem("");

            /// Assert
            repositoryMock.Verify(r => r.GetUserItemById(""));

            var objectResult = Assert.IsType<NotFoundResult>(actionResult.Result);

            Assert.Equal(404, objectResult.StatusCode);
        }
        [Fact]
        public async Task GetUserItemIncorrectId()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.GetUserItemById("a635ffb0-613c-482e-9e05-25ef055d19a7"))
                .ReturnsAsync(new UserItemCreate { Id = "a635ffb0-613c-482e-9e05-25ef055d19a7", Name = "Arthur", Surname = "Morgan", Age = 36 });
            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.GetUserItem("665fef40-f576-4897-9502-e252cd7cbea3");

            /// Assert
            repositoryMock.Verify(r => r.GetUserItemById("665fef40-f576-4897-9502-e252cd7cbea3"));

            var objectResult = Assert.IsType<NotFoundResult>(actionResult.Result);

            Assert.Equal(404, objectResult.StatusCode);
        }
        [Fact]
        public async Task PostUserItem()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.PostUserItem(new UserItemCreate { Name = "John", Surname = "Marston", Age = 25 });

            /// Assert
            repositoryMock.Verify(r => r.AddUser(It.IsAny<UserItemCreate>()));

            var objectResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var userItem = Assert.IsType<UserItemCreate>(objectResult.Value);

            Assert.Equal(201, objectResult.StatusCode);
            Assert.Equal("John", userItem.Name);
            Assert.Equal("Marston", userItem.Surname);
            Assert.Equal((ushort)25, userItem.Age);
        }
        [Fact]
        public async Task PostUserItemNoName()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Name", "The Name field is required.");

            /// Act
            var actionResult = await controller.PostUserItem(new UserItemCreate { Surname = "Marston", Age = 25 });

            /// Assert
            var objectResult = Assert.IsType<BadRequestResult>(actionResult.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }
        [Fact]
        public async Task PostUserItemNoSurname()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.PostUserItem(new UserItemCreate { Name = "John", Age = 25 });

            /// Assert
            repositoryMock.Verify(r => r.AddUser(It.IsAny<UserItemCreate>()));
            
            var objectResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var userItem = Assert.IsType<UserItemCreate>(objectResult.Value);

            Assert.Equal(201, objectResult.StatusCode);
            Assert.Equal("John", userItem.Name);
            Assert.Equal("", userItem.Surname);
            Assert.Equal((ushort)25, userItem.Age);
        }
        [Fact]
        public async Task PostUserItemNoAge()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Age", "The Age field is required.");

            /// Act
            var actionResult = await controller.PostUserItem(new UserItemCreate { Name = "John", Surname = "Marston" });

            /// Assert
            var objectResult = Assert.IsType<BadRequestResult>(actionResult.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }
        [Fact]
        public async Task PostUserItemOverposting()
        {
            /// Arrange
            string id = "Overposted Id";
            DateTime date = new DateTime(1900, 1, 1);

            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.PostUserItem(new UserItemCreate 
            { 
                Id = id, 
                Name = "John", 
                Surname = "Marston", 
                Age = 25, 
                CreationDate = date 
            });

            /// Assert
            repositoryMock.Verify(r => r.AddUser(It.IsAny<UserItemCreate>()));

            var objectResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var userItem = Assert.IsType<UserItemCreate>(objectResult.Value);

            Assert.Equal(201, objectResult.StatusCode);
            Assert.NotEqual(id, userItem.Id);
            Assert.Equal("John", userItem.Name);
            Assert.Equal("Marston", userItem.Surname);
            Assert.NotEqual(date, userItem.CreationDate);
        }
        [Fact]
        public async Task PutUserItem()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.UpdateUser(new UserItemCreate
                {
                    Name = "Jason",
                    Surname = "Brody",
                    Age = 25
                }));
            repositoryMock
                .Setup(r => r.GetUserItemById("811db0ea-fb33-4226-bec2-6f4fa3d920d6"))
                .ReturnsAsync(new UserItemCreate
                {
                    Id = "811db0ea-fb33-4226-bec2-6f4fa3d920d6",
                    Name = "Vaas",
                    Surname = "Montenegro",
                    Age = 28,
                    CreationDate = new DateTime(2022, 05, 13, 18, 30, 15)
                });

            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.PutUserItem("811db0ea-fb33-4226-bec2-6f4fa3d920d6", new UserItemUpdate
            {
                Name = "Jason",
                Surname = "Brody",
                Age = 25
            });

            /// Assert
            repositoryMock.Verify(r => r.UpdateUser(It.IsAny<UserItemCreate>()));

            var objectResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            var userItem = Assert.IsType<UserItemCreate>(objectResult.Value);

            Assert.Equal(201, objectResult.StatusCode);
            Assert.Equal("811db0ea-fb33-4226-bec2-6f4fa3d920d6", userItem.Id);
            Assert.Equal("Jason", userItem.Name);
            Assert.Equal("Brody", userItem.Surname);
            Assert.Equal((ushort)25, userItem.Age);
            Assert.Equal(new DateTime(2022, 05, 13, 18, 30, 15), userItem.CreationDate);
        }
        [Fact]
        public async Task PutUserItemNoName()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.UpdateUser(new UserItemCreate
                {
                    Surname = "Brody",
                    Age = 25
                }));
            repositoryMock
                .Setup(r => r.GetUserItemById("811db0ea-fb33-4226-bec2-6f4fa3d920d6"))
                .ReturnsAsync(new UserItemCreate
                {
                    Id = "811db0ea-fb33-4226-bec2-6f4fa3d920d6",
                    Name = "Vaas",
                    Surname = "Montenegro",
                    Age = 28,
                    CreationDate = new DateTime(2022, 05, 13, 18, 30, 15)
                });

            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.PutUserItem("811db0ea-fb33-4226-bec2-6f4fa3d920d6", new UserItemUpdate
            {
                Surname = "Brody",
                Age = 25
            });

            /// Assert
            repositoryMock.Verify(r => r.UpdateUser(It.IsAny<UserItemCreate>()));

            var objectResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            var userItem = Assert.IsType<UserItemCreate>(objectResult.Value);

            Assert.Equal(201, objectResult.StatusCode);
            Assert.Equal("811db0ea-fb33-4226-bec2-6f4fa3d920d6", userItem.Id);
            Assert.Equal("Vaas", userItem.Name);
            Assert.Equal("Brody", userItem.Surname);
            Assert.Equal((ushort)25, userItem.Age);
            Assert.Equal(new DateTime(2022, 05, 13, 18, 30, 15), userItem.CreationDate);
        }
        [Fact]
        public async Task PutUserItemNoSurname()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.UpdateUser(new UserItemCreate
                {
                    Name = "Jason",
                    Age = 25
                }));
            repositoryMock
                .Setup(r => r.GetUserItemById("811db0ea-fb33-4226-bec2-6f4fa3d920d6"))
                .ReturnsAsync(new UserItemCreate
                {
                    Id = "811db0ea-fb33-4226-bec2-6f4fa3d920d6",
                    Name = "Vaas",
                    Surname = "Montenegro",
                    Age = 28,
                    CreationDate = new DateTime(2022, 05, 13, 18, 30, 15)
                });

            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.PutUserItem("811db0ea-fb33-4226-bec2-6f4fa3d920d6", new UserItemUpdate
            {
                Name = "Jason",
                Age = 25
            });

            /// Assert
            repositoryMock.Verify(r => r.UpdateUser(It.IsAny<UserItemCreate>()));

            var objectResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            var userItem = Assert.IsType<UserItemCreate>(objectResult.Value);

            Assert.Equal(201, objectResult.StatusCode);
            Assert.Equal("811db0ea-fb33-4226-bec2-6f4fa3d920d6", userItem.Id);
            Assert.Equal("Jason", userItem.Name);
            Assert.Equal("Montenegro", userItem.Surname);
            Assert.Equal((ushort)25, userItem.Age);
            Assert.Equal(new DateTime(2022, 05, 13, 18, 30, 15), userItem.CreationDate);
        }
        [Fact]
        public async Task PutUserItemNoAge()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.UpdateUser(new UserItemCreate
                {
                    Name = "Jason",
                    Surname = "Brody"
                }));
            repositoryMock
                .Setup(r => r.GetUserItemById("811db0ea-fb33-4226-bec2-6f4fa3d920d6"))
                .ReturnsAsync(new UserItemCreate
                {
                    Id = "811db0ea-fb33-4226-bec2-6f4fa3d920d6",
                    Name = "Vaas",
                    Surname = "Montenegro",
                    Age = 28,
                    CreationDate = new DateTime(2022, 05, 13, 18, 30, 15)
                });

            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.PutUserItem("811db0ea-fb33-4226-bec2-6f4fa3d920d6", new UserItemUpdate
            {
                Name = "Jason",
                Surname = "Brody"
            });

            /// Assert
            repositoryMock.Verify(r => r.UpdateUser(It.IsAny<UserItemCreate>()));

            var objectResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            var userItem = Assert.IsType<UserItemCreate>(objectResult.Value);

            Assert.Equal(201, objectResult.StatusCode);
            Assert.Equal("811db0ea-fb33-4226-bec2-6f4fa3d920d6", userItem.Id);
            Assert.Equal("Jason", userItem.Name);
            Assert.Equal("Brody", userItem.Surname);
            Assert.Equal((ushort)28, userItem.Age);
            Assert.Equal(new DateTime(2022, 05, 13, 18, 30, 15), userItem.CreationDate);
        }
        [Fact]
        public async Task PutUserItemOverposting()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.UpdateUser(new UserItemCreate
                {
                    Id = "Overposted Id",
                    Name = "Jason",
                    Surname = "Brody",
                    Age = 25,
                    CreationDate = new DateTime(1900, 1, 1)
                }));
            repositoryMock
                .Setup(r => r.GetUserItemById("811db0ea-fb33-4226-bec2-6f4fa3d920d6"))
                .ReturnsAsync(new UserItemCreate
                {
                    Id = "811db0ea-fb33-4226-bec2-6f4fa3d920d6",
                    Name = "Vaas",
                    Surname = "Montenegro",
                    Age = 28,
                    CreationDate = new DateTime(2022, 05, 13, 18, 30, 15)
                });

            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.PutUserItem("811db0ea-fb33-4226-bec2-6f4fa3d920d6", new UserItemUpdate
            {
                Name = "Jason",
                Surname = "Brody",
                Age = 25
            });

            /// Assert
            repositoryMock.Verify(r => r.UpdateUser(It.IsAny<UserItemCreate>()));

            var objectResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            var userItem = Assert.IsType<UserItemCreate>(objectResult.Value);

            Assert.Equal(201, objectResult.StatusCode);
            Assert.Equal("811db0ea-fb33-4226-bec2-6f4fa3d920d6", userItem.Id);
            Assert.Equal("Jason", userItem.Name);
            Assert.Equal("Brody", userItem.Surname);
            Assert.Equal((ushort)25, userItem.Age);
            Assert.Equal(new DateTime(2022, 05, 13, 18, 30, 15), userItem.CreationDate);
        }
        [Fact]
        public async Task PutUserItemEmpty()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.UpdateUser(new UserItemCreate()));
            repositoryMock
                .Setup(r => r.GetUserItemById("811db0ea-fb33-4226-bec2-6f4fa3d920d6"))
                .ReturnsAsync(new UserItemCreate
                {
                    Id = "811db0ea-fb33-4226-bec2-6f4fa3d920d6",
                    Name = "Vaas",
                    Surname = "Montenegro",
                    Age = 28,
                    CreationDate = new DateTime(2022, 05, 13, 18, 30, 15)
                });

            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.PutUserItem("811db0ea-fb33-4226-bec2-6f4fa3d920d6", new UserItemUpdate());

            /// Assert
            repositoryMock.Verify(r => r.UpdateUser(It.IsAny<UserItemCreate>()));

            var objectResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            var userItem = Assert.IsType<UserItemCreate>(objectResult.Value);

            Assert.Equal(201, objectResult.StatusCode);
            Assert.Equal("811db0ea-fb33-4226-bec2-6f4fa3d920d6", userItem.Id);
            Assert.Equal("Vaas", userItem.Name);
            Assert.Equal("Montenegro", userItem.Surname);
            Assert.Equal((ushort)28, userItem.Age);
            Assert.Equal(new DateTime(2022, 05, 13, 18, 30, 15), userItem.CreationDate);
        }
        [Fact]
        public async Task PutUserItemNoId()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.UpdateUser(new UserItemCreate
                {
                    Name = "Jason",
                    Surname = "Brody",
                    Age = 25
                }));
            repositoryMock
                .Setup(r => r.GetUserItemById("811db0ea-fb33-4226-bec2-6f4fa3d920d6"))
                .ReturnsAsync(new UserItemCreate
                {
                    Id = "811db0ea-fb33-4226-bec2-6f4fa3d920d6",
                    Name = "Vaas",
                    Surname = "Montenegro",
                    Age = 28,
                    CreationDate = new DateTime(2022, 05, 13, 18, 30, 15)
                });

            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.PutUserItem("", new UserItemUpdate
            {
                Name = "Jason",
                Surname = "Brody",
                Age = 25
            });

            /// Assert
            var objectResult = Assert.IsType<BadRequestResult>(actionResult);

            Assert.Equal(400, objectResult.StatusCode);
        }
        [Fact]
        public async Task DeleteUserItem()
        {
            /// Arrange
            string id = "0c3b0a22-67f2-4399-8504-d5fdb03c093f";

            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.DeleteUser(new UserItemCreate 
                { 
                    Id = id, 
                    Name = "Jack", 
                    Surname = "Marston", 
                    Age = 12, 
                    CreationDate = DateTime.Now 
                }))
                .Returns(Task.CompletedTask)
                .Verifiable();
            repositoryMock.Setup(r => r.GetUserItemById(id))
                .ReturnsAsync(new UserItemCreate
                {
                    Id = id,
                    Name = "Jack",
                    Surname = "Marston",
                    Age = 12,
                    CreationDate = DateTime.Now
                });

            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.DeleteUserItem(id);

            /// Assert
            repositoryMock.Verify(r => r.DeleteUser(It.IsAny<UserItemCreate>()));

            var objectResult = Assert.IsType<NoContentResult>(actionResult);            
        }
        [Fact]
        public async Task DeleteUserItemNoId()
        {
            /// Arrange
            string id = "0c3b0a22-67f2-4399-8504-d5fdb03c093f";
            string emptyId = null;

            var loggerMock = new Mock<ILogger<UsersController>>();
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock
                .Setup(r => r.DeleteUser(new UserItemCreate
                {
                    Id = id,
                    Name = "Jack",
                    Surname = "Marston",
                    Age = 12,
                    CreationDate = DateTime.Now
                }))
                .Returns(Task.CompletedTask)
                .Verifiable();
            repositoryMock.Setup(r => r.GetUserItemById(id))
                .ReturnsAsync(new UserItemCreate
                {
                    Id = id,
                    Name = "Jack",
                    Surname = "Marston",
                    Age = 12,
                    CreationDate = DateTime.Now
                });

            var controller = new UsersController(repositoryMock.Object, loggerMock.Object);

            /// Act
            var actionResult = await controller.DeleteUserItem(emptyId);

            /// Assert
            var objectResult = Assert.IsType<BadRequestResult>(actionResult);

            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}