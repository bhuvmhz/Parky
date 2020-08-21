using System;
using System.ComponentModel.DataAnnotations;

namespace ParkyWeb.Models
{
    public class Container
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

        public DateTime DateCreated { get; set; }
    }
}
