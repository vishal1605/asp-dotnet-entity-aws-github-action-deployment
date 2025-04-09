using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace entity_framework_aws_deployment.Models
{
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;
        
        [Required]
        public string Age { get; set; } = string.Empty;

        public string? CreatedDate { get; set; } = string.Empty;
        public string? UpdatedDate { get; set; } = string.Empty;
    }
}
