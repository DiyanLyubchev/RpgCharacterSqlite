using System.Linq.Expressions;

namespace RpgCharacterSqlite.Repository.SqlLiteWithMigration.Repository
{
    internal interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity InsertWithSave(TEntity entity);
        TEntity GetById(object id);
        void Delete(object id);
        public void DeleteAll();
    }
}