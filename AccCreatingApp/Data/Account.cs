namespace AccCreatingApp.Data
{
    public class Account
    {
        [Column(TypeName = "nvarchar(255)"), ForeignKey("Incident")]
        public string? IncidentName { get; set; }

        public Incident? Incident { get; set; }

        [Key, Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        public List<Contact> Contact { get; set; }
    }
}
