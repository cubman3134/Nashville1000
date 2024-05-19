using Nashville1000DataAccess.Accessors;
using Nashville1000Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nashville1000Scraper.BusinessLogic
{
    public class RestaurantBL
    {
        public static void UpdateRestaurantForCity(RestaurantInfo restaurant)
        {

        }

        public static void UpdateRestaurantForCity(string restaurantName)
        {
            var restaurant = RestaurantInfoDA.GetRestaurantByName(restaurantName);
            if (restaurant == null)
            {
                return;
            }
            UpdateRestaurantForCity(restaurant);
        }
    }
}
