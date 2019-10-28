using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TPPacket.Packet;
using TPPacket.Class;

public class InputLinkerObj : MonoBehaviour
{
    private SocketLinkerObj SocketLinkerObj;

    CarStatusUIObj CarStatusUIObj;
    bool ReadyToCommand;

    char PressingKey;

    void Start()
    {
        SocketLinkerObj = GameObject.Find("SocketManager").GetComponent<SocketLinkerObj>();

        CarStatusUIObj = gameObject.GetComponent<CarStatusUIObj>();
        ReadyToCommand = true;
    }
    void Update()
    {
        if (CarStatusUIObj.CarDevice.modeType == ModeType.ManualDriveMode || CarStatusUIObj.CarDevice.modeType == ModeType.HaifManualDriveMode)
        {
            bool HasMoveCommand = false;

            if (ReadyToCommand)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if (PressingKey == 'w')
                        return;
                    HasMoveCommand = true;
                    PressingKey = 'w'; 
                    ReqCarDevice reqCarDevice = new ReqCarDevice(255, 255, true, true);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    if (PressingKey == 'a')
                        return;
                    HasMoveCommand = true;
                    PressingKey = 'a';
                    Cardevice cardevice = CarStatusUIObj.CarDevice.GetPacketCarDivice();
                    ReqCarDevice reqCarDevice = new ReqCarDevice(255, 255, false, true);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    if (PressingKey == 's')
                        return;
                    HasMoveCommand = true;
                    PressingKey = 's';
                    Cardevice cardevice = CarStatusUIObj.CarDevice.GetPacketCarDivice();
                    ReqCarDevice reqCarDevice = new ReqCarDevice(255, 255, false, false);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    if (PressingKey == 'd')
                        return;
                    HasMoveCommand = true;
                    PressingKey = 'd';
                    Cardevice cardevice = CarStatusUIObj.CarDevice.GetPacketCarDivice();
                    ReqCarDevice reqCarDevice = new ReqCarDevice(255, 255, true, false);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }

                else if (Input.GetKey(KeyCode.Q))
                {
                    if (PressingKey == 'q')
                        return;
                    PressingKey = 'q';
                    ReqCarDevice reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.LF, !CarStatusUIObj.CarDevice.lf_LED);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    if (PressingKey == 'e')
                        return;
                    PressingKey = 'e';
                    ReqCarDevice reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.RF, !CarStatusUIObj.CarDevice.rf_LED);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
                else if (Input.GetKey(KeyCode.Z))
                {
                    if (PressingKey == 'z')
                        return;
                    PressingKey = 'z';
                    ReqCarDevice reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.LB, !CarStatusUIObj.CarDevice.lb_LED);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
                else if (Input.GetKey(KeyCode.C))
                {
                    if (PressingKey == 'c')
                        return;
                    PressingKey = 'c';
                    ReqCarDevice reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.RB, !CarStatusUIObj.CarDevice.rb_LED);
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
                else if (Input.GetKey(KeyCode.R))
                {
                    if (PressingKey == 'r')
                        return;
                    PressingKey = 'r';
                    ReqCarDevice reqCarDevice;
                    if (CarStatusUIObj.CarDevice.lf_LED || CarStatusUIObj.CarDevice.rf_LED)
                    {
                        reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.LF, false, ReqCarDevice.ledType.RF, false);
                    }
                    else
                    {
                        reqCarDevice = new ReqCarDevice(ReqCarDevice.ledType.LF, false, ReqCarDevice.ledType.RF, false);
                    }
                    SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                    StartCoroutine(KeyCoolDown());
                }
                else if (Input.GetKey(KeyCode.V))
                {
                    if (PressingKey == 'v')
                        return;
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
                else if (Input.GetKey(KeyCode.F))
                {
                    if (PressingKey == 'f')
                        return;
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

            if (HasMoveCommand == false && PressingKey != 'S' && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)))
            {
                PressingKey = 'S';
                ReqCarDevice reqCarDevice = new ReqCarDevice(0, 0, true, true);
                SocketLinkerObj.clientSender.SendPacket(new CarStatusChangeReqPacket(reqCarDevice));
                StartCoroutine(KeyCoolDown());
            }
        }
    }

    private IEnumerator KeyCoolDown()
    {
        ReadyToCommand = false;
        yield return new WaitForSeconds(2f);
        ReadyToCommand = true;
    }
}
