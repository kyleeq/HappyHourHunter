using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealDeals.Models
{
    public class YelpModel
    {
        public string price { get; set; }
        public double rating { get; set; }

        // Create instance for Yelp API Call put in Add Special ActionResult in SpecialsController in MealDealsSpecials
        //YelpAPICall yelpAPICall = new YelpAPICall();
        //yelpAPICall.GetReviewAndPrice(Special.City, Special.Restaurant);
        
    }
}