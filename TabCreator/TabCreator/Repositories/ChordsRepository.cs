using TabCreator.Models;
using TabCreator.Repositories.Interfaces;

namespace TabCreator.Repositories
{
    public class ChordsRepository : RepositoryBase<Chords>, IChordsRepository
    {
        public ChordsRepository(TabCreatorContext tabCreatorContext)
            : base(tabCreatorContext)
        {
        }
    }
}
