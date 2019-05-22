using MealDeals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;

namespace MealDeals.Controllers
{
    public class HomeController  : Controller
    {
        public ApplicationDbContext Context = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View("Home");
        }

        public async Task<ActionResult> MapAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52346/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync("api/Specials");
                    response.EnsureSuccessStatusCode();
                    string data = await response.Content.ReadAsStringAsync();                    
                    var jsonResults = JsonConvert.DeserializeObject<IEnumerable<Special>>(data).ToList();
                    var filtered = Filter(jsonResults);

                    return View("Map", filtered);
                }
                catch (Exception e)
                {
                    return View("Home");
                }           
            }
        }
        public IEnumerable<Special> Filter(IEnumerable<Special> specials)
        {
            var currentUser = Context.Users.Where(u => u.Id == (User.Identity.GetUserId()));
            var currentUserPreferences = Context.UserPreferences.Where(p => p.ApplicationUserId == User.Identity.GetUserId()).SingleOrDefault();

            if (currentUser == null)
            {
                var filteredSpecials = specials;
                return filteredSpecials;
            }
            else
	        {
                var filteredByEthnicity = specials.Where(s => s.Ethnicity.Equals(currentUserPreferences.Ethnicity.Any()));
                var filteredByRange = filteredByEthnicity.Where(s => s.Distance.Equals(currentUserPreferences.Range.Any()));
                var filteredByPrice = filteredByRange.Where(s => s.Price.Equals(currentUserPreferences.Price.Any()));
                var filteredByRating = filteredByPrice.Where(s => s.Rating.Equals(currentUserPreferences.Rating.Any()));
                return filteredByRating;
            }            
        }

        public ActionResult About()
        {            
            return View();
        }

        public ActionResult AddSpecials()
        {
            Special special = new Special();
            return View(special);
        }

        [HttpPost]
        public async Task<ActionResult> AddSpecials(Special special)
        {
            YelpAPICall yelpAPICall = new YelpAPICall();
            Special result = yelpAPICall.GetReviewAndPrice(special.City, special.Restaurant);
            special.Rating = result.Rating;
            special.Price = result.Price;

            LocalAPICall localAPICall = new LocalAPICall();
            await localAPICall.PostLocal(special);

            return RedirectToAction("MapAsync");
        }

        public ActionResult EditSpecials(int Id)
        {
            LocalAPICall localAPICall = new LocalAPICall();
            List<Special> ListSpecials = localAPICall.GetLocal().Result;
            var selectedSpecial = ListSpecials.Where(l => l.Id == Id);
            return View(selectedSpecial);
        }

        [HttpPost]
        public ActionResult EditSpecials(Special selectedSpecial)
        {
            LocalAPICall localAPICall = new LocalAPICall();
            
            localAPICall.PutLocal(selectedSpecial);
            
            return RedirectToAction("MapAsync");
        }
        //public ActionResult Contact()
        //{
        //    YelpAPICall yelpCall = new YelpAPICall();
        //    ViewBag.Message = "Your contact page.";
        //    YelpModel result = yelpCall.GetReviewAndPrice("Milwaukee", "Tre Rivali");
        //    return View(result);
        //}
    }
}