using System.ComponentModel.DataAnnotations;

namespace projeto_xp.Models
{
    public class UserItemCreate
    {
        private DateTime creationDate;
        [Required]
        public string? Name { get; set; }
        public string? Surname { get; set; }
        [Required]
        public ushort? Age { get; set; }
        public DateTime CreationDate { get { return creationDate; } set { creationDate = value; } }
        public string? Id { get; set; }
    }
}
