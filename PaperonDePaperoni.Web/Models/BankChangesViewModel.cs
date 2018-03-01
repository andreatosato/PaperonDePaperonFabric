using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaperonDePaperoni.Web.Models
{
    public class BankChangesViewModel
    {
        public decimal Deposit { get; set; }
        public decimal Withdraw { get; set; }
        public CurrentActorStateViewModel CurrenteActorState { get; set; }
    }
}
