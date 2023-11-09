using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Autofac.Extras.Moq;
using DataAccess.Contracts;
using DataModels.DTO;
using DataModels.Entities;
using Moq;
using Service;

namespace Services.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private List<User> users = new List<User>
        {
            new User { Id = 1, Name = "John", Email = "john@example.com", Role = 1, Cars = new List<Car>()},
            new User { Id = 2, Name = "Anna", Email = "anna@example.com", Role = 2, Cars = new List<Car>()},
            new User { Id = 3, Name = "Tom", Email = "tom@example.com", Role = 1, Cars = new List<Car>()}
        };

        private User GetSampleUser(int id)
        {
            return users.Find(x => x.Id == id);
        }

        private bool DeleteSampleUser(int id)
        {
            var user = users.Find(x => x.Id == id);
            return users.Remove(user);
        }

        private bool UpdateSampleUser(int id, UpdateUserDTO dto)
        { 
            var user = users.Find(x => x.Id == id);
            if (user is null) return false;
            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Role = dto.Role;

            return true;
        }

        [TestMethod]
        public async Task GetUsers_ConnectedToDB_ShouldReturnUserModels()
        {
            var expected = new List<User>
            {
                new User {Id = 1, Name = "John", Email = "john@example.com", Role = 1, Cars = new List<Car>()},
                new User { Id = 2, Name = "Anna", Email = "anna@example.com", Role = 2, Cars = new List<Car>()},
                new User { Id = 3, Name = "Tom", Email = "tom@example.com", Role = 1, Cars = new List<Car>()}
            };

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.GetUsers())
                    .ReturnsAsync(users);

                var cls = mock.Create<UserService>();
                var actual = (await cls.GetUsers()).ToList();

                mock.Mock<IUserRepository>()
                    .Verify(x => x.GetUsers(), Times.Exactly(1));

                Assert.AreEqual(expected.Count, actual.ToList().Count);

                for (var i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(expected[i].Id, actual[i].Id);
                    Assert.AreEqual(expected[i].Name, actual[i].Name);
                    Assert.AreEqual(expected[i].Email, actual[i].Email);
                    Assert.AreEqual(expected[i].Role, actual[i].Role);
                }
            }

        }

        [TestMethod]
        public async Task GetUsers_NotConnectedToDB_ShouldThrowException()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.GetUsers())
                    .ThrowsAsync(new Exception("Simulated error message"));

                var cls = mock.Create<UserService>();

                Assert.ThrowsExceptionAsync<Exception>(async () => await cls.GetUsers());

                mock.Mock<IUserRepository>()
                    .Verify(x => x.GetUsers(), Times.Exactly(1));
            }
        }

        //public Task<User> GetUser(int id);
        // GetUser - valid id
        [TestMethod]
        public async Task GetUser_UsingValidId_ShouldReturnUserModel()
        {
            var id = 2;
            var expected = new User
                {Id = 2, Name = "Anna", Email = "anna@example.com", Role = 2, Cars = new List<Car>()};

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.GetUser(id))
                    .ReturnsAsync(GetSampleUser(id));

                var cls = mock.Create<UserService>();

                var actual = await cls.GetUser(id);

                mock.Mock<IUserRepository>()
                    .Verify(x => x.GetUser(id), Times.Exactly(1));

                Assert.IsTrue(actual != null);
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.Email, actual.Email);
                Assert.AreEqual(expected.Role, actual.Role);
            }
        }
        // GetUser - not existing id
        [TestMethod]
        public async Task GetUser_UsingNotValidId_ShouldReturnUserModel()
        {
            var id = 9999;
            User expected = null;

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.GetUser(id))
                    .ReturnsAsync(GetSampleUser(id));

                var cls = mock.Create<UserService>();

                var actual = await cls.GetUser(id);

                mock.Mock<IUserRepository>()
                    .Verify(x => x.GetUser(id), Times.Exactly(1));

                Assert.IsTrue(actual is null);
            }
        }

        // GetUser - exception thrown
        [TestMethod]
        public async Task GetUser_ErrorConnectingToDb_ShouldReturnUserModel()
        {
            var id = 1;

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.GetUser(id))
                    .ThrowsAsync(new Exception("Simulated error message"));

                var cls = mock.Create<UserService>();

                Assert.ThrowsExceptionAsync<Exception>(async () => await cls.GetUser(id));

                mock.Mock<IUserRepository>()
                    .Verify(x => x.GetUser(id), Times.Exactly(1));
            }
        }

        // CreateUser - valid user
        [TestMethod]
        public async Task CreateUser_ProvidingValidData_ShouldSuccessfullyAddNewUserToDb()
        {
            var userDto = new CreateUserDTO("Michael", "michael@example.com", 1);
            var expected = new User
                {Id = 4, Name = userDto.Name, Email = userDto.Email, Role = userDto.Role, Cars = new List<Car>()};

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.CreateUser(userDto))
                    .Returns<CreateUserDTO>(async (userDto) =>
                    {
                        var newUser = new User
                        {
                            Id = 4,
                            Name = userDto.Name,
                            Email = userDto.Email,
                            Role = userDto.Role,
                            Cars = new List<Car>()
                        };
                        users.Add(newUser);

                        return newUser;
                    });


                var cls = mock.Create<UserService>();

                var actual = await cls.CreateUser(userDto);

                mock.Mock<IUserRepository>()
                    .Verify(x => x.CreateUser(userDto), Times.Exactly(1));

                Assert.IsTrue(actual != null);
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.Email, actual.Email);
                Assert.AreEqual(expected.Role, actual.Role);
                Assert.IsTrue(users.Count == 4);
                Assert.AreEqual(users.Last().Id, expected.Id);
                Assert.AreEqual(users.Last().Name, expected.Name);
                Assert.AreEqual(users.Last().Email, expected.Email);
                Assert.AreEqual(users.Last().Role, expected.Role);
            }
        }
        [TestMethod]
        public async Task CreateUser_ProvidingNull_ShouldThrowException()
        {
            CreateUserDTO userDto = null;

            using (var mock = AutoMock.GetLoose())
            {
                var expectedExceptionThrown = true;
                var actualExceptionThrown = false;
                mock.Mock<IUserRepository>()
                    .Setup(x => x.CreateUser(userDto))
                    .ReturnsAsync(new User());


                var cls = mock.Create<UserService>();
                try
                {
                    await cls.CreateUser(userDto);
                }
                catch (Exception e)
                {
                    actualExceptionThrown = true;
                }

                mock.Mock<IUserRepository>()
                    .Verify(x => x.CreateUser(userDto), Times.Exactly(0));

                Assert.AreEqual(expectedExceptionThrown, actualExceptionThrown);

            }
        }

        [TestMethod]
        public async Task CreateUser_ProvidingValidDataNotConnectedToDb_ShouldThrowException()
        {
            var userDto = new CreateUserDTO("Michael", "michael@example.com", 1);

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.CreateUser(userDto))
                    .ThrowsAsync(new Exception("Simulated exception message."));


                var cls = mock.Create<UserService>();

                Assert.ThrowsExceptionAsync<Exception>(async () => await cls.CreateUser(userDto));

                mock.Mock<IUserRepository>()
                    .Verify(x => x.CreateUser(userDto), Times.Exactly(1));
            }
        }

        [TestMethod]
        public async Task DeleteUser_UsingValidId_ShouldSuccessfullyRemoveUser()
        {
            var id = 2;
            var isIdInitallyExist = GetSampleUser(id) != null;

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.DeleteUser(id))
                    .ReturnsAsync(DeleteSampleUser(id));

                var cls = mock.Create<UserService>();

               await cls.DeleteUser(id);

                mock.Mock<IUserRepository>()
                    .Verify(x => x.DeleteUser(id), Times.Exactly(1));

                Assert.IsTrue(users.Count == 2);
                Assert.IsTrue(isIdInitallyExist);
                Assert.IsTrue(users.Find(user => user.Id == id) is null);
            }
        }

        // todo Consider throwing exception
        [TestMethod]
        public async Task DeleteUser_UsingNotExistingId_ShouldNotRemoveAffectExistingUsers()
        {
            var id = 9999;
            var isIdInitallyExist = GetSampleUser(id) != null;

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.DeleteUser(id))
                    .ReturnsAsync(DeleteSampleUser(id));

                var cls = mock.Create<UserService>();

                await cls.DeleteUser(id);

                mock.Mock<IUserRepository>()
                    .Verify(x => x.DeleteUser(id), Times.Exactly(1));

                Assert.IsTrue(users.Count == 3);
                Assert.IsFalse(isIdInitallyExist);
            }
        }

        [TestMethod]
        public async Task DeleteUser_UsingValidIdNotConnectedToDb_ShouldThrowException()
        {
            var id = 2;

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.DeleteUser(id))
                    .ThrowsAsync(new Exception("Simulated exception message."));

                var cls = mock.Create<UserService>();

                Assert.ThrowsExceptionAsync<Exception>(async () => await cls.DeleteUser(id));

                mock.Mock<IUserRepository>()
                    .Verify(x => x.DeleteUser(id), Times.Exactly(1));
            }
        }
        
        // UpdateUserDTO - not existing id
        // UpdateUserDTO - exception thrown
        //UpdateUserDTO - null instead of updateuserdto

        [TestMethod]
        public async Task UpdateUser_UsingValidData_ShouldSuccessfullyUpdateUser()
        {
            var id = 2;
            var user = GetSampleUser(2);
            var updatedUser = GetSampleUser(2);
            var userDto = new UpdateUserDTO("Anna", "anna@updatedemail.com", 2);

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.UpdateUser(id, userDto))
                    .ReturnsAsync(UpdateSampleUser(id, userDto));

                var cls = mock.Create<UserService>();

                await cls.UpdateUser(id, userDto);

                mock.Mock<IUserRepository>()
                    .Verify(x => x.UpdateUser(id, userDto), Times.Exactly(1));

                Assert.IsTrue(users.Count == 3);
                Assert.AreEqual(updatedUser.Email, "anna@updatedemail.com");
                Assert.AreEqual(updatedUser.Name, "Anna");
                Assert.AreEqual(updatedUser.Role, 2);
            }
        }

        [TestMethod]
        public async Task UpdateUser_UsingNotExistingId_ShouldThrowException()
        {
            var id = 99999;
            var userDto = new UpdateUserDTO("Anna", "anna@updatedemail.com", 2);

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.UpdateUser(id, userDto))
                    .ReturnsAsync(UpdateSampleUser(id, userDto));

                var cls = mock.Create<UserService>();

                Assert.ThrowsExceptionAsync<ArgumentException>(async () => await cls.UpdateUser(id, userDto));
                mock.Mock<IUserRepository>()
                    .Verify(x => x.UpdateUser(id, userDto), Times.Exactly(1));

                Assert.IsTrue(users.Count == 3);
            }
        }

        [TestMethod]
        public async Task UpdateUser_UsingInvalidData_ShouldThrowException()
        {
            var id = 99999;
            UpdateUserDTO userDto = null;

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(x => x.UpdateUser(id, userDto))
                    .ReturnsAsync(UpdateSampleUser(id, userDto));

                var cls = mock.Create<UserService>();

                Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await cls.UpdateUser(id, userDto));
                mock.Mock<IUserRepository>()
                    .Verify(x => x.UpdateUser(id, userDto), Times.Exactly(0));

                Assert.IsTrue(users.Count == 3);
            }
        }
    }
}
