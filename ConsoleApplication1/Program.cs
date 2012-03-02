using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ConsoleApplication1.MessageService;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter when the server is running.");
            Console.ReadKey();

            Listener listener = new Listener();
            listener.Open();

            Sender sender = new Sender();
            sender.Go();

            //sender.Dispose();
            //listener.Dispose();

            Console.WriteLine("Done, press enter to exit");
            Console.ReadKey();
        }

        [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
        class Sender : IMessageCallback, IDisposable
        {
            private MessageClient messageClient;

            public void Go()
            {
                InstanceContext context = new InstanceContext(this);
                messageClient = new MessageClient(context, "WSDualHttpBinding_IMessage");

                //for (int i = 0; i < 5; i++)
                //{
                    string message = string.Format("message #{0}", 0);
                    Console.WriteLine(">>> listening " + message);
                    messageClient.AddMessage(message, "client1");
                //}

            }

            public void OnMessageAdded(string message, DateTime timestamp)
            {
            }

            public void Dispose()
            {
                messageClient.Close();
            }
        }

        [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
        class Listener : IMessageCallback, IDisposable
        {
            private MessageClient messageClient;

            public void Open()
            {
                InstanceContext context = new InstanceContext(this);
                messageClient = new MessageClient(context, "WSDualHttpBinding_IMessage");

                messageClient.Subscribe("client1");
            }

            public void OnMessageAdded(string message, DateTime timestamp)
            {
                Console.WriteLine("<<< Recieved {0} with a timestamp of {1}", message, timestamp);
            }

            public void Dispose()
            {
                messageClient.Unsubscribe();
                messageClient.Close();
            }
        }
    }
}
