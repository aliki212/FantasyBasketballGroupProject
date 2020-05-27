using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantasyEuroleague.Models
{
    public class UserAccount : ApplicationUser
    {

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(255)]
        public string IdentityPapersNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string PrimaryResidence { get; set; }

        [Required]
        [StringLength(255)]
        public string Street { get; set; }

        public string Town { get; set; }

        [Required]
        public int PostalCode { get; set; }

        public Wallet Wallet { get; set; }

        public ICollection<EightPlayerTeam> Teams { get; set; }





        public UserAccount()
        {
            Teams = new Collection<EightPlayerTeam>();
            Wallet = new Wallet();
        }
    }
}