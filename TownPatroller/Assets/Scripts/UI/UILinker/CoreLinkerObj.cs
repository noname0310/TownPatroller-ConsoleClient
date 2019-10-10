using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreLinkerObj : MonoBehaviour
{
    void Start()
    {
        
    }

    public void OnButtonEvent(ButtonType buttonType)
    {
        switch (buttonType)
        {
            case ButtonType.PositionListObj_DeleteButton:

                break;
            default:
                break;
        }
    }
}

public enum ButtonType
{
    PositionListObj_DeleteButton
}