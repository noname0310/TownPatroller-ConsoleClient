using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CarStatusUI
{
    private GameObject FRLED;
    private GameObject FLLED;
    private GameObject BRLED;
    private GameObject BLLED;

    private Text RMtext;
    private Text LMtext;
    private Text FDStext;
    private Text FLHStext;
    private Text FRHStext;
    private Text LDStext;
    private Text RDStext;

    private static string[] CarStatusNum0TO1000 = new string[1002];
    private static string[] CarStatusNumm255TOm0 = new string[257];

    public CarStatusUI(GameObject _FRLED, GameObject _FLLED, GameObject _BRLED, GameObject _BLLED, 
        Text _RMtext, Text _LMtext, Text _FDStext, Text _FLHStext, Text _FRHStext, Text _LDStext, Text _RDStext)
    {
        FRLED = _FRLED;
        FLLED = _FLLED;
        BRLED = _BRLED;
        BLLED = _BLLED;

        RMtext = _RMtext;
        LMtext = _LMtext;
        FDStext = _FDStext;
        FLHStext = _FLHStext;
        FRHStext = _FRHStext;
        LDStext = _LDStext;
        RDStext = _RDStext;

        InitStatusNum();
        InitLED();
        InitTexts();
    }

    private void InitStatusNum()
    {
        for (int i = 0; i < 1001; i++)
        {
            CarStatusNum0TO1000[i] = i.ToString();
        }

        CarStatusNum0TO1000[1001] = "ERR";

        for(int i = 1; i < 257; i++)
        {
            CarStatusNumm255TOm0[i] = (i - 256).ToString();
        }

        CarStatusNumm255TOm0[0] = "MERR";
    }

    private void InitTexts()
    {
        foreach (var item in this.GetType().GetFields())
        {
            if(item.FieldType == typeof(Text))
            {
                SetText(item, 0);
            }
        }
    }

    private void SetText(FieldInfo textobj, int value)
    {
        if (0 <= value)
        {
            if (1000 < value)
                value = 1001;
            (textobj.GetValue(this) as Text).text = CarStatusNum0TO1000[value];
        }
        else
        {
            if (value < -255)
                value = -256;
            (textobj.GetValue(this) as Text).text = CarStatusNumm255TOm0[value + 256];
        }
    }

    public void SetText(string textobj, int value)
    {
        FieldInfo fieldInfo = this.GetType().GetField(textobj);

        if (0 <= value)
        {
            if (1000 < value)
                value = 1001;
            (fieldInfo.GetValue(this) as Text).text = CarStatusNum0TO1000[value];
        }
        else
        {
            if (value < -255)
                value = -256;
            (fieldInfo.GetValue(this) as Text).text = CarStatusNumm255TOm0[value + 256];
        }
    }

    private void InitLED()
    {
        foreach (LEDtype item in Enum.GetValues(typeof(LEDtype)))
        {
            ChangeLEDStatus(item, false);
        }
    }

    public void ChangeLEDStatus(LEDtype lEDtype, bool value)
    {
        GameObject LEDObj;

        switch (lEDtype)
        {
            case LEDtype.FRLED:
                LEDObj = FRLED;
                break;
            case LEDtype.FLLED:
                LEDObj = FLLED;
                break;
            case LEDtype.BRLED:
                LEDObj = BRLED;
                break;
            case LEDtype.BLLED:
                LEDObj = BLLED;
                break;
            default:
                return;
        }

        if (value)
        {
            if (LEDObj.transform.childCount < 2)
                return;

            LEDObj.transform.GetChild(0).gameObject.SetActive(true);
            LEDObj.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            if (LEDObj.transform.childCount < 2)
                return;

            LEDObj.transform.GetChild(0).gameObject.SetActive(false);
            LEDObj.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public enum LEDtype
    {
        FRLED,
        FLLED,
        BRLED,
        BLLED
    }
}
