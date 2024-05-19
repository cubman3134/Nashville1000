using Nashville1000Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nashville1000Scraper.BusinessLogic
{
    public abstract class ScrapingBase
    {
        public abstract void ScrapeDataFromSite(RestaurantInfo objectToUpdate, WebDriver driver);
    }
}
