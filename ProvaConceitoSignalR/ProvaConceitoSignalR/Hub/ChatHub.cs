namespace ProvaConceitoSignalR.Hub
{
    using Microsoft.AspNetCore.SignalR;
    using SignalRSwaggerGen.Attributes;


    [SignalRHub]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("SendMessage", message);
        }

        public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
        {
            while (true)
            {
                yield return DateTime.UtcNow;
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
