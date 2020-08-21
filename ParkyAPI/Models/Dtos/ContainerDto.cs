using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Models.Dtos
{
    public class ContainerDto
    {
        public int ContainerId { get; set; }
        [Required]
        public string ContainerName { get; set; }
        [Required]
        public int Top { get; set; }
        [Required]
        public int Left { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Background { get; set; }
    }
}
