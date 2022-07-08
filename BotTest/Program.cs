using Botservice;
using System;

namespace BotTest
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            DirectLineInterface integration = new DirectLineInterface();
            
            bool flag = true;
            //integration.SendMessage("owns wintool");
            // var list = integration.GetMessages();
            // try
            //  {
            //var list = integration.GetMessages();
            //      foreach (string a in list)
            //     {
            //         Console.WriteLine(a);
            //     }

            //     Console.ReadLine();

            // }
            // catch (Exception ex)
            //  {
            //    Console.WriteLine(ex.ToString());
            // }

            try
            {
               var conversation= await integration.StartConversationAsync();
                var response = await integration.SendMessageAsync("");
                Console.WriteLine(response);
                while(flag)
                {
                    string userMessage = Console.ReadLine();
                    if (userMessage.ToLower() == "exit")
                    {
                        flag = false;
                        break;
                    }
                    else {
                       response=  await integration.SendMessageAsync(userMessage);
                       Console.WriteLine(response);
                    }

                    


                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


    }
}
