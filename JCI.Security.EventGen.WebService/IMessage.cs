using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace JCI.Security.EventGen.WebService
{
    using System.ServiceModel;

    [ServiceContract(CallbackContract = typeof(IMessageCallback))]
    public interface IMessage
    {
        [OperationContract]
        void AddMessage(string message, string clientName);

        [OperationContract]
        bool Subscribe(string clientName);

        [OperationContract]
        bool Unsubscribe();
    }
}


