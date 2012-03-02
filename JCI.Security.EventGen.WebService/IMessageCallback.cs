namespace JCI.Security.EventGen.WebService
{
    using System;
    using System.ServiceModel;

    public interface IMessageCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnMessageAdded(string message, DateTime timestamp);
    }
}
