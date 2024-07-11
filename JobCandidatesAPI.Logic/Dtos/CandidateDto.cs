using System.ComponentModel.DataAnnotations;

namespace Logic.Dtos
{
    public class CandidateDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? CallTime { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? GitHubUrl { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
