# Sequence .NET Boilerplate

The Sequence .NET port (`SequenceDotNet.dll`) was built based on the Sequence Unity SDK 3.19.5

## .NET Project

- Console Application
- .NET 7.0
- C# 11.0

## Dockerfile

Run the Dockerfile for a server-side integration using the .NET SDK 7.0.

## Features

The `Program.cs` acts as an integration example.
- Initialize the SDK with the Project Access Key and WaaS Config Key
- Create a guest wallet
- Send a transaction to an ERC1155 Sale contract to purchase one item
- Use the Indexer API to check if the guest wallet owns the item 

## Dependencies

- Newtonsoft.Json
- BountyCastle.Cryptography
- NBitcoin.Secp256k1
