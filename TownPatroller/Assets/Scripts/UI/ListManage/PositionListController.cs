using UnityEngine;
using TPPacket.Class;

namespace TownPatroller.UI.ListManage
{
    public class PositionListController : BaseListController
    {
        public void RanderList(GPSSpotManager gPSSpotManager)
        {
            DeleteChildObject();
            SetContentHeight(gPSSpotManager.GPSPositions.Count);

            for (int i = 0; i < gPSSpotManager.GPSPositions.Count; i++)
            {
                GameObject go = AddItemAt(i);
                go.GetComponent<PosItemLinkerObj>().Name.text = gPSSpotManager.GPSPositions[i].LocationName;
                go.GetComponent<PosItemLinkerObj>().SetDisplayPosition(i, gPSSpotManager.GPSPositions[i]);
                if (i == gPSSpotManager.CurrentMovePosIndex)
                    go.GetComponent<PosItemLinkerObj>().HighLight.SetActive(true);
            }
        }
    }
}
