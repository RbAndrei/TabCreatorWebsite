using TabCreator.Models;
using TabCreator.Repositories.Interfaces;
using TabCreator.Services.Interfaces;

namespace TabCreator.Services
{
    public class TablatureService : ITablatureService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public TablatureService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public void CreateTablature(Tablature tablature)
        {
            _repositoryWrapper.TablatureRepository.Create(tablature);
            _repositoryWrapper.Save();
        }

        public void DeleteTablature(Tablature tablature)
        {
            _repositoryWrapper.TablatureRepository.Delete(tablature);
            _repositoryWrapper.Save();
        }

        public void UpdateTablature(Tablature tablature)
        {
            _repositoryWrapper.TablatureRepository.Update(tablature);
            _repositoryWrapper.Save();
        }

        public Tablature GetTablatureById(int id)
        {
            return _repositoryWrapper.TablatureRepository.FindByCondition(c => c.TablatureId == id).FirstOrDefault()!;
        }

        public List<Tablature> GetTablatures()
        {
            return _repositoryWrapper.TablatureRepository.FindAll().ToList();
        }
    }
}
