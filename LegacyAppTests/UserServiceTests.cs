using LegacyApp;

namespace LegacyAppTests
{
    public class UserServiceTests
    {
        [Fact]
        public void AddUser_Should_Return_False_When_Missing_FirstName()
        {
            //Arrange
            var service = new UserService();

            //Act
            var result = service.AddUser(null, null, "user@name.pl", new DateTime(1989, 4, 4), 1);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddUser_Should_Return_False_When_Email_Validation_Fails()
        {
            var service = new UserService();
            var result = service.AddUser("Andrzej", "Michalecki", "user.name.pl", new DateTime(1989, 4, 4), 1) 
                ||
                service.AddUser("Andrzej", "Michalecki", "user@name", new DateTime(1989, 4, 4), 1);
            Assert.False(result);
        }

        [Fact]
        public void AddUser_Should_Throw_Exception_When_Client_Does_Not_Exists()
        {
            var service = new UserService();
            Assert.Throws<ArgumentException>(() => 
            { 
                _ = service.AddUser("Andrzej", "Michalecki", "user@name.pl", new DateTime(1989, 4, 4), 100);
            });
        }

        [Fact]
        public void AddUser_Should_Return_False_When_Age_Lower_Than_21()
        {
            var service = new UserService();
            var result = service.AddUser("Jesse", "James", "jess@jam.pl", DateTime.Now.AddYears(-21).AddMonths(1), 1);
            Assert.False(result);
        }

        [Fact]
        public void AddUser_Should_Throw_Exception_When_LastName_Does_Not_Exist_In_UserCreditService()
        {
            var service = new UserService();
            Assert.Throws<ArgumentException>(() =>
            {
                _ = service.AddUser("Andrzej", "Michalecki", "user@name.pl", new DateTime(1989, 4, 4), 1);
            });
        }
    }
}