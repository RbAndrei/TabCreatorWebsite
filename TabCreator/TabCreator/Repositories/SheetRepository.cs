using TabCreator.Models;
using TabCreator.Repositories.Interfaces;

namespace TabCreator.Repositories
{
    public class SheetRepository : RepositoryBase<Sheet>, ISheetRepository
    {
        public SheetRepository(TabCreatorContext tabCreatorContext)
            : base(tabCreatorContext)
        {
        }
    }
}
