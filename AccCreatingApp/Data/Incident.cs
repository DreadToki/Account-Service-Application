namespace AccCreatingApp.Data
{
    public class Incident
    {
        [Key, Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Description { get; set; }

        public List<Account> Account { get; set; }
    }
}
