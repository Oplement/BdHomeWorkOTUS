using System.ComponentModel.DataAnnotations;


namespace HW.Entities
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
