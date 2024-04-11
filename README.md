# LaraCryptSharp

[![NuGet](https://img.shields.io/nuget/v/LaraCryptSharp.svg?maxAge=3600)](https://www.nuget.org/packages/LaraCryptSharp/)
[![Build status](https://github.com/rgdcastro/LaraCryptSharp/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/rgdcastro/LaraCryptSharp/actions/workflows/build-and-test.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

LaraCryptSharp is a .NET library designed to facilitate seamless encryption and decryption of strings compatible with Laravel's encryption mechanisms. It aims to provide an easy-to-use interface for .NET applications that need to interact with data encrypted by Laravel applications, or vice versa.

## Features

- **Encrypt**: Encrypt plain text using AES-256-CBC algorithm, generating a payload that includes the IV (Initialization Vector) and the encrypted data.
- **Decrypt**: Decrypt payloads encrypted by Laravel (assuming no MAC check is required), allowing for seamless data exchange between Laravel and .NET applications.

## Getting Started

To use LaraCryptSharp in your project, include the `LaraCryptSharp` namespace and utilize the `Cipher` class for encryption and decryption tasks.

### Installation

Currently, you need to clone the repository or copy the code directly into your project as LaraCryptSharp is not yet available as a package.

### Usage

#### Encrypting a String

```csharp
using LaraCryptSharp;

string plainText = "Hello, world!";
string key = "YourBase64EncodedLaravelAppKey"; // Laravel app key without 'base64:' prefix

string encryptedPayload = Cipher.Encrypt(plainText, key);
Console.WriteLine($"Encrypted Payload: {encryptedPayload}");
```

#### Encrypting a String

```csharp
using LaraCryptSharp;

string encryptedPayload = "YourEncryptedPayloadHere";
string key = "YourBase64EncodedLaravelAppKey"; // Laravel app key without 'base64:' prefix

string decryptedText = Cipher.Decrypt(encryptedPayload, key);
Console.WriteLine($"Decrypted Text: {decryptedText}");
```

### Notes

- The encryption function generates a new IV for each encryption operation and constructs a payload similar to Laravel's, including the IV and the encrypted data.
- The decryption function is designed to work with payloads encrypted by Laravel, assuming no MAC (Message Authentication Code) verification is required.
- Ensure the key used for encryption and decryption matches the Laravel application key used to encrypt the data.

### Contributing

Contributions are welcome! If you have suggestions or improvements, feel free to fork the repository and submit a pull request.

### License

LaraCryptSharp is open-source software licensed under the [MIT license](https://opensource.org/licenses/MIT).