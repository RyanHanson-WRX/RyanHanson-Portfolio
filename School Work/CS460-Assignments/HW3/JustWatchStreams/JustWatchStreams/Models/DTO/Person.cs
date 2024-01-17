using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JustWatchStreams.Models.DTO
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        public int JustWatchPersonId { get; set; }

    }
}

namespace JustWatchStreams.ExtensionMethods
{
    public static class PersonExtensions
    {
        public static JustWatchStreams.Models.DTO.Person ToPersonDTO(this JustWatchStreams.Models.Person person)
        {
            return new JustWatchStreams.Models.DTO.Person
            {
                Id = person.Id,
                FullName = person.FullName,
                JustWatchPersonId = person.JustWatchPersonId,
            };
        }

        public static JustWatchStreams.Models.Person ToPerson(this JustWatchStreams.Models.DTO.Person person)
        {
            return new JustWatchStreams.Models.Person
            {
                Id = person.Id,
                FullName = person.FullName,
                JustWatchPersonId = person.JustWatchPersonId
            };
        }
    }
}

