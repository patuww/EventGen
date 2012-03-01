using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System;
using System.ServiceModel;
using JCI.Security.EventGen.WebService;


namespace JCI.Security.EventGen.WebHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var myService = new ServiceHost(typeof(MessageService));

            ServiceDescription serviceDesciption = myService.Description;

            foreach (ServiceEndpoint endpoint in serviceDesciption.Endpoints)
            {
                ConsoleColor oldColour = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Endpoint - address:  {0}", endpoint.Address);
                Console.WriteLine("         - binding name:\t\t{0}", endpoint.Binding.Name);
                Console.WriteLine("         - contract name:\t\t{0}", endpoint.Contract.Name);
                Console.WriteLine();
                Console.ForegroundColor = oldColour;
            }

            myService.Open();
            Console.WriteLine("Press enter to stop.");
            Console.ReadKey();

            if (myService.State != CommunicationState.Closed)
                myService.Close();
        }
    }
}
