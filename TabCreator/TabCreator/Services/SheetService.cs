using TabCreator.Models;
using TabCreator.Repositories.Interfaces;
using TabCreator.Services.Interfaces;

namespace TabCreator.Services
{
    public class SheetService : ISheetService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public SheetService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public void CreateSheet(Sheet sheet)
        {
            _repositoryWrapper.SheetRepository.Create(sheet);
            _repositoryWrapper.Save();
        }

        public void DeleteSheet(Sheet sheet)
        {
            _repositoryWrapper.SheetRepository.Delete(sheet);
            _repositoryWrapper.Save();
        }

        public void UpdateSheet(Sheet sheet)
        {
            _repositoryWrapper.SheetRepository.Update(sheet);
            _repositoryWrapper.Save();
        }

        public Sheet GetSheetById(int id)
        {
            return _repositoryWrapper.SheetRepository.FindByCondition(c => c.SheetId == id).FirstOrDefault()!;
        }

        public List<Sheet> GetSheets()
        {
            return _repositoryWrapper.SheetRepository.FindAll().ToList();
        }
    }
}
