using TabCreator.Models;

namespace TabCreator.Services.Interfaces
{
	public interface IChordsService
	{
		void CreateChord(Chords chord);

		void DeleteChord(Chords chord);

		void UpdateChord(Chords chord);

		Chords GetChordById(int id);

		List<Chords> GetChords();
	}
}
