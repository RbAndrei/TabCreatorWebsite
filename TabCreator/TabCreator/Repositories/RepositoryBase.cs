using TabCreator.Models;
using TabCreator.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TabCreator.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected TabCreatorContext TabCreatorContext { get; set; }

        public RepositoryBase(TabCreatorContext tabCreatorContext)
        {
            this.TabCreatorContext = tabCreatorContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.TabCreatorContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.TabCreatorContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.TabCreatorContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.TabCreatorContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.TabCreatorContext.Set<T>().Remove(entity);
        }
    }
}
