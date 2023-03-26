using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models{

    // Uses C# Properties

    public class Company {

        public Company () {
            Name = " ";
            Address = " ";
            OpenTime = " ";
            CloseTime = " ";
            DaysOpen = " ";
        }

        [Required(ErrorMessage = "Please enter the name of the Company.")]
        public string? Name {get; set; }

        [Required(ErrorMessage = "Please enter the address of the Company.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Please enter what time the Company opens.")]
        public string? OpenTime { get; set; }

        [Required(ErrorMessage = "Please enter what time the Company closes.")]
        public string? CloseTime { get; set; }

        [Required(ErrorMessage = "Please enter what days of the week the Company is open. Ex: Mon,Tues,Weds,Thurs,Fri")]
        public string? DaysOpen { get; set; }

        public double? Stars {get; set;}
    }
}