using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabCreator.Models
{
    public class Tablature
    {
        public int TablatureId { get; set; }

		[Required]
		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User? User { get; set; }

		public string? UserTab { get; set; }
    }
}
