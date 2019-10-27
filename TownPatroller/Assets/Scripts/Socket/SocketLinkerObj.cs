using UnityEngine;
using UnityEngine.UI;
using TownPatroller.SocketClient;
using TPPacket.Packet;
using TPPacket.Class;
using TownPatroller.Socket.Helper;

public class SocketLinkerObj : MonoBehaviour
{
    public GameObject CoreLinker;

    private Cardevice cardevice;
    public RawImage camtexture;

    public SocketObj socketObj;
    public IClientSender clientSender;

    private void Awake()
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
                CamPacket camPacket = (CamPacket)basePacket;

                Texture ttexture = camtexture.texture;
                camtexture.texture = TextureConverter.Base64ToTexture2D(camPacket.CamFrame);
                if (ttexture != null)
                    Destroy(ttexture);
                break;
            case PacketType.CamConfig:
                break;
            case PacketType.CarStatus:
                CarStatusPacket csp = (CarStatusPacket)basePacket;
                CoreLinker.GetComponent<CarStatusUIObj>().CarStatusUI.SetStatus(csp.cardevice, csp.position, csp.rotation);
                break;
            case PacketType.CarGPSSpotStatus:
                break;
            case PacketType.CarGPSSpotStatusChanged:
                break;
            case PacketType.UpdateDataChanged:
                break;
            case PacketType.UpdateConsoleModeChanged:
                ConsoleUpdatedPacket cudp = (ConsoleUpdatedPacket)basePacket;
                switch (cudp.consoleMode)
                {
                    case ConsoleMode.ViewBotList:
                        ViewChanger.Instance.ChangeView(ViewChanger.ConsoleView.Botlist);
                        break;
                    case ConsoleMode.ViewSingleBot:
                        ViewChanger.Instance.ChangeView(ViewChanger.ConsoleView.Console);
                        break;
                    default:
                        break;
                }
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
