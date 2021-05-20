using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        public static readonly string VISA = "VISA";
        public static readonly string MASTER = "MASTER";
        public static readonly string PAYMENT = "PAYMENT";
        public static readonly string ADJUSTMENT = "ADJUSTMENT";
        public String MessageType { get; set; }
        [Key]
        public string TransactionId { get; set; }

        public long AccountId { get; set; }
        public string Origin { get; set; }
        public decimal Amount { get; set; }
        public decimal oldBalance { get; set; }

        public decimal newBalance { get; set; }

        public decimal commision { get; set; }


        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        public DateTime? UpdateDate { get; set; }

    }
}
