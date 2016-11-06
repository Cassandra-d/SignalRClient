using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Http;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRClient
{
	class Program
	{
		static string token = "V30bzalBv6u0weyM56aasZOH70h9vNce5UCqdkBy1gklP1lFca8Y08A7qqgVu_EfyrOFtT01WDpaOf36dMDK3DPlxuwKJudZWPVJmJrIrdAp8ViMfeoAmCT0m5ADzIS-rI6VotvhXKUuL_1_S3E8jWq3-7bGxpjmRWyVdyqGhSOHmIVcU-i-Jm8GbuvGnr7WzTt0L86osGrsbsA2CZwZRGKdhDglGESEGWRPmV7m9f8iuhtABf1cGCX54iy8EwBdql0ikZSvAudBD77eTolLdiNmL23lPA9LhAUysL8vJf0WvatuwN8SKVOMi5uPCD5zkL5R9HA0cYukK0CSmXT0JGK2-I5NPk0oTFneNhJlwveQFDFoIF7Dl0p8vs3fyzyR9VaYGge61dfI3pTTmkFiNudKR7lCHDmVXJucHaIOcIYMrYK2mfH30uG1vPaDzfeQ9V-LHGY4H2_FEHyK5F49EMd0kNqF2khYXD7ZBxKhsnO2p7Kuiy5v-yGoWn5o63aw";
		static IHttpClient _client;
		static void Main(string[] args)
		{
			Thread thread = new Thread(DoMagic);
			thread.Name = "Client thread";
			thread.IsBackground = false;
			thread.Start();

			Console.WriteLine("Press Enter to exit");
			Console.ReadLine();
		}

		static void DoMagic()
		{
			var hubConnection = new HubConnection("http://localhost:8277");
			
			hubConnection.TraceLevel = TraceLevels.All;
			hubConnection.TraceWriter = Console.Out;

			hubConnection.Headers.Add("authorization", "bearer " + token);

			var dialogsHubProxy = hubConnection.CreateHubProxy("dialogs");

			dialogsHubProxy.On<string>("NewMessage", MessageReceived);

			hubConnection.Start().GetAwaiter().GetResult();

			var pendinMessages = dialogsHubProxy.Invoke("GetPendingMessages");

			dialogsHubProxy.Invoke("SendMessage", Guid.Parse("7b69ca31-4a09-49f5-b297-ed4a0b08a6c4"), "lalalal");
		}

		static void MessageReceived(string mess)
		{
			Console.WriteLine($"New message: {mess}");
		}
	}
}
