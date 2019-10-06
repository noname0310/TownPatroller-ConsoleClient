using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TownPatroller.SocketClient;
using TPPacket.Packet;
using TPPacket.Class;

public class SocketLinkerObj : MonoBehaviour
{
    public GameObject CarStatusObject;

    private Cardevice cardevice;
    private Texture2D texture2D;

    public SocketObj socketObj;
    private IClientSender clientSender;

    private void Start()
    {
        CarStatusObject = GameObject.Find("CarStatusObject");

        socketObj = GameObject.Find("NetworkManager(Clone)").GetComponent<SocketObj>();
        clientSender = socketObj.socketClient;
        socketObj.OnDataInvoke += SocketObj_OnDataInvoke;
    }

    private void SocketObj_OnDataInvoke(BasePacket basePacket)
    {
        
    }
}
