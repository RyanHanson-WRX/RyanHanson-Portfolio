namespace HW3Project.Models {
    public class Repository {
        private static List<StudentResponse> responses = new();

        public static IEnumerable<StudentResponse> Responses => responses;

        public static void AddResponse(StudentResponse response) {
            Console.WriteLine(response);
            responses.Add(response);
        }
    }
}