namespace projeto_xp.Api.Models
{
    public class UserItemUpdate
    {
        private DateTime creationDate;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public ushort? Age { get; set; }
        public DateTime CreationDate { get { return creationDate; } set { creationDate = value; } }
        public string? Id { get; set; }
    }
}
