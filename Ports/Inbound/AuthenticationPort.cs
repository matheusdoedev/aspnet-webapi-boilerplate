public interface AuthenticationPort
{
	public LoginResponseDto Login(LoginDto loginDto);
	public SignUpResponseDto SignUp(SignUpDto signUpDto);
}