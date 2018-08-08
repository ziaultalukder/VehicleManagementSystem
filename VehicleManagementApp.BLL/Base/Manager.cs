using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementApp.Repository.Contracts;
using VehicleManagementApp.Repository.Repository;

namespace VehicleManagementApp.BLL.Base
{
    public abstract class Manager<T> where T:class
    {
        private Repository<T> _repository;
        public Manager(Repository<T> repository )
        {
            _repository = repository;
        }

        public virtual bool Add(T entity)
        {
            return _repository.Add(entity);
        }

        public virtual bool Update(T entity)
        {
            return _repository.Update(entity);
        }

        public virtual bool Remove(IDeletable entity)
        {
            bool IsDeletable = entity is IDeletable;
            if (!IsDeletable)
            {
                throw new Exception("This Item Is Not Deletable");
            }
            return _repository.Remove((IDeletable) entity);
        }

        public virtual bool Remove(ICollection<IDeletable> entites)
        {
            return _repository.Remove(entites);
        }

        public virtual T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual ICollection<T> GetAll(bool withDeleted = false)
        {
            return _repository.GetAll(withDeleted);
        }
        
    }
}
