using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MealDeals.Models
{
    public class Special
    {
        [Key]
        public int Id { get; set; }
        public string StreetNumber { get; set; }
        public string Direction { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Restaurant { get; set; }
        public string Description { get; set; }
        public string Ethnicity { get; set; }
        public string Price { get; set; }
        public double Distance { get; set; }
        public double Rating { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }

        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
    }

    public class ListAllSpecials
    {
        public IEnumerable<Special> AllSpecials { get; set; }
    }
}