namespace LaraCryptSharp.Tests;
using Xunit;
using LaraCryptSharp;

public class Tests
{
    [Fact]
    public void Encrypt_ShouldReturnNonEmptyString_WhenPlainTextIsProvided()
    {
        // Arrange
        const string plainText = "Hello World!";
        const string key = "OXZjemFjZXFtODVnZ204d2NwbHRtOWhpc2l3dzd6ZDM=";
        
        // Act
        var encryptedText = Cipher.Encrypt(plainText, key);

        // Assert
        Assert.False(string.IsNullOrEmpty(encryptedText));
    }

    [Fact]
    public void Decrypt_ShouldReturnOriginalText_WhenEncryptedTextIsProvided()
    {
        // Arrange
        const string originalText = "eyJpdiI6Im9sTndScmpQS0FVSWdlRUZYN3p6M0E9PSIsInZhbHVlIjoiMTBnM0x0R3NNektYczFYNjV5dVBzQT09IiwibWFjIjoiIn0=";
        const string key = "OXZjemFjZXFtODVnZ204d2NwbHRtOWhpc2l3dzd6ZDM=";
        var encryptedText = Cipher.Encrypt(originalText, key);

        // Act
        var decryptedText = Cipher.Decrypt(encryptedText, key);

        // Assert
        Assert.Equal(originalText, decryptedText);
    }
    
    [Fact]
    public void Encrypt_ShouldThrowArgumentException_WhenPlainTextIsNull()
    {
        // Arrange
        const string key = "OXZjemFjZXFtODVnZ204d2NwbHRtOWhpc2l3dzd6ZDM=";
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Cipher.Encrypt(null, key));
    }
    
    [Fact]
    public void Encrypt_ShouldThrowArgumentException_WhenPlainTextIsEmpty()
    {
        // Arrange
        const string key = "OXZjemFjZXFtODVnZ204d2NwbHRtOWhpc2l3dzd6ZDM=";
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Cipher.Encrypt(string.Empty, key));
    }
    
    [Fact]
    public void Decrypt_ShouldThrowArgumentException_WhenEncryptedTextIsNull()
    {
        // Arrange
        const string key = "OXZjemFjZXFtODVnZ204d2NwbHRtOWhpc2l3dzd6ZDM=";
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Cipher.Decrypt(null, key));
    }
    
    [Fact]
    public void Decrypt_ShouldThrowArgumentException_WhenEncryptedTextIsEmpty()
    {
        // Arrange
        const string key = "OXZjemFjZXFtODVnZ204d2NwbHRtOWhpc2l3dzd6ZDM=";
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Cipher.Decrypt(string.Empty, key));
    }
    
    [Fact]
    public void Decrypt_ShouldThrowArgumentNullException_WhenKeyIsNull()
    {
        // Arrange
        const string encryptedText = "eyJpdiI6Im9sTndScmpQS0FVSWdlRUZYN3p6M0E9PSIsInZhbHVlIjoiMTBnM0x0R3NNektYczFYNjV5dVBzQT09IiwibWFjIjoiIn0=";
        
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => Cipher.Decrypt(encryptedText, null));
    }
    
    [Fact]
    public void Decrypt_ShouldThrowArgumentException_WhenKeyIsEmpty()
    {
        // Arrange
        const string encryptedText = "eyJpdiI6Im9sTndScmpQS0FVSWdlRUZYN3p6M0E9PSIsInZhbHVlIjoiMTBnM0x0R3NNektYczFYNjV5dVBzQT09IiwibWFjIjoiIn0=";
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Cipher.Decrypt(encryptedText, string.Empty));
    }
    
    [Fact]
    public void Decrypt_ShouldThrowFormatException_WhenEncryptedTextIsInvalid()
    {
        // Arrange
        const string key = "OXZjemFjZXFtODVnZ204d2NwbHRtOWhpc2l3dzd6ZDM=";
        
        // Act & Assert
        Assert.Throws<FormatException>(() => Cipher.Decrypt("invalid", key));
    }
    
    [Fact]
    public void Decrypt_ShouldThrowInvalidOperationException_WhenEncryptedTextIsMissingData()
    {
        // Arrange
        const string key = "OXZjemFjZXFtODVnZ204d2NwbHRtOWhpc2l3dzd6ZDM=";
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => Cipher.Decrypt("eyJpdiI6Im9sTndScmpQS0FVSWdlRUZYN3p6M0E9PSIsInZhbHVlIjoiIiwibWFjIjoiIn0=", key));
    }
}