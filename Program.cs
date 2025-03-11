using System.Numerics;
using Sequence;
using Sequence.Config;
using Sequence.Contracts;
using Sequence.EmbeddedWallet;
using Sequence.Provider;
using Sequence.Utils;

namespace SequenceDotNetBoilerplate
{
    public static class Program
    {
        private const Chain CurrentChain = Chain.TestnetArbitrumSepolia;
        private const string TokenContractAddress = "0xd2926e2ee243e8df781ab907b48f77ec5d7a8be1";
        private const string SaleContractAddress = "0x476f14887372e21fea64baba11c849b518a2e928";
        
        private static readonly SequenceConfigBase Config = new SequenceConfigBase
        {
            WaaSConfigKey = "eyJwcm9qZWN0SWQiOjIwNSwicnBjU2VydmVyIjoiaHR0cHM6Ly93YWFzLnNlcXVlbmNlLmFwcCJ9",
            BuilderAPIKey = "EGBjtLEdovfU6GHd4OUpsvZAAAAAAAAAA",
            WaaSVersion = "DotNet (3.19.5)"
        };
        
        private static IWallet _wallet;
        
        public static async Task Main()
        {
            SequenceConfig.SetConfig(Config);
            await LoginAsync();
            await TestSaleTransaction();
            await TestIndexer();
        }
    
        private static async Task LoginAsync()
        {
            var done = false;
            SequenceWallet.OnWalletCreated += wallet =>
            {
                LogHandler.Info($"Wallet created: {wallet.GetWalletAddress()}");
                _wallet = wallet;
                done = true;
            };
    
            var loginHandler = SequenceLogin.GetInstance();
            loginHandler.OnLoginFailed += (error, method, email, methods) =>
            {
                LogHandler.Error($"Error creating session: {error}");
                done = true;
            };
            
            loginHandler.GuestLogin();
    
            while (!done)
                await Task.Delay(100);
        }

        private static async Task TestSaleTransaction()
        {
            var sale = new ERC1155Sale(SaleContractAddress);
            var paymentToken = await sale.GetPaymentTokenAsync(new SequenceEthClient(CurrentChain));
            var fn = sale.Mint(_wallet.GetWalletAddress(), new[] {new BigInteger(1)}, new[] {new BigInteger(1)}, null, paymentToken, new BigInteger(1), Array.Empty<byte>());
            var response = await _wallet.SendTransaction(CurrentChain, new Transaction[] {new RawTransaction(fn)});
            
            if (response is SuccessfulTransactionReturn success)
                LogHandler.Info($"Successful Transaction: {success.txHash}");
            else if (response is FailedTransactionReturn failed)
                LogHandler.Info($"Error during Transaction: {failed.error}");
        }
        
        private static async Task TestIndexer()
        {
            var indexer = new ChainIndexer(CurrentChain);
            var balanceArgs = new GetTokenBalancesArgs(
                _wallet.GetWalletAddress(), 
                TokenContractAddress, 
                true);
            
            await Task.Delay(3000);
            var balanceResponse = await indexer.GetTokenBalances(balanceArgs);
            
            foreach (var balance in balanceResponse.balances)
                LogHandler.Info($"{balance.accountAddress} owns {balance.balance}");
        }
    }
}