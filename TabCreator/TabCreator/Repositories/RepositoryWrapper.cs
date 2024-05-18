using TabCreator.Models;
using TabCreator.Repositories.Interfaces;

namespace TabCreator.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TabCreatorContext _tabCreatorContext;

        private ITablatureRepository? _tablatureRepository;
        private ISheetRepository? _sheetRepository;
        private IChordsRepository? _chordsRepository;
        public ITablatureRepository TablatureRepository
        {
            get
            {
                if (_tablatureRepository == null)
                {
                    _tablatureRepository = new TablatureRepository(_tabCreatorContext);
                }

                return _tablatureRepository;
            }
        }

        public ISheetRepository SheetRepository
        {
            get
            {
                if (_sheetRepository == null)
                {
                    _sheetRepository = new SheetRepository(_tabCreatorContext);
                }

                return _sheetRepository;
            }
        }

        public IChordsRepository ChordsRepository
        {
            get
            {
                if (_chordsRepository == null)
                {
                    _chordsRepository = new ChordsRepository(_tabCreatorContext);
                }

                return _chordsRepository;
            }
        }

        public RepositoryWrapper(TabCreatorContext tabCreatorContext)
        {
            _tabCreatorContext = tabCreatorContext;
        }

        public void Save()
        {
            _tabCreatorContext.SaveChanges();
        }
    }
}
