# Sequence .NET Boilerplate

The Sequence .NET port (`SequenceDotNet.dll`) was built based on the Sequence Unity SDK 3.19.5

## Features

The `Program.cs` acts as an integration examples.
- Initialize the SDK with the Project Access Key and WaaS Config Key
- Create a guest wallet
- Send a transaction to an ERC1155 Sale contract to purchase one item
- Use the Indexer API to check if the guest wallet owns the item 

## Dependencies

- Newtonsoft.Json
- BountyCastle.Cryptography
- NBitcoin.Secp256k1
