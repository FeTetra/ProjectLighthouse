using System.Net.Sockets;

namespace LBPUnion.ProjectLighthouse.Servers.Presence.Types;

public class GameClient
{
    public GameClient(TcpClient tcpClient)
    {
        this.TcpClient = tcpClient;
        this.IpAddress = this.TcpClient.Client.RemoteEndPoint!.Serialize().ToString();
    }

    public TcpClient TcpClient;
    public string IpAddress { get; set; }
    public readonly byte[] RecieveBuffer = new byte[512];

    public long LastPing = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long ConnectionTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    public string? AuthToken = null;

    public Task? ReceiveTask = null;
    public Task? SendTask = null;

    public int SlotToSend = 0;
}