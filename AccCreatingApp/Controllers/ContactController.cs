namespace AccCreatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        readonly DataContext _dataContext;

        public ContactController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateContact(Request request)
        {
            Account account = await Queries.GetAccount(_dataContext, request.AccountName);

            if (account == null)
            {
                return NotFound($"Account {request.AccountName} was not found.");
            }

            Contact contact = await Queries.GetContact(_dataContext, request.ContactEmail);

            if (contact != null)
            {
                if (string.IsNullOrEmpty(contact.AccountName))
                {
                    contact.AccountName = account.Name;
                    contact.Account = account;
                }
                contact.FirstName = request.ContactFirstName;
                contact.LastName = request.ContactLastName;
            }
            else
            {
                contact = new Contact
                {
                    AccountName = account.Name,
                    Account = account,
                    Email = request.ContactEmail,
                    FirstName = request.ContactFirstName,
                    LastName = request.ContactLastName
                };
                _dataContext.Add(contact);
            }

            await _dataContext.SaveChangesAsync();
            return Ok(new
            {
                ContactName = $"{contact.LastName}, {contact.FirstName}",
                ContactEmail = contact.Email
            });
        }
    }
}
