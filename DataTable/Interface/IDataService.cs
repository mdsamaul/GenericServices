namespace DataTable.Interface
{
    public interface IDataService
    {
        Task<List<T>> GetDataAsync<T>() where T : class;
    }
}
