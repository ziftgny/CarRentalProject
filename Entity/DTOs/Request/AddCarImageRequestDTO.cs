﻿using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.Request
{
    public class AddCarImageRequestDTO : IDto
    {
        public int CarId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
