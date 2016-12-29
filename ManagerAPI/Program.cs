using System;

namespace ManagerAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            TtManager manager = new TtManager("tp.dev.soft-fx.eu", 8, "*******");
            manager.Connect();
            //manager.CreateAccount();
            manager.GetAccount(100042);
            manager.MakeDeposit(100042, 1000, "USD");
            manager.GetAccount(100042);
            manager.Disconnect();

            Console.ReadKey();
        }
    }
}
