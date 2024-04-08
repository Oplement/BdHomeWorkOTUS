using System.ComponentModel.DataAnnotations;

namespace HW.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public int AccountId { get; set; }

        public string TransactionType { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public virtual Account Account { get; set; }
    }
}
