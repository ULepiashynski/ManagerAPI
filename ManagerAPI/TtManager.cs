using System;
using TickTrader.BusinessObjects;
using TickTrader.Manager.Model;
using TickTrader.BusinessObjects.Requests;

namespace ManagerAPI
{
    public class TtManager
    {
        private TickTrader.Manager.Model.ITickTraderManagerModel _manager;
        private readonly string _address;
        private readonly long _login;
        private readonly string _password;

        public TtManager(string address, long login, string password)
        {
            _address = address;
            _login = login;
            _password = password;
        }

        public void Connect()
        {
            try
            {
                _manager = new TickTraderManagerModel(new TickTrader.Server.Monitoring.Log4NetMonitoringService());
                _manager.Connect(_address, _login, _password);
                Console.WriteLine("\nConnected");
                _manager.EnablePumping(PumpingFlag.CLIENT_FLAGS_EXTENDEDPUMP);
                Console.WriteLine("\nPumping enabled");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Disconnect()
        {
            if (_manager == null || !_manager.IsConnected())
            {
                Console.WriteLine("\nManager model is not connected!");
                return;
            }

            try
            {
                _manager.DisablePumping();
                Console.WriteLine("\nPumping disabled");
                _manager.Disconnect();
                Console.WriteLine("\nDisconnected");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void CreateAccount()
        {
            if (_manager == null || !_manager.IsConnected())
            {
                Console.WriteLine("\nManager model is not connected!");
                return;
            }

            try
            {
                AccountNewRequest request = AccountNewRequest.Create(-1, "qazxsw", "qazxsw", "demoforex_gross", AccountingTypes.Gross, "", "New user", null, false, false, 100, 10000, "USD");
                _manager.CreateAccount(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void GetAccount(long login)
        {
            if (_manager == null || !_manager.IsConnected())
            {
                Console.WriteLine("\nManager model is not connected!");
                return;
            }

            try
            {
                AccountInfo account = _manager.GetAccountById(login);
                Console.WriteLine($"\n{account}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void MakeDeposit(long accountLogin, decimal amount, string currency)
        {
            if (_manager == null || !_manager.IsConnected())
            {
                Console.WriteLine("\nManager model is not connected!");
                return;
            }

            try
            {
                DepositWithdrawalRequest request = new DepositWithdrawalRequest
                {
                    AccountId = accountLogin, Amount = amount, Currency = currency, Comment = "Deposit"
                };
                _manager.DepositWithdrawal(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void MakeWithdrawal(long accountLogin, decimal amount)
        {
            if (_manager == null || !_manager.IsConnected())
            {
                Console.WriteLine("\nManager model is not connected!");
                return;
            }

            try
            {
                DepositWithdrawalRequest request = new DepositWithdrawalRequest
                {
                    AccountId = accountLogin,
                    Amount = -amount,
                    Comment = "Withdrawal"
                };
                _manager.DepositWithdrawal(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
