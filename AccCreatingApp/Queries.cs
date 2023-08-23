#pragma warning disable CS8603  // Possible null reference return.

namespace AccCreatingApp
{
    public class Queries
    {
        public static async Task<Contact> GetContact(DataContext context, string contactEmail) =>
            await context.Contact.Where(c => c.Email == contactEmail).FirstOrDefaultAsync();

        public static async Task<Account> GetAccount(DataContext context, string accountName) =>
            await context.Account.Where(a => a.Name == accountName).FirstOrDefaultAsync();

        public static async Task<Account> GetIncidentAccount(DataContext context, string accountName) =>
            await context.Account.Where(a => a.Name == accountName).FirstOrDefaultAsync();

        public static async Task<Incident> GetLastIncident(DataContext context) =>
            await context.Incident.OrderByDescending(i => i.Name).FirstOrDefaultAsync();
    }
}
