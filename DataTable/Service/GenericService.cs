using DataTable.Data;
using DataTable.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataTable.Service
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericService(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public string Add(T entity)
        {
            if (Exists(entity))
                return "Entity with same ID or Name already exists!";

            _dbSet.Add(entity);
            _context.SaveChanges();
            return "Entity added successfully.";
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public string Update(int id, T entity)
        {
            var existingEntity = _dbSet.Find(id);
            if (existingEntity == null)
                return "Entity not found!";

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return "Entity updated successfully.";
        }

        public string Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
                return "Entity not found!";

            _dbSet.Remove(entity);
            _context.SaveChanges();
            return "Entity deleted successfully.";
        }


        private int GetEntityId(T entity)
        {
            var idProperty = entity.GetType().GetProperty("Id");
            if (idProperty != null)
                return (int)idProperty.GetValue(entity);
            return 0;
        }

        private bool Exists(T entity)
        {
            var id = GetEntityId(entity);

            var nameProperty = entity.GetType().GetProperty("Name");
            var name = nameProperty != null ? (string)nameProperty.GetValue(entity) : null;

            var entities = _dbSet.ToList(); // Database থেকে মেমরিতে নিয়ে আসা

            return entities.Any(e =>
                GetEntityId(e) == id ||
                (name != null && nameProperty != null && (string)nameProperty.GetValue(e) == name)
            );
        }

    }
}
