using TabCreator.Repositories.Interfaces;

namespace TabCreator.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        ITablatureRepository TablatureRepository { get; }
        ISheetRepository SheetRepository { get; }
        IChordsRepository ChordsRepository { get; }

        void Save();
    }
}
