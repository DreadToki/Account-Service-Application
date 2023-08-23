namespace AccCreatingUnitTests;

[TestFixture]
public class ContactControllerTests
{
    [Test]
    public async Task CreateContact_GoodContacts_Passes()
    {
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
                AccountName = "UserNameTesting 1",
                ContactEmail = "testingEmail@gmail.com",
                ContactFirstName = "RomanTest",
                ContactLastName = "MorozTest",
                IncidentDescription = "Incident test"
            };
            ContactController controller = new(context);
            var result = await controller.CreateContact(requestObj);
            var okResult = result as ObjectResult;

            // ASSERT
            Assert.That(okResult.StatusCode, Is.EqualTo(200),
                $"Test Failed: The result was {okResult.StatusCode}.");
            Assert.Pass("Test Passed: Success! [ContactControllerTests/CreateContact_GoodContacts_Passes]");
        }

    }

    [Test]
    public async Task CreateContact_AccountNameWasNotFound_Passes()
    {
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
                AccountName = "UserNameTesting 2",
            };
            ContactController controller = new(context);
            var result = await controller.CreateContact(requestObj);
            var okResult = result as ObjectResult;

            // ASSERT
            Assert.That(okResult.StatusCode, Is.EqualTo(404),
                $"Test Failed: The result was {okResult.StatusCode}.");
            Assert.Pass("Test Passed: Success! [ContactControllerTests/CreateContact_AccountNameWasNotFound_Passes]");
        }

    }
}
