using UnityEngine;
using TownPatroller.SocketClient;
using TPPacket.Packet;
using TPPacket.Class;

public class SocketLinkerObj : MonoBehaviour
{
    public GameObject CoreLinker;

    private Cardevice cardevice;
    private Texture2D texture2D;

    public SocketObj socketObj;
    public IClientSender clientSender;

    private void Start()
    {
        socketObj = GameObject.Find("NetworkManager(Clone)")?.GetComponent<SocketObj>();
        if (socketObj == null)
            return;

        clientSender = socketObj.socketClient;
        socketObj.OnDataInvoke += SocketObj_OnDataInvoke;
    }

    private void OnDestroy()
    {
        if (socketObj == null)
            return;
        socketObj.OnDataInvoke -= SocketObj_OnDataInvoke;
    }

    private void SocketObj_OnDataInvoke(BasePacket basePacket)
    {
        switch (basePacket.packetType)
        {
            case PacketType.ConnectionStat:
                break;
            case PacketType.CamFrame:
                break;
            case PacketType.CamConfig:
                break;
            case PacketType.CarStatus:
                break;
            case PacketType.CarGPSSpotStatus:
                break;
            case PacketType.CarGPSSpotStatusChanged:
                break;
            case PacketType.UpdateDataChanged:
                break;
            case PacketType.UpdateConsoleModeChanged:
                break;
            case PacketType.UniversalCommand:
                break;
            case PacketType.ClientsInfo:
                CoreLinker.GetComponent<CoreLinkerObj>().ClientsListController.RanderList(((ClientinfoPacket)basePacket).ClientsInfo);
                break;
            default:
                break;
        }
    }
}
