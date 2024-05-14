using Nashville1000DataAccess.Accessors;
using Nashville1000Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nashville1000Scraper
{
    internal class Program
    {
        public static void ReadRestaurantNamesFromFile()
        {
            List<string> restaurants = File.ReadLines("C:\\Users\\cubma\\Desktop\\Stuff\\RestaurantNames.txt").ToList();
            foreach (var restaurantName in restaurants)
            {
                RestaurantInfoDA.InsertRecord(new RestaurantInfo() { RestaurantName = restaurantName.Trim() });
            }
        }

        static void Main(string[] args)
        {
            //ReadRestaurantNamesFromFile();
            var restaurants = RestaurantInfoDA.GetAllRestaurants();
            var driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 5);
            foreach (var restaurant in restaurants)
            {
                driver.Navigate().GoToUrl("https://google.com");
                var element = driver.FindElement(By.Id("APjFqb"));
                if (!restaurant.RestaurantName.ToLower().Contains("nashville"))
                {
                    restaurant.RestaurantName += " Nashville";
                }
                element.SendKeys(restaurant.RestaurantName);
                WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                element.Submit();
                Thread.Sleep(3000);
                string googleScore = driver.FindElement(By.ClassName(Constants.GoogleScoreClass)).Text;
                string restaurantName = driver.FindElement(By.ClassName(Constants.RestaurantNameClass)).Text;
                string restaurantType = driver.FindElement(By.ClassName(Constants.RestaurantTypeClass)).Text;
                var informationalElements = driver.FindElements(By.ClassName("wDYxhc"));
                Dictionary<string, string> searchTextToValue = new Dictionary<string, string>()
                {
                    { Constants.AddressSearch, string.Empty },
                    { Constants.MenuSearch, string.Empty },
                    { Constants.PhoneSearch, string.Empty },
                    { Constants.PricePerPerson, string.Empty },
                };
                List<string> buttonClassesToPress = new List<string>()
                {
                    Constants.MoreDescriptionButtonClass,
                    Constants.MoreHoursButtonClass
                };
                foreach (var informationalElement in informationalElements)
                {
                    string data = informationalElement.Text;
                    var match = searchTextToValue.FirstOrDefault(x => data.StartsWith(x.Key));
                    if (match.Key == null)
                    {
                        continue;
                    }
                    searchTextToValue[match.Key] = data.Substring(match.Key.Length);
                }
                foreach (var buttonClass in buttonClassesToPress)
                {
                    var buttonElement = driver.FindElement(By.ClassName(buttonClass));
                    if (buttonElement != null)
                    {
                        buttonElement.Click();
                    }
                }
                var hoursTable = driver.FindElement(By.ClassName("WgFkxc"));
                var descriptionTextClass = driver.FindElements(By.ClassName("sATSHe"));
                string description = string.Empty;
                foreach (var descriptionText in descriptionTextClass)
                {
                    var text = descriptionText.Text;
                    if (text.StartsWith($"From {restaurantName}"))
                    {
                        text = text.Split('\n')[1];
                        description = text.Trim('\"');
                    }
                }
            }
            
        }
    }
}
