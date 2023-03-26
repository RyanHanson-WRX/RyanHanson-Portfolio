using System.ComponentModel.DataAnnotations;

namespace HW4Project.Models{

    public class ColorInterpolation {

        private static List<string> colors = new();

        public static IEnumerable<string> Colors => colors;

        public static void AddColor(string color) {
            colors.Add(color);
        }

        public static void ClearColors() {
            colors.Clear();
        }

        [Required(ErrorMessage = "Please enter valid Hex Value for First Color")]
        [RegularExpression("^#[a-fA-F0-9]{6}$")]
        public string? FirstColor {get; set; }

        [Required(ErrorMessage = "Please enter valid Hex Value for Last Color")]
        [RegularExpression("^#[a-fA-F0-9]{6}$")]
        public string? LastColor { get; set; }

        [Required(ErrorMessage = "Please enter valid number for Number of Colors"), Range(0, 100)]
        public int Number { get; set; }

    }
}