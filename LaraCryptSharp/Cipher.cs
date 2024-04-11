using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace LaraCryptSharp;

public static class Cipher
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="textToEncrypt">The plain text to encrypt.</param>
    /// <param name="key">The Laravel App Key to use to encrypt the payload without the 'base64:'</param>
    /// <returns>Returns an encrypted 'textToEncrypt'.</returns>
    /// <exception cref="ArgumentException">An exception is thrown if the payload is either null or empty.</exception>
    public static string Encrypt(string textToEncrypt, string key)
    {
        if (string.IsNullOrEmpty(textToEncrypt))
            throw new ArgumentException("Plain text cannot be null or empty.", nameof(textToEncrypt));

        var keyBytes = Convert.FromBase64String(key);

        using var aesAlg = Aes.Create();
        
        aesAlg.Key = keyBytes;
        aesAlg.GenerateIV(); // Generates a new IV for each encryption
        aesAlg.Mode = CipherMode.CBC;
        aesAlg.Padding = PaddingMode.PKCS7;

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            // Write all data to the stream.
            swEncrypt.Write(textToEncrypt);
        }

        var ivBase64 = Convert.ToBase64String(aesAlg.IV);
        var encryptedData = msEncrypt.ToArray();
        var encryptedDataBase64 = Convert.ToBase64String(encryptedData);

        // Construct the payload with IV and encrypted data
        var payload = new
        {
            iv = ivBase64,
            value = encryptedDataBase64,
            mac = "" // MAC is left empty, as generating a Laravel-compatible MAC in C# is non-trivial
        };

        var jsonPayload = JsonSerializer.Serialize(payload);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonPayload));
    }
    
    /// <summary>
    /// Decrypts an encrypted payload using AES-256-CBC.
    /// </summary>
    /// <param name="encryptedPayload">The encrypted payload to decrypt.</param>
    /// <param name="key">The Laravel App Key used to encrypt the payload without the 'base64:'</param>
    /// <returns>Return a decrypted string.</returns>
    /// <exception cref="ArgumentException">An exception is thrown if the payload is either null or empty, or if the provided key is invalid.</exception>
    /// <exception cref="InvalidOperationException">An exception is thrown if the payload is invalid or missing necessary data.</exception>
    public static string Decrypt(string encryptedPayload, string key)
    {
        if (string.IsNullOrEmpty(encryptedPayload))
            throw new ArgumentException("Encrypted payload cannot be null or empty.", nameof(encryptedPayload));

        // Base64 decode the encrypted payload
        var payloadBytes = Convert.FromBase64String(encryptedPayload);

        // Deserialize the JSON payload
        var payloadJson = Encoding.UTF8.GetString(payloadBytes);

        var payload = JsonSerializer.Deserialize<Payload>(payloadJson, JsonOptions.DefaultOptions);

        if (payload == null || string.IsNullOrEmpty(payload.Iv) || string.IsNullOrEmpty(payload.Value))
            throw new InvalidOperationException("Payload is invalid or missing necessary data.");

        // Base64 decode the key
        var keyBytes = Convert.FromBase64String(key);

        // Ensure key length is valid for AES-256
        if (keyBytes.Length != 32)
            throw new ArgumentException("Key length is not valid for AES-256. Expected length is 32 bytes.", nameof(key));

        var ivBytes = Convert.FromBase64String(payload.Iv);

        using var aesAlg = Aes.Create();
        
        aesAlg.Key = keyBytes;
        aesAlg.IV = ivBytes;
        aesAlg.Mode = CipherMode.CBC;
        aesAlg.Padding = PaddingMode.PKCS7;

        // Create a decryptor and perform decryption
        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        var cipherBytes = Convert.FromBase64String(payload.Value);
        var plaintextBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        return Encoding.UTF8.GetString(plaintextBytes);
    }
}