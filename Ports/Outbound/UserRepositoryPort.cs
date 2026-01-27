public interface UserRepositoryPort : RepositoryPort<User>
{
	public Task<User?> GetUserByUsername(string username);
}