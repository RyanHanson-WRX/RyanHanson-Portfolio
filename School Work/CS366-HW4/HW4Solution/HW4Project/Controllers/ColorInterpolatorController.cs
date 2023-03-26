using System.Drawing;

namespace HW4Project.Controllers;

public class ColorInterpolator : Controller
{
    [HttpGet]
    public IActionResult Create() {
        return View("Create");
    }
    
    [HttpPost]
    public IActionResult Create(ColorInterpolation c) {
        if (ModelState.IsValid) {
            ColorInterpolation.ClearColors();
            Color ColorOne = ColorExtensionMethods.ToColor(c.FirstColor);
            Color ColorLast = ColorExtensionMethods.ToColor(c.LastColor);
            double ColorOneHue;
            double ColorOneSaturation;
            double ColorOneValue;
            double ColorLastHue;
            double ColorLastSaturation;
            double ColorLastValue;
                
            HSVConvertor.ColorToHSV(ColorOne, out ColorOneHue, out ColorOneSaturation, out ColorOneValue);
            HSVConvertor.ColorToHSV(ColorLast, out ColorLastHue, out ColorLastSaturation, out ColorLastValue);
                

            double HueDiff = ColorLastHue - ColorOneHue;
            double SatDiff = ColorLastSaturation - ColorOneSaturation;
            double ValueDiff = ColorLastValue - ColorOneValue;
            double HueStep;
            double SatStep;
            double ValueStep;

            if(HueDiff != 0) {
                HueStep = HueDiff / (c.Number - 1);
            } else {
                HueStep = 0;
            }
            if (SatDiff != 0) {
                SatStep = SatDiff / (c.Number - 1);
            } else {
                SatStep = 0;
            }
            if (ValueDiff != 0) {
                ValueStep = ValueDiff / (c.Number - 1);
            } else {
                ValueStep = 0;
            }
                
            double[] Hues = new double[c.Number];
            double[] Saturations = new double[c.Number];
            double[] Values = new double[c.Number];
                
            double colorHue = ColorOneHue;
            for (int i = 0; i < c.Number; i++){
                Hues[i] = colorHue;
                colorHue += HueStep;
            }

            double colorSat = ColorOneSaturation;
            for (int i = 0; i < c.Number; i++){
                Saturations[i] = colorSat;
                colorSat += SatStep;
            }

            double colorVal = ColorOneValue;
            for (int i = 0; i < c.Number; i++){
                Values[i] = colorVal;
                colorVal += ValueStep;
            }
            
            for (int i = 0; i < c.Number; i++) {
                Color tempColor = HSVConvertor.ColorFromHSV(Hues[i], Saturations[i], Values[i]);
                string tempHTMLColor = ColorExtensionMethods.ToHtml(tempColor);
                ColorInterpolation.AddColor(tempHTMLColor);
            }
            return View("Create", c); 
        } else {
            return View();
        }
    }

}