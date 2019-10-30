using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationListLinkerObj : MonoBehaviour
{
    public Button BackToCamButton;
    public Button AddItemButton;

    public GameObject AddPosPanel;
    public InputField Name;
    public Button SetCurrentPosition;
    public InputField Latitude;
    public InputField Longitude;
    public Button AddPosition;
    public Button Close;

    private SocketLinkerObj SocketLinkerObj;

    void Start()
    {
        SocketLinkerObj = GameObject.Find("SocketManager").GetComponent<SocketLinkerObj>();

        BackToCamButton.onClick.AddListener(BackToCam);
        AddItemButton.onClick.AddListener(AddItem);
    }

    private void BackToCam()
    {
        ViewChanger.Instance.ChangeView(ViewChanger.ConsoleView.Console);
    }

    private void AddItem()
    {

    }
}
