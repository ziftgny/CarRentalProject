using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.Request
{
    public class UpdateCarImageRequestDTO:IDto
    {
        public int CarImageId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
