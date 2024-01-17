using System.ComponentModel.DataAnnotations;

namespace HW4Project.Models{

    public class RGBInput {

        public RGBInput () {
            Red = 0;
            Green = 0;
            Blue = 0;
        }

        [Required(ErrorMessage = "Please enter valid Red value (0-255)"), Range(0,255)]
        public int? Red {get; set; }

        [Required(ErrorMessage = "Please enter valid Green value (0-255)"), Range(0,255)]
        public int? Green { get; set; }

        [Required(ErrorMessage = "Please enter valid Blue value (0-255)"), Range(0,255)]
        public int? Blue { get; set; }

    }
}