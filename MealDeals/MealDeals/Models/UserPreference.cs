using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MealDeals.Models
{
    public class UserPreference
    {
        [Key]
        public int Id { get; set; }
        public List<string> Range { get; set; }
        public List<string> Price { get; set; }
        public List<string> Rating { get; set; }
        public List<string> Ethnicity { get; set; }
        public List<string> TypeOfSpecial { get; set; }

        [ForeignKey("ApplicationUser")]
        [Display(Name = "UserId")]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}