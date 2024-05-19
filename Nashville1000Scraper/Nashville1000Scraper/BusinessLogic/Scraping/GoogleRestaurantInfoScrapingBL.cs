using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nashville1000Models;
using Nashville1000Models.Restaurant;

namespace Nashville1000Scraper.BusinessLogic
{
    public class GoogleRestaurantInfoScrapingBL : ScrapingBase
    {
        public override void ScrapeDataFromSite(RestaurantInfo restaurant, WebDriver driver)
        {
            string searchableRestaurantName = restaurant.RestaurantName;
            driver.Navigate().GoToUrl("https://google.com");
            var element = driver.FindElement(By.Id("APjFqb"));
            string restaurantNameToSearch = restaurant.RestaurantName;
            if (!searchableRestaurantName.ToLower().Contains(restaurant.RestaurantCity))
            {
                restaurant.RestaurantName += $" {restaurant.RestaurantCity}";
            }
            element.SendKeys(restaurant.RestaurantName);
            WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            element.Submit();
            Thread.Sleep(3000);
            List<SearchableTextElementInfo> searchableTextElements = new List<SearchableTextElementInfo>()
            {
                new SearchableTextElementInfo(Constants.GoogleScoreClass, nameof(RestaurantInfo.GoogleFoodScore)),
                new SearchableTextElementInfo(Constants.RestaurantNameClass, nameof(RestaurantInfo.RestaurantName)),
                new SearchableTextElementInfo(Constants.RestaurantTypeClass, nameof(RestaurantInfo.FoodTypeString)),

            };
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
            if (!string.IsNullOrWhiteSpace(searchTextToValue[Constants.AddressSearch]))
            {

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
