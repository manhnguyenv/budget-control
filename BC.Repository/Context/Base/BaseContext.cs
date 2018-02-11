using BC.Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BC.Repository.Context
{
    public class BaseContext<T> : DbContext where T : class
    {
        public DbSet<T> DbSet { get; set; }

        public BaseContext() : base("bd")
        {
            Database.SetInitializer<BaseContext<T>>(new DropCreateDatabaseAlways<BaseContext<T>>());
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToMapping = (from x in Assembly.GetExecutingAssembly().GetTypes()
                                  where x.IsClass && typeof(IConfiguration).IsAssignableFrom(x)
                                  select x).ToList();

            foreach (var mapping in typesToMapping)
            {
                dynamic mappingClass = Activator.CreateInstance(mapping);
                modelBuilder.Configurations.Add(mappingClass);
            }
        }

        public virtual void ChangeObjectState(object model, EntityState state)
        {
            ((IObjectContextAdapter)this)
                          .ObjectContext
                          .ObjectStateManager
                          .ChangeObjectState(model, state);
        }

        public virtual int Save(T model)
        {
            DbSet.Add(model);
            return SaveChanges();
        }

        public virtual int Update(T model)
        {
            var entry = Entry(model);
            if (entry.State == EntityState.Detached)
                DbSet.Attach(model);

            ChangeObjectState(model, EntityState.Modified);
            return SaveChanges();
        }

        public virtual void Delete(T model)
        {
            var entry = Entry(model);
            if (entry.State == EntityState.Detached)
                DbSet.Attach(model);

            ChangeObjectState(model, EntityState.Deleted);
            SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<T> Where(Expression<Func<T, bool>> expression, params string[] navigationProperties)
        {
            return Include(navigationProperties).Where(expression);
        }

        private IQueryable<T> Include(params string[] navigationProperties)
        {
            var query = DbSet.AsQueryable();
            foreach(var property in navigationProperties)
            {
                query.Include(property);
            }
            return query;
        }

    }
}
