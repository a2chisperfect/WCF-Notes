using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;


namespace Server.Model
{
    [ServiceContract]
    interface ILoginService
    {
        [OperationContract]
        bool Register(string username, string password, string email);
        [OperationContract]
        User Login(string username, string password);
    }
}
