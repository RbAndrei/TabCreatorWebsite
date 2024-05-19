using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabCreator.Models
{
    public class Sheet
    {
        public int SheetId { get; set; }

        [Required]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser? User { get; set; }

        public string? SheetName { get; set; }

        public string? UserSheet { get; set; }
    }
}
