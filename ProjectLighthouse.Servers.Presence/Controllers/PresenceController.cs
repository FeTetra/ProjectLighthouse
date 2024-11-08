using LBPUnion.ProjectLighthouse.Configuration;
using LBPUnion.ProjectLighthouse.Database;
using LBPUnion.ProjectLighthouse.Logging;
using LBPUnion.ProjectLighthouse.Types.Users;
using LBPUnion.ProjectLighthouse.Types.Levels;
using LBPUnion.ProjectLighthouse.Types.Logging;
using LBPUnion.ProjectLighthouse.Servers.Presence.Types;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Buffers.Binary;
using System.Text;

// I have not begun writing this, but I am aware of how atrocious this will be

namespace LBPUnion.ProjectLighthouse.Servers.Presence.Controllers;

public class PresenceController
{
    private readonly HashSet<GameClient> _clients = [];

    public void Start()
    {
        Task.Factory.StartNew(this.ManageClients, TaskCreationOptions.LongRunning);
    }

    public async Task ManageClients()
    {
        //hardcoded, AND fuck logs
        using TcpListener tcpListener = new(IPAddress.Parse("0.0.0.0"), 10072);
        tcpListener.Start();
        if (tcpListener.Pending())
        {
            try
            {
                //disorganized but im rushing through this, im mad bored
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

                tcpClient.LingerState = new LingerOption(false, 0);
                tcpClient.ReceiveTimeout = 1000;
                tcpClient.SendTimeout = 1000;
                tcpClient.SendBufferSize = 256;
                tcpClient.ReceiveBufferSize = 256;

                this._clients.Add(tcpClient);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to connect to client.", LogArea.Presence);
            }

            foreach (GameClient client in this._clients)
            {
                long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                client.ReceiveTask = Task.Factory.StartNew(this.ReceiveTask, client);
                if (client.SlotToSend != 0)
                {
                    int slotId = Interlocked.Exchange(ref client.SlotToSend, 0);

                    client.SendTask = Task.Run(() =>
                    {
                        try
                        {
                            Span<byte> sendData = stackalloc byte[sizeof(int) * 4];

                            BinaryPrimitives.WriteInt32BigEndian(sendData, 0x01);
                            BinaryPrimitives.WriteInt32BigEndian(sendData[4..], slotId);

                            BinaryPrimitives.WriteInt32BigEndian(sendData[8..], 0x01);
                            BinaryPrimitives.WriteInt32BigEndian(sendData[12..], slotId);

                            XxteaEncrypt(sendData[..8], this._key);

                            XxteaEncrypt(sendData[8..], this._key);
                            XxteaEncrypt(sendData[8..], this._key);

                            client.TcpClient.Client.Send(sendData);
                        }
                    });
                }

            }
        }
    }

    private async Task ReceiveTask(object? state)
    {
        GameClient client = (GameClient)state!;
        try
        {
            if (GameClient.TcpClient.Available == 0) return;
            switch (read[0])
            {
                case 0x4c when read[1] == 0x0d && read[2] == 0x0a ;
                {
                    XxteaDecrypt(read.AsSpan()[3..][..128], this._key;
                    string authToken = Encoding.UTF8.GetString["MM_AUTH=".Length..]).TrimEnd('\0');
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error("Failed to recieve packet data.", LogArea.Presence);
        }
    }
}