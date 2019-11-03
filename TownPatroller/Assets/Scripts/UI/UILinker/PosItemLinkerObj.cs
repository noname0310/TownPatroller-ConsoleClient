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

    [HideInInspector]
    public int Index { get; private set; }

    private CoreLinkerObj linkerObj;

    void Start()
    {
        linkerObj = GameObject.Find("UILinkManager").GetComponent<CoreLinkerObj>();
        linkerObj.RegisterObj(this);
    }

    public void SetDisplayPosition(int index, GPSPosition gPSPosition)
    {
        Index = index;
        Position.text = "Lat : " + gPSPosition.latitude + ", Long : " + gPSPosition.longitude;
    }
}
