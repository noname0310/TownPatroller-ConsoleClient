using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TPPacket.Class;

namespace TownPatroller.UI.ListManage
{
    class PositionListController : BaseListController
    {
        public PositionListController(GameObject _ContentObj, GameObject Prefab) : base(_ContentObj, Prefab)
        {

        }

        public void RanderList(GPSSpotManager gPSSpotManager, int HighlightIndex)
        {
            DeleteChildObject();

            for (int i = 0; i < gPSSpotManager.GPSPositions.Count; i++)
            {
                GameObject go = AddItemAt(i);
                //go.GetComponent<PosItemLinkerObj>().Name = gPSSpotManager;//////////////////////////////////////////////////////////////////////////
                go.GetComponent<PosItemLinkerObj>().SetDisplayPosition(gPSSpotManager.GPSPositions[i]);
                if (i == HighlightIndex)
                    go.GetComponent<PosItemLinkerObj>().HighLight.SetActive(true);
            }
        }
    }
}
