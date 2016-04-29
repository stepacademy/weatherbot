
// local WeatherBot host

using System;
using System.IO;
using System.ServiceModel;
using Test.AHost.MessageConveyorServiceReference;

namespace Test.AHost {

    public class Program {

        static void Main(string[] args) {

            Program test = new Program();

            InstanceContext instanceContext = new InstanceContext(test);
            ManagementContractClient proxy = new ManagementContractClient(instanceContext);
            proxy.Open();

            string botToken;

            try {
                using (StreamReader file = new StreamReader("botToken.txt")) {

                    if ((botToken = file.ReadLine()) != null) {
                        proxy.Start(botToken, InteractionMode.GetUpdatesBased);
                        Console.WriteLine("Telegram API Interaction ready...\n");
                    }
                    file.Close();
                }
            }
            catch (FileNotFoundException e) {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();            
        }
    }
}