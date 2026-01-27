public interface EncryptorPort
{
	public byte[] Encrypt(string plainText);
	public string Decrypt(byte[] cipherText);
}