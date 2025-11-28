
using Microsoft.EntityFrameworkCore;

public class UserRepositoryAdapter(AppDbContext context) : UserRepositoryPort
{
	private readonly AppDbContext _context = context;

	public async Task<List<User>> GetAll()
	{
		return await _context.Users.ToListAsync();
	}

	public async Task<User> GetById(string id)
	{
		User? user = await _context.Users.FindAsync(id) ?? throw new Exception($"User with id {id} not found");

		return user;
	}

	public Task<User?> GetUserByUsername(string username)
	{
		return _context.Users.FirstOrDefaultAsync(u => u.Username == username);
	}

	public async Task Save(User entity)
	{
		await _context.Users.AddAsync(entity);
		await _context.SaveChangesAsync();
	}
}