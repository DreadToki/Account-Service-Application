namespace AccCreatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly DataContext _dataContext;

        public AccountController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateAccount(Request request)
        {
            Account account = await Queries.GetAccount(_dataContext, request.AccountName);

            if (account != null)
            {
                return NotFound($"Account {account.Name} already exists.");
            }
            else
            {
                account = new Account
                {
                    Name = request.AccountName,
                    Contact = new()
                };

                Contact contact = await Queries.GetContact(_dataContext, request.ContactEmail);

                if (contact == null)
                {
                    contact = new Contact
                    {
                        Email = request.ContactEmail,
                        FirstName = request.ContactFirstName,
                        LastName = request.ContactLastName
                    };
                    account.Contact.Add(contact);
                }

                _dataContext.Add(account);
                await _dataContext.SaveChangesAsync();
                return Ok(new
                {
                    AccountName = account.Name
                });
            }
        }
    }
}
