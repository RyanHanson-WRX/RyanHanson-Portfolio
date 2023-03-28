namespace DbProject.Web.Models {
    public class Repository {
        private static List<Company> companies = new();

        private static List<Subscription> subscriptions = new();
        
        private static List<CompanyFormatted> formatcompanies = new();

        public static IEnumerable<Company> Companies => companies;

        public static IEnumerable<CompanyFormatted> FormatCompanies => formatcompanies;

        public static IEnumerable<Subscription> Subscriptions => subscriptions;

        public static void AddCompany(Company company) {
            companies.Add(company);
        }

        public static void AddSub(Subscription subscription) {
            subscriptions.Add(subscription);
        }

        public static void AddFormatCompany(CompanyFormatted formatcompany) {
            formatcompanies.Add(formatcompany);
        }

        public static void Clear() {
            formatcompanies.Clear();
            companies.Clear();
            subscriptions.Clear();
        }
    }
}