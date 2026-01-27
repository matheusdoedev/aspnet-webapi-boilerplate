public interface RepositoryPort<T>
{
	public Task<List<T>> GetAll();
	public Task<T> GetById(string id);
	public Task Save(T entity);
}