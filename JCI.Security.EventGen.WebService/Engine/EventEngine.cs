using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;


namespace JCI.Security.EventGen.WebService.Engine
{
    public class EventEngine
    {

        private static EventEngine _instance;
        private static List<Thread> _threadList;
        //private static ThreadStart[] _threadStartArray;
        private EventEngine()
        {

        }

        public static EventEngine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventEngine();
                }
                return _instance;
            }
        }

        public void CreateMessageThread(string clientName)
        {
            string message = "{ \"P2000Message\": {\"AuditDetails\": {\"Action\": \"0\", \"AuditGuid\": \"7B674566-5A22-4EA0-B75C-62117D61F38F\", \"DataChanged\": null, \"ItemGuid\": \"{00000000-0000-0000-0000-000000000000}\", \"ItemID\": \"0\", \"LocalTimestamp\": \"2012-03-01T12:15:26\", \"MessageVersion\": \"201\", \"UTCTimestamp\": \"2012-03-01T18:15:26\" }, \"MessageBase\": {\"BaseVersion\": \"300\", \"Category\": null, \"Escalation\": \"0\", \"ItemName\": \"Real Time List\", \"MessageSubType\": \"86\", \"MessageType\": \"28675\", \"OperatorName\": \"Cardkey\", \"PartitionName\": \"Super User\", \"Priority\": \"0\", \"Public\": \"0\", \"QueryString\": null, \"SiteName\": \"P2000v4_1Site\", \"SupportItemName\": \"1\", \"SupportOperatorName\": \"1\", \"SupportPartition\": \"1\", \"SupportQueryString\": \"0\" }, \"MessageDecode\": {\"DetailsText\": \"Real Time List\", \"MessageDateTime\": \"3/1/2012 12:15:26 PM\", \"MessageText\": \"Execute Application\", \"MessageTypeText\": \"Audit\" } }}";
            Thread t = new Thread(() => MessageLoop(clientName, message, 8000, 8000));
            t.Start();
        }

        public static void MessageLoop(string clientName, string message, int messagesPerMinute, int totalMessages)
        {
            int sleepTime = 60/messagesPerMinute*1000;
            for(int i = 0; i < totalMessages; i++)
            {
                MessageService ms = new MessageService();
                foreach (var subscriber in MessageService._subscribers)
                {
                    if(subscriber.Value == clientName)
                    {
                        subscriber.Key.OnMessageAdded(message, DateTime.Now);
                    }
                }
                //ms.OnMessageAdded(message, DateTime.Now);
                //MessageService._subscribers.AddMessage(message, clientName);
                Console.WriteLine("Sending Message");
                Thread.Sleep(sleepTime);
            }

        }
        //foreach (var callback in callbacks.Where(callback => ((ICommunicationObject) callback).State == CommunicationState.Opened))
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            string message = string.Format("Message #{0}", i + 1);
        //            callback.OnMessageAdded(message, DateTime.Now);
        //        }
        //    }

        //protected static readonly List<IMessageCallback> subscribers = new List<IMessageCallback>();
        //private readonly Dictionary<IMessageCallback, string> _subscribers = new Dictionary<IMessageCallback, string>();

        //public void AddMessage(string message, string clientName)
        //{
            //foreach (var kvp in MessageService._subscribers)
            //{
            //    if (((ICommunicationObject) kvp.Key).State == CommunicationState.Opened && kvp.Value == clientName)
            //    {
            //        kvp.Key.OnMessageAdded(message, DateTime.Now);
            //    }
            //    else
            //    {
            //        MessageService._subscribers.Remove(kvp.Key);
            //    }
            //}
            
        //}



        //public bool Subscribe(string clientName)
        //{
        //    try
        //    {
        //        var callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
        //        if (_subscribers.ContainsKey(callback))
        //        {
        //            _subscribers[callback] = clientName;
        //        }
        //        else
        //        {
        //            _subscribers.Add(callback, clientName);
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public bool Unsubscribe()
        //{
        //    try
        //    {

        //        IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
        //        if (_subscribers.ContainsKey(callback))
        //            _subscribers.Remove(callback);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}



    }
}
