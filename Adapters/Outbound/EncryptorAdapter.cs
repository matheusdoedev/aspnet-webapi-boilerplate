using System.Security.Cryptography;

public class EncryptorAdapter : EncryptorPort
{
	private readonly Aes _aes;
	private readonly ICryptoTransform _encryptor;
	private readonly ICryptoTransform _decryptor;

	public EncryptorAdapter()
	{
		string encryptKey = Environment.GetEnvironmentVariable("ENCRYPT_KEY") ?? throw new InvalidOperationException("ENCRYPT_KEY environment variable is not set.");
		string encryptIV = Environment.GetEnvironmentVariable("ENCRYPT_IV") ?? throw new InvalidOperationException("ENCRYPT_IV environment variable is not set.");

		_aes = Aes.Create();
		_aes.Key = Convert.FromBase64String(encryptKey);
		_aes.IV = Convert.FromBase64String(encryptIV);
		_encryptor = _aes.CreateEncryptor(Convert.FromBase64String(encryptKey), Convert.FromBase64String(encryptIV));
		_decryptor = _aes.CreateDecryptor(Convert.FromBase64String(encryptKey), Convert.FromBase64String(encryptIV));
	}

	public string Decrypt(byte[] cipherText)
	{
		using MemoryStream ms = new(cipherText);
		using CryptoStream cs = new(ms, _decryptor, CryptoStreamMode.Read);
		using StreamReader sr = new(cs);

		return sr.ReadToEnd();
	}

	public byte[] Encrypt(string plainText)
	{
		using MemoryStream ms = new();
		using CryptoStream cs = new(ms, _encryptor, CryptoStreamMode.Write);
		using StreamWriter sw = new(cs);

		sw.Write(plainText);
		return ms.ToArray();
	}
}