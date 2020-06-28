using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Testing Greeter
            // var input = new HelloRequest{ Name = "Alex"};
            // var channel = GrpcChannel.ForAddress("https://localhost:5001");
            // var client = new Greeter.GreeterClient(channel);
            //
            // var reply = await client.SayHelloAsync(input);
            //
            // Console.WriteLine(reply.Message);

            //Testing Games
            var input1 = new OsRequest{UserId = 235};
            var input2 = new OsRequest{UserId = 234};
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Games.GamesClient(channel);

            var reply1 = await client.GetUserOsAsync(input1);
            var reply2 = await client.GetUserOsAsync(input2);

            Console.WriteLine(reply1.Useros);
            Console.WriteLine(reply2.Useros);

            using (var newGamesCall = client.GetNewGames(new NewGamesRequest()))
            {
                while (await newGamesCall.ResponseStream.MoveNext())
                {
                    var currentGame = newGamesCall.ResponseStream.Current;
                        
                    Console.WriteLine($"{currentGame.Name} {currentGame.Price} \nGraphics Card Required: {currentGame.Requirement.NeedsGraphicsCard}");
                }
            }

            Console.ReadLine();
        }
    }
}
