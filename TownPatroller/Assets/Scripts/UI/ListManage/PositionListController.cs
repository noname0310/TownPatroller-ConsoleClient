using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TPPacket.Class;

namespace TownPatroller.UI.ListManage
{
    public class PositionListController : BaseListController
    {
        public void RanderList(GPSSpotManager gPSSpotManager, int HighlightIndex)
        {
            DeleteChildObject();
            SetContentHeight(gPSSpotManager.GPSPositions.Count);

            for (int i = 0; i < gPSSpotManager.GPSPositions.Count; i++)
            {
                GameObject go = AddItemAt(i);
                go.GetComponent<PosItemLinkerObj>().Name.text = gPSSpotManager.GPSPositions[i].LocationName;
                go.GetComponent<PosItemLinkerObj>().SetDisplayPosition(gPSSpotManager.GPSPositions[i]);
                if (i == HighlightIndex)
                    go.GetComponent<PosItemLinkerObj>().HighLight.SetActive(true);
            }
        }
    }
}
