using CSharp2SqlLib;

using System;

namespace TestCSharp2SQlLib {
    class Program {
        static void Main(string[] args) {

            var sqllib = new SqlLib();
            sqllib.Connect();

            var users = sqllib.GetAllUsers();
            var user = sqllib.GetByPK(1);
            var nulluser = sqllib.GetByPK(0);

            sqllib.Disconnect();
        }
    }
}
