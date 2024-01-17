using System.Drawing;

namespace HW4Project.Models {

    public static class ColorExtensionMethods {
        public static string ToHtml (Color color) {
            return ColorTranslator.ToHtml(color);
        }
        public static Color ToColor (string htmlvalue) {
            return ColorTranslator.FromHtml(htmlvalue);
        }
    }
}