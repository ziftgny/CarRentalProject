using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.Response
{
    public class OpenImageResponseDTO : IDto
    {
        public string ImageType { get; set; }
        public FileStream FileStream { get; set; }
    }
}
