using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabCreator.Models
{
    public class Tablature
    {
        public int TablatureId { get; set; }

		[Required]
		public string? UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual IdentityUser? User { get; set; }

		public string? TablatureName { get; set; }

		public string? UserTab { get; set; }
    }
}
