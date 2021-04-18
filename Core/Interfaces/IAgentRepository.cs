using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IAgentRepository<T> where T : class
    {
        T GetAgent(int id);
        IList<T> GetAll();
         T GetFirst();
        IList<T> GetLast();
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}