namespace DataTable.Interface
{
    public interface IGenericService<T> where T : class
    {
        string Add(T entity);
        T Get(int id);
        List<T> GetAll();
        string Update(int id, T entity);
        string Delete(int id);
    }

}
