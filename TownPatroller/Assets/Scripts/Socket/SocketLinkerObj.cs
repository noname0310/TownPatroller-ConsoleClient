using UnityEngine;
using UnityEngine.UI;
using TownPatroller.SocketClient;
using TPPacket.Packet;
using TPPacket.Class;
using TownPatroller.Socket.Helper;

public class SocketLinkerObj : MonoBehaviour
{
    public GameObject CoreLinker;

    private CarStatusUIObj carStatusUIObj;
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

        carStatusUIObj = CoreLinker.GetComponent<CarStatusUIObj>();
    }

    private void OnDestroy()
    {
        if (socketObj == null)
            return;
        socketObj.OnDataInvoke -= SocketObj_OnDataInvoke;
    }

    private void SocketObj_OnDataInvoke(BasePacket basePacket)
    {
        IGConsole.Instance.println(basePacket.packetType.ToString() + " Packet Received");
        switch (basePacket.packetType)
        {
            case PacketType.CamFrame:
                CamPacket camPacket = (CamPacket)basePacket;

                Texture ttexture = camtexture.texture;
                camtexture.texture = TextureConverter.Base64ToTexture2D(camPacket.CamFrame);
                camtexture.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = camtexture.texture.width / (float)camtexture.texture.height;
                if (ttexture != null)
                    Destroy(ttexture);
                clientSender.SendPacket(new CamPacketRecived());
                break;

            case PacketType.CamResolution:
                CamResolutionPacket crp = (CamResolutionPacket)basePacket;
                carStatusUIObj.CarDevice.camResolution = crp.Resolution;
                break;

            case PacketType.CarStatus:
                CarStatusPacket csp = (CarStatusPacket)basePacket;
                carStatusUIObj.CarDevice.SetStatus(csp.cardevice, csp.position, csp.rotation);
                clientSender.SendPacket(new CarStatusRecivedPacket());
                break;

            case PacketType.CarGPSSpotStatus:
                CarGPSSpotStatusPacket cgpssp = (CarGPSSpotStatusPacket)basePacket;
                switch (cgpssp.GPSSpotManagerChangeType)
                {
                    case GPSSpotManagerChangeType.AddSpot:
                        carStatusUIObj.CarDevice.gPSSpotManager.AddPos(cgpssp.GPSPosition);
                        carStatusUIObj.CarDevice.GPSSpotManagerUpdate();
                        break;
                    case GPSSpotManagerChangeType.RemoveSpot:
                        carStatusUIObj.CarDevice.gPSSpotManager.RemovePos(cgpssp.Index);
                        carStatusUIObj.CarDevice.GPSSpotManagerUpdate();
                        break;
                    case GPSSpotManagerChangeType.SetCurrentPos:
                        carStatusUIObj.CarDevice.gPSSpotManager.CurrentMovePosIndex = cgpssp.Index;
                        carStatusUIObj.CarDevice.GPSSpotManagerUpdate();
                        break;
                    case GPSSpotManagerChangeType.OverWrite:
                        carStatusUIObj.CarDevice.gPSSpotManager = cgpssp.GPSMover;
                        break;
                    default:
                        break;
                }
                break;

            case PacketType.UpdateDataChanged:
                DataUpdatedPacket dup = (DataUpdatedPacket)basePacket;
                carStatusUIObj.CarDevice.modeType = dup.modeType;
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

            case PacketType.ClientsInfo:
                CoreLinker.GetComponent<CoreLinkerObj>().ClientsListController.RanderList(((ClientinfoPacket)basePacket).ClientsInfo);
                break;

            default:
                break;
        }
    }
}
