public interface AuthenticationPort
{
	public Task<LoginResponseDto> Login(LoginDto loginDto);
	public Task<SignUpResponseDto> SignUp(SignUpDto signUpDto);
}