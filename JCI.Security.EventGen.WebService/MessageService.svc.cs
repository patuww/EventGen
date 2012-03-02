using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using JCI.Security.EventGen.WebService.Engine;




namespace JCI.Security.EventGen.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MessageService" in code, svc and config file together.
    public class MessageService : IMessage
    {
        public static readonly Dictionary<IMessageCallback, string> _subscribers = new Dictionary<IMessageCallback, string>();
        public void DoWork()
        {
        }

        public void AddMessage(string message, string clientName)
        {
            foreach (var kvp in _subscribers)
            {
                if (((ICommunicationObject)kvp.Key).State == CommunicationState.Opened && kvp.Value == clientName)
                {
                    //kvp.Key.OnMessageAdded(message, DateTime.Now);
                    EventEngine.Instance.CreateMessageThread(clientName);
                }
                else
                {
                    _subscribers.Remove(kvp.Key);
                }
            }
        }

        public bool Subscribe(string clientName)
        {
            try
            {
                IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                if (MessageService._subscribers.ContainsKey(callback))
                {
                    _subscribers[callback] = clientName;
                }
                else
                {
                    _subscribers.Add(callback, clientName);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Unsubscribe()
        {
            try
            {
                IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                if (_subscribers.ContainsKey(callback))
                    _subscribers.Remove(callback);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
