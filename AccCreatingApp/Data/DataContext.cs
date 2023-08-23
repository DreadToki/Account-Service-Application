namespace AccCreatingApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Incident> Incident { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Account> Account { get; set; }
    }
}
