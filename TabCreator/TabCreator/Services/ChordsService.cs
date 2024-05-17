using TabCreator.Models;
using TabCreator.Repositories.Interfaces;
using TabCreator.Services.Interfaces;

namespace TabCreator.Services
{
	public class ChordsService : IChordsService
	{
		private readonly IRepositoryWrapper _repositoryWrapper;

		public ChordsService(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public void CreateChord(Chords chord)
		{
			_repositoryWrapper.ChordsRepository.Create(chord);
			_repositoryWrapper.Save();
		}

		public void DeleteChord(Chords chord)
		{
			_repositoryWrapper.ChordsRepository.Delete(chord);
			_repositoryWrapper.Save();
		}

		public void UpdateChord(Chords chord)
		{
			_repositoryWrapper.ChordsRepository.Update(chord);
			_repositoryWrapper.Save();
		}

		public Chords GetChordById(int id)
		{
			return _repositoryWrapper.ChordsRepository.FindByCondition(c => c.ChordsId == id).FirstOrDefault()!;
		}

		public List<Chords> GetChords()
		{
			return _repositoryWrapper.ChordsRepository.FindAll().ToList();
		}
	}
}
