using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TPPacket.Class;

public class ClientItemLinkerObj : MonoBehaviour
{
    public Text ClientID;
    public Text TextLocation;
    public Text GPSLocation;
    public Button SelectButton;

    private CoreLinkerObj linkerObj;

    void Start()
    {
        linkerObj = GameObject.Find("UILinkManager").GetComponent<CoreLinkerObj>();
        linkerObj.RegisterObj(this);
    }

    public void SetDisplayGPSLocation(GPSPosition gPSPosition)
    {
        if (gPSPosition == null)
        {
            TextLocation.text = "N/A";
            GPSLocation.text = "0, 0";
            return;
        }

        TextLocation.text = gPSPosition.LocationName;
        GPSLocation.text = gPSPosition.latitude + ", " + gPSPosition.longitude;
    }
}
