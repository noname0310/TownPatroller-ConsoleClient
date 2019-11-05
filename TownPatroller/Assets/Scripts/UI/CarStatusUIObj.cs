using UnityEngine;
using UnityEngine.UI;
using TownPatroller.UI;

public class CarStatusUIObj : MonoBehaviour
{
    public GameObject FRLED;
    public GameObject FLLED;
    public GameObject BRLED;
    public GameObject BLLED;

    public Text RMtext;
    public Text LMtext;
    public Text FDStext;
    public Text FLHStext;
    public Text FRHStext;
    public Text LDStext;
    public Text RDStext;

    public Text Longitude;
    public Text Latitude;

    public Text CompassT;
    public GameObject Compass;

    public Text DriveMode;
    public Text DrivingLocation;

    public Text CamResolution;

    public CarDevice CarDevice;

    void Start()
    {
        CarDevice = new CarDevice(this);
    }
}
