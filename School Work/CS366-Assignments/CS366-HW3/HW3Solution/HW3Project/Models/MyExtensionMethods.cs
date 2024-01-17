
namespace HW3Project.Models {

    public static class MyExtensionMethods {
        public static string SuccessName(this StudentResponse response){
            return $"{response.AssignmentName} for {response.ClassName} has been successfully added to your schedule!";
        }

        public static IEnumerable<StudentResponse> SortByClass(this IEnumerable<StudentResponse> responseEnum){
            var sortedResponses = responseEnum.OrderBy(x => x.ClassName);
            return sortedResponses;
        }

        public static string FormatNames(this StudentResponse response) {
             return $"{response.ClassName}: {response.AssignmentName}";
        }
    }
}