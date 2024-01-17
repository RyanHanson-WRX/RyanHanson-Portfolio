using System.ComponentModel.DataAnnotations;

namespace HW3Project.Models{

    public class StudentResponse : IResponses {

        public static int responseCount = 0;
        public StudentResponse() {
            responseCount = responseCount + 1;
        }

        [Required(ErrorMessage = "Please enter the class name")]
        public string? ClassName {get; set; }

        [Required(ErrorMessage = "Please enter your assignment name")]
        public string? AssignmentName { get; set; }

        [Required(ErrorMessage = "Please enter the day of the week that you plan to work on the assignment")]
        public string? Day { get; set; }

        [Required(ErrorMessage = "Please enter the due date for this assignment")]
        public string? DueDate { get; set; }

        public int ResponseCount() {
            return responseCount;
        }
    }
}