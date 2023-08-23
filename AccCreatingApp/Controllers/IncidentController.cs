namespace AccCreatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        readonly DataContext _dataContext;

        public IncidentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateIncident(Request request)
        {
            if (string.IsNullOrWhiteSpace(request.AccountName))
            {
                return BadRequest("request.AccountName is null or empty.");
            }

            Account account = await Queries.GetAccount(_dataContext, request.AccountName);

            if (account == null)
            {
                return NotFound($"This account: \'{request.AccountName}\' does not exist in the system.");
            }

            Account accontIncedent = await Queries.GetIncidentAccount(_dataContext, request.AccountName);

            if (accontIncedent.IncidentName != null)
            {
                return BadRequest($"This account \'{request.AccountName}\' has already have an incident.");
            }

            var incidentName = GenerateNewIncidentName();

            Incident incident = new()
            {
                Name = incidentName.Result,
                Description = request.IncidentDescription,
                Account = new()
            };
            incident.Account.Add(account);

            _dataContext.Add(incident);
            await _dataContext.SaveChangesAsync();
            return Ok(new
            {
                IncidentName = incident.Name,
                IncidentDescription = incident.Description
            });
        }

        async Task<string> GenerateNewIncidentName()
        {
            Incident incident = await Queries.GetLastIncident(_dataContext);
            if (incident == null)
            {
                return "INC-0001";
            }
            string digits = incident.Name.Split('-').Last();
            if (int.TryParse(digits, out int number))
            {
                number++;
            }
            else
            {
                return string.Empty;
            }
            return $"INC-{number:D4}";
        }
    }
}
