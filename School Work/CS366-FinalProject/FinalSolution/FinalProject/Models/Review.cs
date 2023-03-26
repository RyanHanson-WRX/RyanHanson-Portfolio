using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models{

    // Uses C# Properties
    // Project has a unique controller, with a defined model and view
    public class Review {

        [Required(ErrorMessage = "Please enter the company's name.")]
        public string? CompanyName {get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        public string? UserName {get; set; }

        [Required(ErrorMessage = "Please enter the amount of stars you rate this company on a scale of 0 to 5."), Range(0,5)]
        public int StarRating { get; set; }

        [Required(ErrorMessage = "Please enter if you would do business with company again.")]
        public string? Revisit { get; set; }

        [Required(ErrorMessage = "Please enter your review of your experience with this company.")]
        public string? ReviewResponse { get; set; }
    }
}