namespace AccCreatingApp.Data
{
    public class Contact
    {
        [Required, Column(TypeName = "nvarchar(255)"), ForeignKey("Account")]
        public string AccountName { get; set; }

        public Account Account { get; set; }

        [Key, Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }

        [Required, Column(TypeName = "nvarchar(255)")]
        public string FirstName { get; set; }

        [Required, Column(TypeName = "nvarchar(255)")]
        public string LastName { get; set; }
    }
}
