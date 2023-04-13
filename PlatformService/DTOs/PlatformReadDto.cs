using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.DTOs
{
    public class PlatformReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public string Cost { get; set; } = null!;
    }
}