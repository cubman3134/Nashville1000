﻿using Nashville1000CommonData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nashville1000Models
{
    public class RestaurantInfo : ModelBase
    {
        public long RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public FoodTypes FoodType { get; set; }
        public decimal PricePerPersonMinimum { get; set; }
        public decimal PricePerPersonMaximum { get; set; }
        public decimal GoogleFoodScore { get; set; }
        public NashvilleNeighborhoods NashvilleNeighborhood { get; set; }
        public string Description { get; set; }
    }
}
