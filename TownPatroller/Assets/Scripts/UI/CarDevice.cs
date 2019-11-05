using UnityEngine;
using TPPacket.Class;
using TPPacket.Packet;

namespace TownPatroller.UI
{
    public class CarDevice
    {
        CarStatusUIObj CarStatusUIObj; 
        private static string[] CarStatusNum0TO1000 = new string[1002];
        private static string[] CarStatusNumm255TOm0 = new string[257];

        public ushort f_sonardist 
        { 
            get 
            { 
                return F_sonardist;
            }
            set
            {
                F_sonardist = value;
                CarStatusUIObj.FDStext.text = GetStringFromNum(value);
            } 
        }
        public ushort rh_sonardist
        {
            get
            {
                return RH_sonardist;
            }
            set
            {
                RH_sonardist = value;
                CarStatusUIObj.FRHStext.text = GetStringFromNum(value);
            }
        }
        public ushort lh_sonardist
        {
            get
            {
                return LH_sonardist;
            }
            set
            {
                LH_sonardist = value;
                CarStatusUIObj.FLHStext.text = GetStringFromNum(value);
            }
        }
        public ushort rs_sonardist
        {
            get
            {
                return RS_sonardist;
            }
            set
            {
                RS_sonardist = value;
                CarStatusUIObj.RDStext.text = GetStringFromNum(value);
            }
        }
        public ushort ls_sonardist
        {
            get
            {
                return LS_sonardist;
            }
            set
            {
                LS_sonardist = value;
                CarStatusUIObj.LDStext.text = GetStringFromNum(value);
            }
        }

        public byte r_motorpower
        {
            get
            {
                return R_motorpower;
            }
            set
            {
                R_motorpower = value;
                if (R_motorDIR)
                    CarStatusUIObj.RMtext.text = GetStringFromNum(value);
                else
                    CarStatusUIObj.RMtext.text = GetStringFromNum(value * -1);
            }
        }
        public byte l_motorpower
        {
            get
            {
                return L_motorpower;
            }
            set
            {
                L_motorpower = value;
                if (L_motorDIR)
                    CarStatusUIObj.LMtext.text = GetStringFromNum(value);
                else
                    CarStatusUIObj.LMtext.text = GetStringFromNum(value * -1);
            }
        }
        public bool R_motorDIR { get; set; }
        public bool L_motorDIR { get; set; }
        public bool rf_LED 
        { 
            get 
            { 
                return RF_LED; 
            }
            set
            {
                RF_LED = value;
                ChangeLEDStatus(LEDtype.FRLED, value);
            } 
        }
        public bool lf_LED
        {
            get
            {
                return LF_LED;
            }
            set
            {
                LF_LED = value;
                ChangeLEDStatus(LEDtype.FLLED, value);
            }
        }
        public bool rb_LED
        {
            get
            {
                return RB_LED;
            }
            set
            {
                RB_LED = value;
                ChangeLEDStatus(LEDtype.BRLED, value);
            }
        }
        public bool lb_LED
        {
            get
            {
                return LB_LED;
            }
            set
            {
                LB_LED = value;
                ChangeLEDStatus(LEDtype.BLLED, value);
            }
        }

        public GPSPosition gPSPosition 
        {
            get
            {
                return GPSPosition;
            }
            set
            {
                GPSPosition = value;
                CarStatusUIObj.Longitude.text = GPSPosition.longitude.ToString();
                CarStatusUIObj.Latitude.text = GPSPosition.latitude.ToString();
            }
        }
        public float rotation 
        {
            get
            {
                return Rotation;
            }
            set
            {
                Rotation = value;
                CarStatusUIObj.Compass.transform.localRotation = Quaternion.Euler(0f, 0f, value * -1);
                CarStatusUIObj.CompassT.text = (value * -1 % 360).ToString();
            }
        }
        public ModeType modeType
        {
            get
            {
                return ModeType;
            }
            set
            {
                ModeType = value;
                switch (value)
                {
                    case ModeType.AutoDriveMode:
                        CarStatusUIObj.DriveMode.text = "AutoDriveMode";
                        break;
                    case ModeType.ManualDriveMode:
                        CarStatusUIObj.DriveMode.text = "ManualDriveMode";
                        break;
                    case ModeType.HaifManualDriveMode:
                        CarStatusUIObj.DriveMode.text = "HaifManualDriveMode";
                        break;
                    default:
                        break;
                }
            }
        }

        public GPSSpotManager gPSSpotManager
        {
            get
            {
                return GPSSpotManager;
            }
            set
            {
                GPSSpotManager = value;

                GPSSpotManagerUpdate();
            }
        }

        public int camResolution
        {
            get
            {
                return CamResolution;
            }
            set
            {
                CamResolution = value;
                CarStatusUIObj.CamResolution.text = "1/" + value;
            }
        }


        private ushort F_sonardist;
        private ushort RH_sonardist;
        private ushort LH_sonardist;
        private ushort RS_sonardist;
        private ushort LS_sonardist;
        private byte R_motorpower;
        private byte L_motorpower;

        private bool LF_LED;
        private bool RF_LED;
        private bool RB_LED;
        private bool LB_LED;

        private GPSPosition GPSPosition;
        private float Rotation;
        private ModeType ModeType;
        private GPSSpotManager GPSSpotManager;
        private int CamResolution;


        public CarDevice(CarStatusUIObj carStatusUIObj)
        {
            CarStatusUIObj = carStatusUIObj;
            InitStatusNum();
        }

        public void GPSSpotManagerUpdate()
        {
            CarStatusUIObj.GetComponent<CoreLinkerObj>().PositionListController.RanderList(GPSSpotManager);

            if (GPSSpotManager.GPSPositions.Count > 0)
                CarStatusUIObj.DrivingLocation.text = "goto: " + GPSSpotManager.GPSPositions[GPSSpotManager.CurrentMovePosIndex].LocationName;
            else
                CarStatusUIObj.DrivingLocation.text = "N/A";
        }

        private void InitStatusNum()
        {
            for (int i = 0; i < 1001; i++)
            {
                CarStatusNum0TO1000[i] = i.ToString();
            }

            CarStatusNum0TO1000[1001] = "ERR";

            for (int i = 1; i < 257; i++)
            {
                CarStatusNumm255TOm0[i] = (i - 256).ToString();
            }

            CarStatusNumm255TOm0[0] = "MERR";
        }

        private string GetStringFromNum(int Num)
        {
            if (0 <= Num)
            {
                if (1000 < Num)
                    Num = 1001;
                return CarStatusNum0TO1000[Num];
            }
            else
            {
                if (Num < -255)
                    Num = -256;
                return CarStatusNumm255TOm0[Num + 256];
            }
        }

        public void SetStatus(Cardevice cardevice, GPSPosition _gPSPosition, float _rotation)
        {
            f_sonardist = cardevice.F_sonardist;
            rh_sonardist = cardevice.RH_sonardist;
            lh_sonardist = cardevice.LH_sonardist;
            rs_sonardist = cardevice.RS_sonardist;
            ls_sonardist = cardevice.LS_sonardist;
            R_motorDIR = cardevice.R_motorDIR;
            L_motorDIR = cardevice.L_motorDIR;
            r_motorpower = cardevice.R_motorpower;
            l_motorpower = cardevice.L_motorpower;
            rf_LED = cardevice.RF_LED;
            lf_LED = cardevice.LF_LED;
            rb_LED = cardevice.RB_LED;
            lb_LED = cardevice.LB_LED;

            gPSPosition = _gPSPosition;
            rotation = _rotation;
        }

        public void ChangeLEDStatus(LEDtype lEDtype, bool value)
        {
            GameObject LEDObj;

            switch (lEDtype)
            {
                case LEDtype.FRLED:
                    LEDObj = CarStatusUIObj.FRLED;
                    break;
                case LEDtype.FLLED:
                    LEDObj = CarStatusUIObj.FLLED;
                    break;
                case LEDtype.BRLED:
                    LEDObj = CarStatusUIObj.BRLED;
                    break;
                case LEDtype.BLLED:
                    LEDObj = CarStatusUIObj.BLLED;
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
        public Cardevice GetPacketCarDivice()
        {
            return new Cardevice(F_sonardist, RH_sonardist, LH_sonardist, RS_sonardist, LS_sonardist,
                R_motorpower, L_motorpower, R_motorDIR, L_motorDIR,
                RF_LED, LF_LED, RB_LED, LB_LED);
        }
    }
}