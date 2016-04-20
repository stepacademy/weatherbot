﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Database.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public City City { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
