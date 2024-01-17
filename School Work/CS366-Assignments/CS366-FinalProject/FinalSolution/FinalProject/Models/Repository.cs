namespace FinalProject.Models {
    public class Repository {

        // Uses C# Properties
        private static List<Company> companies = new();

        private static List<Review> reviews = new();

        public static IEnumerable<Company> Companies => companies;

        public static IEnumerable<Review> Reviews => reviews;

        public static void AddCompany(Company company) {
            companies.Add(company);
        }

        public static void AddReview(Review review) {
            reviews.Add(review);
        }

        public static int CompanyCount () {
            int companyCount = 0;
            foreach (Company c in Companies) {
                companyCount += 1;
            }
            return companyCount;
        }
        
        public static int ReviewCount () {
            int reviewCount = 0;
            foreach (Review r in Reviews) {
                reviewCount += 1;
            }
            return reviewCount;
        }

        public static void CalculateStars () {
            foreach (Company c in Companies) {
                double stars = 0;
                double count = 0;
                foreach (Review r in Reviews) {
                    if (r.CompanyName == c.Name) {
                        count += 1;
                        stars += r.StarRating;
                    } else {
                        continue;
                    }
                }
                if (stars != 0 && count != 0) {
                    c.Stars = Convert.ToDouble(string.Format("{0:F2}",(stars/count)));
                } else {
                    c.Stars = 0;
                }
            }
        }
    }
}