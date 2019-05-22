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
    public class UserPreferencesController : Controller
    {
        public ApplicationDbContext Context = new ApplicationDbContext();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserPreference userPreference)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userPreference.ApplicationUserId = User.Identity.GetUserId();
                    Context.UserPreferences.Add(userPreference);
                    //if preferences array contains 'Chinese' then add 'Chinese' to 
                    //a new list which will be added to userPreference.Ethnicity

                    List<string> preferences = new List<string>();

                    foreach (var item in ViewBag.Preferences)
                    {
                        preferences.Add(item);
                    }
                    if (preferences.Contains("American"))
                    {
                        userPreference.Ethnicity.Add("American");
                    }
                    if (preferences.Contains("Chinese"))
                    {
                        userPreference.Ethnicity.Add("Chinese");
                    }
                    if (preferences.Contains("Fillipino"))
                    {
                        userPreference.Ethnicity.Add("Fillipino");
                    }
                    if (preferences.Contains("Healthy"))
                    {
                        userPreference.Ethnicity.Add("Healthy");
                    }
                    if (preferences.Contains("Indian"))
                    {
                        userPreference.Ethnicity.Add("Indian");
                    }
                    if (preferences.Contains("Italian"))
                    {
                        userPreference.Ethnicity.Add("Italian");
                    }
                    if (preferences.Contains("Sandwiches"))
                    {
                        userPreference.Ethnicity.Add("Sandwiches");
                    }
                    if (preferences.Contains("Vegan"))
                    {
                        userPreference.Ethnicity.Add("Vegan");
                    }
                    if (preferences.Contains("oneDollar"))
                    {
                        userPreference.Price.Add("oneDollar");
                    }
                    if (preferences.Contains("twoDollar"))
                    {
                        userPreference.Price.Add("twoDollar");
                    }
                    if (preferences.Contains("threeDollar"))
                    {
                        userPreference.Price.Add("threeDollar");
                    }
                    if (preferences.Contains("fourDollar"))
                    {
                        userPreference.Price.Add("fourDollar");
                    }
                    if (preferences.Contains("fiveDollar"))
                    {
                        userPreference.Price.Add("fiveDollar");
                    }
                    if (preferences.Contains("fivemiles"))
                    {
                        userPreference.Range.Add("fivemiles");
                    }
                    if (preferences.Contains("tenmiles"))
                    {
                        userPreference.Range.Add("tenmiles");
                    }
                    if (preferences.Contains("fifteenmiles"))
                    {
                        userPreference.Range.Add("fifteenmiles");
                    }
                    if (preferences.Contains("twentymiles"))
                    {
                        userPreference.Range.Add("twentymiles");
                    }
                    if (preferences.Contains("twentyfivemiles"))
                    {
                        userPreference.Range.Add("twentyfivemiles");
                    }
                    if (preferences.Contains("onestar"))
                    {
                        userPreference.Rating.Add("onestar");
                    }
                    if (preferences.Contains("twostar"))
                    {
                        userPreference.Rating.Add("twostar");
                    }
                    if (preferences.Contains("threestar"))
                    {
                        userPreference.Rating.Add("threestar");
                    }
                    if (preferences.Contains("fourstar"))
                    {
                        userPreference.Rating.Add("fourstar");
                    }
                    if (preferences.Contains("fivestar"))
                    {
                        userPreference.Rating.Add("fivestar");
                    }


                    Context.SaveChanges();
                    return RedirectToAction("MapAsync", "Home");
                }
                catch (Exception)
                {
                    return View();
                }
            }

            return View("Preferences");
        }
    }
}