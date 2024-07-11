using Confab.Shared.Abstractions.Modules;
using Microsoft.Extensions.Hosting;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers;

internal sealed class BackgroundDispatcher : BackgroundService
{
    private readonly IMessageChannel _messageChannel;
    private readonly IModuleClient _moduleClient;

    public BackgroundDispatcher(IMessageChannel messageChannel, IModuleClient moduleClient)
    {
        _messageChannel = messageChannel;
        _moduleClient = moduleClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach(var message in _messageChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await _moduleClient.PublishAsync(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

    }
}