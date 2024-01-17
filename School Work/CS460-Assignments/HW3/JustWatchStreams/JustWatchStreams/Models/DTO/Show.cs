using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustWatchStreams.Models.DTO
{
    public class Show
    {
        public int Id { get; set; }

        [StringLength(12)]
        public string JustWatchShowId { get; set; } = null!;

        [StringLength(128)]
        public string Title { get; set; } = null!;

        public string Description { get; set; }
        public int ShowTypeId { get; set; }

        public int ReleaseYear { get; set; }

        public int? AgeCertificationId { get; set; }

        public int Runtime { get; set; }

        public int? Seasons { get; set; }

        [StringLength(12)]
        public string ImdbId { get; set; }

        public double? ImdbScore { get; set; }

        public double? ImdbVotes { get; set; }

        public double? TmdbPopularity { get; set; }

        public double? TmdbScore { get; set; }

        [StringLength(20)]
        public string CertificationIdentifier { get; set; } = null!;

        [StringLength(100)]
        public string Genres { get; set; } = null!;

        [StringLength(20)]
        public string ShowTypeIdentifier { get; set; } = null!;
        [StringLength(50)]
        public string DirectorName { get; set; } = null!;
    }
}

namespace JustWatchStreams.ExtensionMethods
{
    public static class ShowExtensions
    {
        public static JustWatchStreams.Models.DTO.Show ToShowDTO(this JustWatchStreams.Models.Show show)
        {
            return new JustWatchStreams.Models.DTO.Show
            {
                Id = show.Id,
                JustWatchShowId = show.JustWatchShowId,
                Title = show.Title,
                Description = show.Description,
                ShowTypeId = show.ShowTypeId,
                ReleaseYear = show.ReleaseYear,
                AgeCertificationId = show.AgeCertificationId,
                Runtime = show.Runtime,
                Seasons = show.Seasons,
                ImdbId = show.ImdbId,
                ImdbScore = show.ImdbScore,
                ImdbVotes = show.ImdbVotes,
                TmdbPopularity = show.TmdbPopularity,
                TmdbScore = show.TmdbScore,
                CertificationIdentifier = show.AgeCertification?.CertificationIdentifier,
                Genres = string.Join(", ", show.GenreAssignments.Select(ga => ga.Genre.GenreString)),
                ShowTypeIdentifier = show.ShowType.ShowTypeIdentifier,
                DirectorName = string.IsNullOrEmpty(string.Join(", ", show.Credits.Select(c => new {Director = c.Person.FullName, Role = c.Role.RoleName}).Where(c => c.Role == "DIRECTOR").Select(c => c.Director))) ? "N/A" : string.Join(", ", show.Credits.Select(c => new {Director = c.Person.FullName, Role = c.Role.RoleName}).Where(c => c.Role == "DIRECTOR").Select(c => c.Director))
            };
        }

        public static JustWatchStreams.Models.Show ToShow(this JustWatchStreams.Models.DTO.Show show)
        {
            return new JustWatchStreams.Models.Show
            {
                Id = show.Id,
                JustWatchShowId = show.JustWatchShowId,
                Title = show.Title,
                Description = show.Description,
                ShowTypeId = show.ShowTypeId,
                ReleaseYear = show.ReleaseYear,
                AgeCertificationId = show.AgeCertificationId,
                Runtime = show.Runtime,
                Seasons = show.Seasons,
                ImdbId = show.ImdbId,
                ImdbScore = show.ImdbScore,
                ImdbVotes = show.ImdbVotes,
                TmdbPopularity = show.TmdbPopularity,
                TmdbScore = show.TmdbScore
            };
        }
    }
}
