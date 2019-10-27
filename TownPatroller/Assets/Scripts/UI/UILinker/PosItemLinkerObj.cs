using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TPPacket.Class;

public class PosItemLinkerObj : MonoBehaviour
{
    public Text Name;
    public Text Position;
    public GameObject HighLight;
    public Button DeleteButton;

    private CoreLinkerObj linkerObj;

    void Start()
    {
        HighLight.SetActive(false);
        linkerObj = GameObject.Find("UILinkManager").GetComponent<CoreLinkerObj>();
        linkerObj.RegisterObj(this);
    }

    public void SetDisplayPosition(GPSPosition gPSPosition)
    {
        Position.text = "Lat : " + gPSPosition.latitude + ", Long : " + gPSPosition.longitude;
    }
}
