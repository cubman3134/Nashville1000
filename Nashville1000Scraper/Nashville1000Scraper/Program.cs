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
                var informationalElements = driver.FindElements(By.ClassName("wDYxhc"));
                var hoursTable = driver.FindElements(By.ClassName("WgFkxc"));
                foreach (var informationalElement in informationalElements)
                {
                    string data = informationalElement.Text;
                    if (data.StartsWith("Address: "))
                    {

                    }
                    else if (data.StartsWith("Phone: "))
                    {

                    }
                    else if (data.StartsWith("Menu: "))
                    {

                    }
                    else if (data.StartsWith("Price per person: "))
                    {

                    }
                }
                foreach (var hours in hoursTable)
                {
                    string data = hours.Text;
                }
            }
            
        }
    }
}
