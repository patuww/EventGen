namespace JCI.Security.EventGen.Core
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
