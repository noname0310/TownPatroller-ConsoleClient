using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TPPacket.Class;

namespace TownPatroller.UI.ListManage
{
    public class ClientsListController : BaseListController
    {
        public void RanderList(ClientInfo[] clientsInfo)
        {
            DeleteChildObject();
            SetContentHeight(clientsInfo.Length);

            for (int i = 0; i < clientsInfo.Length; i++)
            {
                GameObject go = AddItemAt(i);
                go.GetComponent<ClientItemLinkerObj>().ClientID.text = clientsInfo[i].ClientID.ToString();
                go.GetComponent<ClientItemLinkerObj>().SetDisplayGPSLocation(clientsInfo[i].GPSPosition);
            }
        }
    }
}
