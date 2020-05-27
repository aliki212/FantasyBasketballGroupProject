using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FantasyEuroleague.Models
{
    public class Wallet
    {
        public string UserAccountId { get; set; }
        public UserAccount UserAccount{ get; set; }
        public float Amount { get; set; }

        public Wallet()
        {

        }
       
    }
}