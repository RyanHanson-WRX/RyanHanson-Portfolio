using System.Net;
using System.Globalization;

namespace HW4.Utilities
{
    public static class FormatMethods
    {
        public static string FormatMovieRevenue(long revenue) 
        {
            if (revenue == 0) {
                return "N/A";
            }
            return revenue.ToString("C0", CultureInfo.CurrentCulture);
        }
        public static string FormatMovieReleaseDate(string releaseDate) 
        {
            if (releaseDate == null || releaseDate == "") {
                return "N/A";
            }
            return DateTime.Parse(releaseDate).ToString("MMMM dd, yyyy");
        }

        public static string FormatMovieRuntime(int runtime) 
        {   
            if (runtime == 0) {
                return "N/A";
            }
            else if (runtime < 60)
            {
                return $"{runtime} minutes";
            }
            else
            {
                int hours = runtime / 60;
                int minutes = runtime % 60;
                if (hours == 1 && minutes == 0)
                {
                    return $"{hours} hour";
                }
                else if (hours == 1 && minutes > 0)
                {
                    if (minutes == 1)
                    {
                        return $"{hours} hour {minutes} minute";
                    }
                    else
                    {
                        return $"{hours} hour {minutes} minutes";
                    }
                }
                else if (hours > 1 && minutes == 0)
                {
                    return $"{hours} hours";
                }
                else // case when hours > 1 and minutes > 0
                {
                    if (minutes == 1)
                    {
                        return $"{hours} hours {minutes} minute";
                    }
                    else
                    {
                        return $"{hours} hours {minutes} minutes";
                    }
                }
            }
        }
    }
}