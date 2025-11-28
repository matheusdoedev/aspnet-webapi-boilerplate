using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class AuthenticationAdapter(UserRepositoryPort userRepository, TokenizerPort tokenizer, EncryptorPort encryptor) : AuthenticationPort
{
	private readonly UserRepositoryPort _userRepository = userRepository;
	private readonly TokenizerPort _tokenizer = tokenizer;
	private readonly EncryptorPort _encryptor = encryptor;

	public async Task<LoginResponseDto> Login(LoginDto loginDto)
	{
		User? user = await GetUser(loginDto.UserName) ?? throw new ArgumentException("User with these credentials already exists.");

		// verify if password is valid

		// generate jwt token

		// return token
	}

	public async Task<SignUpResponseDto> SignUp(SignUpDto signUpDto)
	{
		User? user = await GetUser(signUpDto.UserName) ?? throw new ArgumentException("User with these credentials already exists.");
		bool isPasswordValid = CheckIfPasswordIsValid(signUpDto.Password, user.PasswordHash);

		if (!isPasswordValid)
		{
			throw new ArgumentException("Password is not valid.");
		}
		await SaveAccount(signUpDto);

		string token = GenerateToken(signUpDto);
		SignUpResponseDto response = new()
		{
			AccessToken = token
		};

		return response;
	}

	private async Task<User?> GetUser(string username)
	{
		return await _userRepository.GetUserByUsername(username);
	}

	private bool CheckIfPasswordIsValid(string password, byte[] userPassword)
	{
		string decryptedUserPassword = DecryptPassword(userPassword);

		return decryptedUserPassword.Equals(password);
	}

	private byte[] EncryptPassword(string password)
	{
		return _encryptor.Encrypt(password);
	}

	private string DecryptPassword(byte[] encryptedPassword)
	{
		return _encryptor.Decrypt(encryptedPassword);
	}

	private async Task SaveAccount(SignUpDto signUpDto)
	{
		User user = new()
		{
			Email = signUpDto.Email,
			Id = Guid.NewGuid().ToString(),
			PasswordHash = EncryptPassword(signUpDto.Password),
			Username = signUpDto.UserName
		};

		await _userRepository.Save(user);
	}

	private string GenerateToken(SignUpDto signUpDto)
	{
		Dictionary<string, string> claims = new()
		{
			{ "username", signUpDto.UserName },
			{ "email", signUpDto.Email }
		};

		return _tokenizer.GenerateToken(claims);
	}
}