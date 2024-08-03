﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class CarDetailsDTO:IDto
    {
        public int Id {  get; set; }
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public string DailyPrice { get; set; }

    }
}
