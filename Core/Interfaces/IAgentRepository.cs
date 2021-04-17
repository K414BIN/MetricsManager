namespace Core.Interfaces
{
    public interface IAgentRepository<T> where T : class
    {

        T GetAgent(int id);
    }
}