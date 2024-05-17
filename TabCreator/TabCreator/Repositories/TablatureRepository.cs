using TabCreator.Models;
using TabCreator.Repositories.Interfaces;

namespace TabCreator.Repositories
{
    public class TablatureRepository : RepositoryBase<Tablature>, ITablatureRepository
    {
        public TablatureRepository(TabCreatorContext tabCreatorContext)
            : base(tabCreatorContext)
        {
        }
    }
}
