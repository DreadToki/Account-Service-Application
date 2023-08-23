namespace AccCreatingUnitTests;

[TestFixture]
public class IncidentControllerTests
{
    [Test]
    public async Task CreateIncident_GoodIncident_Passes()
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
                AccountName = "UserNameTesting 1",
                ContactEmail = "testingEmail@gmail.com",
                ContactFirstName = "RomanTest",
                ContactLastName = "MorozTest",
                IncidentDescription = "Incident test"
            };
            IncidentController controller = new(context);
            var result = await controller.CreateIncident(requestObj);
            var okResult = result as ObjectResult;

            // ASSERT
            Assert.That(okResult.StatusCode, Is.EqualTo(200),
                $"Test Failed: The result was {okResult.StatusCode}.");
            Assert.Pass("Test Passed: Success! [IncidentControllerTests/CreateIncident_GoodIncident_Passes]");
        }
    }

    [Test]
    public async Task CreateIncident_AccountNameIsNullOrWhiteSpace_Passes()
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
                AccountName = String.Empty,
            };
            IncidentController controller = new(context);
            var result = await controller.CreateIncident(requestObj);
            var okResult = result as ObjectResult;

            // ASSERT
            Assert.That(okResult.StatusCode, Is.EqualTo(400),
                $"Test Failed: The result was {okResult.StatusCode}.");
            Assert.Pass("Test Passed: Success! [IncidentControllerTests/CreateIncident_AccountNameIsNullOrWhiteSpace_Passes]");
        }
    }

    [Test]
    public async Task CreateIncident_AccountNameDoesNotExist_Passes()
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
                AccountName = "UserNameTesting 2",
            };
            IncidentController controller = new(context);
            var result = await controller.CreateIncident(requestObj);
            var okResult = result as ObjectResult;

            // ASSERT
            Assert.That(okResult.StatusCode, Is.EqualTo(400),
                $"Test Failed: The result was {okResult.StatusCode}.");
            Assert.Pass("Test Passed: Success! [IncidentControllerTests/CreateIncident_AccountNameDoesNotExist_Passes]");
        }
    }

    [Test]
    public async Task CreateIncident_AccountHasAnIncident_Passes()
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
                Name = "UserNameTesting 1"
            };
            Incident incident = new()
            {
                Name = "INC-036",
                Account = new()
            };
            incident.Account.Add(account);
            context.Add(incident);
            await context.SaveChangesAsync();

            // ACT - Creating Request object to run the test
            Request requestObj = new()
            {
                AccountName = "UserNameTesting 1",
            };
            IncidentController controller = new(context);
            var result = await controller.CreateIncident(requestObj);
            var okResult = result as ObjectResult;

            // ASSERT
            Assert.That(okResult.StatusCode, Is.EqualTo(400),
                $"Test Failed: The result was {okResult.StatusCode}.");
            Assert.Pass("Test Passed: Success! [IncidentControllerTests/CreateIncident_AccountHasAnIncident_Passes]");
        }
    }
}
