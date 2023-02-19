using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp116.Models
{
    class Auto
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Car number is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters long (Max - 50).")]
        public string Number { get; set; }

        
        public int ID_Color { get; set; }
        [Required(ErrorMessage = "Car color is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters long (Max - 50).")]
        [ForeignKey("ID_Color")]
        public ColorAuto Color { get; set; }

        public int ID_Type { get; set; }
        [Required(ErrorMessage = "type of a car is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters long (Max - 50).")]
        [ForeignKey("ID_Type")]
        public TypeAuto Type { get; set; }

        public int ID_Model { get; set; }
        [Required(ErrorMessage = "model of a car is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters long (Max - 50).")]
        [ForeignKey("ID_Model")]
        public ModelAuto Model{ get; set; }

        public Auto(string number, int id_color, int id_type, int id_model)
        {
            Number = number;
            ID_Color = id_color;
            ID_Type = id_type;
            ID_Model = id_model;
        }
        public Auto()
        {
            Number = null;
            ID_Color = -1;
            ID_Type = -1;
            ID_Model = -1;
        }

        public override string ToString()
        {
            return $"{this.Number} {this.Color.Name} {this.Model.Name} {this.Type.Name}";
        }
    }
}
