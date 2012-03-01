using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;


namespace JCI.Security.EventGen.WebService
{
    [ServiceContract(CallbackContract = typeof(IMessageCallback))]
    public interface IEventGenAdmin
    {

        [OperationContract]
        void AddMessage(string message);

        [OperationContract]
        bool Subscribe();

        [OperationContract]
        bool Unsubscribe();
    }
}
