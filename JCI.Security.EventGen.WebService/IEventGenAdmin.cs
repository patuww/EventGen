using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;


namespace JCI.Security.EventGen.WebService
{
    //[ServiceContract(CallbackContract = typeof(IMessageCallback))]
    [ServiceContract]
    public interface IEventGenAdmin
    {
        [OperationContract]
        List<string> GetSubscribedClients();

        [OperationContract]
        void GenerateMessages(string clientName, string message, int messagesPerMinute, int totalMessages);
    }
}
