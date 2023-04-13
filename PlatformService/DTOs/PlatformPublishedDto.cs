using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.DTOs
{
    public class PlatformPublishedDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Event { get; set; } = null!;

    }
}