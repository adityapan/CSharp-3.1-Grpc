using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcServer.Services
{
    public class GamesService : Games.GamesBase
    {
        private readonly ILogger<GamesService> _logger;
        public GamesService(ILogger<GamesService> logger)
        { 
            _logger = logger;
        }

        public override Task<OsResponse> GetUserOs(OsRequest request, ServerCallContext context)
        {
            OsResponse response = new OsResponse();
            if (request.UserId == 234)
            {
                response.Useros = "windows";
            }
            else
            {
                response.Useros = "linux";
            }

            return Task.FromResult(response);
        }

        public override async Task GetNewGames(NewGamesRequest request, IServerStreamWriter<NewGamesResponse> responseStream, ServerCallContext context)
        {
            List<NewGamesResponse> response = new List<NewGamesResponse>
            {
                new NewGamesResponse
                {
                    Name = "Duty Calls",
                    Price = 24.33f,
                    Requirement = new NewGamesRequirement
                    {
                        IsLinuxSupported = true,
                        IsWindowsSupported = true,
                        NeedsGraphicsCard = false
                    }
                },
                new NewGamesResponse
                {
                    Name = "Ultimate 8k Fighter",
                    Price = 124.33f,
                    Requirement = new NewGamesRequirement
                    {
                        IsLinuxSupported = false,
                        IsWindowsSupported = true,
                        NeedsGraphicsCard = true
                    }
                },
                new NewGamesResponse
                {
                    Name = "Alpha Test Go",
                    Price = 14.33f,
                    Requirement = new NewGamesRequirement
                    {
                        IsLinuxSupported = true,
                        IsWindowsSupported = false,
                        NeedsGraphicsCard = false
                    }
                }
            };

            foreach (var gamesResponse in response)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(gamesResponse);
            }
        }
    }
}
