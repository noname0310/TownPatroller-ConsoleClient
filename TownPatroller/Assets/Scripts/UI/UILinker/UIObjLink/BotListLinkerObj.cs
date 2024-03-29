﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TPPacket.Class;
using TPPacket.Packet;

public class BotListLinkerObj : MonoBehaviour
{
    public Button DisconnectButton;
    public Button RefreshButton;

    private SocketLinkerObj SocketLinkerObj;

    private void Start()
    {
        SocketLinkerObj = GameObject.Find("SocketManager").GetComponent<SocketLinkerObj>();
        Refresh();
        DisconnectButton.onClick.AddListener(Disconnect);
        RefreshButton.onClick.AddListener(Refresh);
    }

    private void Disconnect()
    {
        SocketLinkerObj.socketObj?.QuitClient(); 
        SceneManager.LoadScene("ConnectScene", LoadSceneMode.Single);
    }

    private void Refresh()
    {
        SocketLinkerObj?.clientSender?.SendPacket(new ClientinfoReqPacket());
    }
}
