using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TPPacket.Packet;
using TPPacket.Class;

public class InputLinkerObj : MonoBehaviour
{
    public InputField PositionNameField;

    private SocketLinkerObj SocketLinkerObj;
    private CarStatusUIObj CarStatusUIObj;

    private bool ReadyToCommand;
    private char PressingKey;
     
    void Start()
    {
        SocketLinkerObj = GameObject.Find("SocketManager").GetComponent<SocketLinkerObj>();

        CarStatusUIObj = gameObject.GetComponent<CarStatusUIObj>();
        ReadyToCommand = true;
    }
    void Update()
    {
        if (PositionNameField.isFocused)
            return;
        if (ViewChanger.Instance.Console.activeSelf != true)
            return;

        if (ReadyToCommand)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PressingKey = 'q';
                ReqCarDevice reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.LF, !CarStatusUIObj.CarDevice.lf_LED);
                SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                StartCoroutine(KeyCoolDown());
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                PressingKey = 'e';
                ReqCarDevice reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.RF, !CarStatusUIObj.CarDevice.rf_LED);
                SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                StartCoroutine(KeyCoolDown());
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                PressingKey = 'z';
                ReqCarDevice reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.LB, !CarStatusUIObj.CarDevice.lb_LED);
                SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                StartCoroutine(KeyCoolDown());
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                PressingKey = 'c';
                ReqCarDevice reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.RB, !CarStatusUIObj.CarDevice.rb_LED);
                SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                StartCoroutine(KeyCoolDown());
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                PressingKey = 'r';
                ReqCarDevice reqCarDevice;
                if (CarStatusUIObj.CarDevice.lf_LED || CarStatusUIObj.CarDevice.rf_LED)
                {
                    reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.LF, false, ReqCarDevice.ledType.RF, false);
                }
                else
                {
                    reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.LF, true, ReqCarDevice.ledType.RF, true);
                }
                SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                StartCoroutine(KeyCoolDown());
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                PressingKey = 'v';
                ReqCarDevice reqCarDevice;
                if (CarStatusUIObj.CarDevice.lb_LED || CarStatusUIObj.CarDevice.rb_LED)
                {
                    reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.LB, false, ReqCarDevice.ledType.RB, false);
                }
                else
                {
                    reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.LB, true, ReqCarDevice.ledType.RB, true);
                }
                SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                StartCoroutine(KeyCoolDown());
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                PressingKey = 'f';
                ReqCarDevice reqCarDevice;
                if (CarStatusUIObj.CarDevice.lf_LED || CarStatusUIObj.CarDevice.rf_LED || CarStatusUIObj.CarDevice.lb_LED || CarStatusUIObj.CarDevice.rb_LED)
                {
                    reqCarDevice = new ReqCarDevice(false, false, false, false);
                }
                else
                {
                    reqCarDevice = new ReqCarDevice(true, true, true, true);
                }
                SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                StartCoroutine(KeyCoolDown());
            }
        }

        if (CarStatusUIObj.CarDevice.modeType == ModeType.ManualDriveMode || CarStatusUIObj.CarDevice.modeType == ModeType.HaifManualDriveMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PressingKey = 'S';
                ReqCarDevice reqCarDevice = new ReqCarDevice(0, 0);
                SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                StartCoroutine(KeyCoolDown());
            }

            if (ReadyToCommand)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (PressingKey == 'w')
                        return;
                    PressingKey = 'w'; 
                    ReqCarDevice reqCarDevice = new ReqCarDevice(255, 255, true, true);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    if (PressingKey == 'a')
                        return;
                    PressingKey = 'a';
                    ReqCarDevice reqCarDevice = new ReqCarDevice(255, 255, false, true);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    if (PressingKey == 's')
                        return;
                    PressingKey = 's';
                    ReqCarDevice reqCarDevice = new ReqCarDevice(255, 255, false, false);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    if (PressingKey == 'd')
                        return;
                    PressingKey = 'd';
                    ReqCarDevice reqCarDevice = new ReqCarDevice(255, 255, true, false);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
            }
        }
    }

    private IEnumerator KeyCoolDown()
    {
        ReadyToCommand = false;
        yield return new WaitForSeconds(1f);
        ReadyToCommand = true;
    }
}
