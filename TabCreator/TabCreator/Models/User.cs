namespace TabCreator.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public virtual ICollection<Tablature>? Tablatures { get; set; }

        public virtual ICollection<Sheet>? Sheet { get; set; }

        public virtual ICollection<Chords>? Chords { get; set; }
    }
}
