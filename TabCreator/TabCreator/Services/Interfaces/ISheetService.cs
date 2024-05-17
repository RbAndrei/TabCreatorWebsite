using TabCreator.Models;

namespace TabCreator.Services.Interfaces
{
    public interface ISheetService
    {
        void CreateSheet(Sheet sheet);

        void DeleteSheet(Sheet sheet);

        void UpdateSheet(Sheet sheet);

        Sheet GetSheetById(int id);

        List<Sheet> GetSheets();
    }
}

