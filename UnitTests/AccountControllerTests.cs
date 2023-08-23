namespace AccCreatingUnitTests;

[TestFixture]
public class AccountControllerTests
{
    [Test]
    public async Task CreateAccount_GoodUserName_Passes()
    {
        // ARRANGE - creating In Memory Database
        var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: "AccCreatingServiceTests")
        .Options;

        // Creating mocked Data for the test
        using (var context = new DataContext(options))
        {
            // Deleting the old one database and creating brand new
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Account account = new()
            {
                Name = "UserNameTesting 1",
                Contact = new()
            };
            Contact contact = new()
            {
                Email = "testingEmail@gmail.com",
                FirstName = "RomanTest",
                LastName = "MorozTest"
            };
            account.Contact.Add(contact);
            context.Add(account);
            await context.SaveChangesAsync();

            // ACT - Creating Request object to run the test
            Request requestObj = new()
            {
                AccountName = "UserNameTesting 2",
                ContactEmail = "testingEmail@gmail.com",
                ContactFirstName = "RomanTest",
                ContactLastName = "MorozTest",
                IncidentDescription = "Incident test"
            };
            AccountController controller = new(context);
            var result = await controller.CreateAccount(requestObj);
            var okResult = result as ObjectResult;

            // ASSERT
            Assert.That(okResult.StatusCode, Is.EqualTo(200),
                $"Test Failed: The result was {okResult.StatusCode}.");
            Assert.Pass("Test Passed: Success! [AccountControllerTests/CreateAccount_GoodUserName_Passes]");
        }
    }

    [Test]
    public async Task CreateAccount_UserNameAlreadyExists_Passes()
    {
        // ARRANGE - creating In Memory Database
        var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: "AccCreatingServiceTests") // AccountsTestDataBase
        .Options;

        // Creating mocked Data for the test
        using (var context = new DataContext(options))
        {
            // Deleting the old one database and creating brand new
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Account account = new()
            {
                Name = "UserNameTesting 1",
            };
            context.Add(account);
            await context.SaveChangesAsync();

            // ACT - Creating Request object to run the test
            Request requestObj = new()
            {
                AccountName = "UserNameTesting 1",
            };
            AccountController controller = new(context);
            var result = await controller.CreateAccount(requestObj);
            var okResult = result as ObjectResult;

            // ASSERT
            Assert.That(okResult.StatusCode, Is.EqualTo(404),
                $"Test Failed: The result was {okResult.StatusCode}.");
            Assert.Pass("Test Passed: Success! [AccountControllerTests/CreateAccount_UserNameAlreadyExists_Passes]");
        }
    }
}