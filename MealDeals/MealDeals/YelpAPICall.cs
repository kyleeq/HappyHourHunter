using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using SimpleOAuth;
using System.Text;
using MealDeals.Models;

namespace MealDeals
{
    public class YelpAPICall
    {
        const string subscriptionKey = Keys.YelpAPIKey;
        const string uriBase = "https://api.yelp.com/v3/businesses/";
        static HttpClient Client = new HttpClient();

        
        public Special GetReviewAndPrice(string City, string RestaurantName)
        {
            RestaurantName = RestaurantName.Replace(" ", "-");
            string formattedRestaurantParam = RestaurantName + "-" + City;
            string url = uriBase + formattedRestaurantParam;

            var myWebRequest = WebRequest.Create(url);
            var myHttpWebRequest = (HttpWebRequest)myWebRequest;
            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Headers.Add("Authorization", "Bearer " + subscriptionKey);
            myHttpWebRequest.Accept = "application/json";

            var myWebResponse = myWebRequest.GetResponse();
            var responseStream = myWebResponse.GetResponseStream();
            if (responseStream == null) return null;

            var myStreamReader = new StreamReader(responseStream, Encoding.Default);
            var json = myStreamReader.ReadToEnd();

            responseStream.Close();
            myWebResponse.Close();


            //WebRequest request = WebRequest.Create(url);
            //request.Method = "GET";
            //// Authorization to use Yelp API
            //request.(
            //    new Tokens
            //    {
            //        ConsumerKey = subscriptionKey
            //    }

            //).WithEncryption(EncryptionMethod.HMACSHA1).InHeader();
            //HttpWebResponse responseObject = (HttpWebResponse)request.GetResponse();
            //string urlResult = null;
            //using (Stream stream = responseObject.GetResponseStream())
            //{
            //    StreamReader sr = new StreamReader(stream);
            //    urlResult = sr.ReadToEnd();
            //    sr.Close();
            //}
            Special results;            
            // deserialize: converting plain text into objects 
            YelpRootObject yelp = JsonConvert.DeserializeObject<YelpRootObject>(json);
            try
            {
                results = new Special{
                    Rating = yelp.rating,
                    Price = yelp.price.ToString()
                }

                /*results = yelp.rating + "," + yelp.price*/;
            }
            catch
            {
                return null;
            }
            return results;
        }
    }
    public class Category
    {
        public string alias { get; set; }
        public string title { get; set; }
    }

    public class Location
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public List<string> display_address { get; set; }
        public string cross_streets { get; set; }
    }

    public class Coordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Open
    {
        public bool is_overnight { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public int day { get; set; }
    }

    public class Hour
    {
        public List<Open> open { get; set; }
        public string hours_type { get; set; }
        public bool is_open_now { get; set; }
    }

    public class YelpRootObject
    {
        public string id { get; set; }
        public string alias { get; set; }
        public string name { get; set; }
        public string image_url { get; set; }
        public bool is_claimed { get; set; }
        public bool is_closed { get; set; }
        public string url { get; set; }
        public string phone { get; set; }
        public string display_phone { get; set; }
        public int review_count { get; set; }
        public List<Category> categories { get; set; }
        public double rating { get; set; }
        public Location location { get; set; }
        public Coordinates coordinates { get; set; }
        public List<string> photos { get; set; }
        public string price { get; set; }
        public List<Hour> hours { get; set; }
        public List<object> transactions { get; set; }
    }
}
