using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.DTOs
{
    public class PlatformCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Publisher { get; set; } = null!;

        [Required]
        public string Cost { get; set; } = null!;
    }
}