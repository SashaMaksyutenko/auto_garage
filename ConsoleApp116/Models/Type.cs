using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp116.Models
{
    class TypeAuto
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name type is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters long (Max - 50).")]
        public string Name { get; set; }

        public TypeAuto(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
