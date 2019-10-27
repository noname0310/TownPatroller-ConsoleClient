using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject Compass;

    public CarStatusUI CarStatusUI;

    void Start()
    {
        CarStatusUI = new CarStatusUI(FRLED, FLLED, BRLED, BLLED, RMtext, LMtext, FDStext, FLHStext, FRHStext, LDStext, RDStext);
    }
}
