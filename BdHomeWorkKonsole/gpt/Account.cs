using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW.Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public string AccountType { get; set; }

        public decimal Balance { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
