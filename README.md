# Sequence .NET Boilerplate

The Sequence .NET port (`SequenceDotNet.dll`) was built based on the Sequence Unity SDK 3.19.5

## .NET Project

- Console Application
- .NET Framework 7.0
- C# 11.0

## Dockerfile

Run the Dockerfile for a server-side integration using the .NET SDK 7.0

Rename the `example.Dockerfile` to `Dockerfile` and fill the `ENV` values to match your project.

```
docker build -t seqdotnet .
docker run -t seqdotnet
```

## Features

The `Program.cs` acts as an integration example.
- Initialize the SDK with the Project Access Key and WaaS Config Key
- Create a guest wallet or wallet from an EOA private key
- Send an ERC1155 Sale or Mint transaction
- Use the Indexer API to check if the guest wallet owns the item 

## Dependencies

- Newtonsoft.Json
- BountyCastle.Cryptography
- NBitcoin.Secp256k1
