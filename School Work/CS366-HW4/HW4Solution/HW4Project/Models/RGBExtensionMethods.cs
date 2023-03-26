
namespace HW4Project.Models {

    public static class RGBExtensionMethods {
        public static string HexValue (this RGBInput rgbcolor) {
            return string.Format($"{rgbcolor.Red:X2}{rgbcolor.Green:X2}{rgbcolor.Blue:X2}");
        }
    }
}