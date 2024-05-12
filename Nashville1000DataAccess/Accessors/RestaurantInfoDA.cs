using Nashville1000Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nashville1000DataAccess.Accessors
{
    public class RestaurantInfoDA
    {
        public static bool InsertRecord(RestaurantInfo restaurantData)
        {
            var ignorableProperties = new List<string>()
            {
                nameof(restaurantData.RestaurantId)
            };
            return DatabaseInfo.Instance.UpdateRecord("pp_RIInsertRecord", restaurantData, ignorableProperties);
        }

        public static List<RestaurantInfo> GetAllRestaurants()
        {
            DatabaseInfo.Instance.ReadRecords<RestaurantInfo>("pp_RIReadAll", new List<System.Data.SqlClient.SqlParameter>(), out var response);
            return response;
        }
    }
}
