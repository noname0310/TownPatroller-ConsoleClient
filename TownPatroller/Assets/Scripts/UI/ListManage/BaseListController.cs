using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TownPatroller.UI.ListManage
{
    class BaseListController : MonoBehaviour
    {
        private readonly GameObject ContentObj;
        private readonly GameObject ObjectPrefab;
        private readonly float PrefabHeight;
        private const int BetweenSpace = 1;
        private const int startpos = -300;


        public BaseListController(GameObject _ContentObj, GameObject Prefab)
        {
            ContentObj = _ContentObj;
            ObjectPrefab = Prefab;
            PrefabHeight = ObjectPrefab.GetComponent<RectTransform>().sizeDelta.y;
        }

        protected GameObject AddItemAt(int index)
        {
            GameObject item = Instantiate(ObjectPrefab, ContentObj.transform);
            float CreateYPos = startpos + (PrefabHeight * index) + (BetweenSpace * index);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, CreateYPos, item.transform.localPosition.z);

            return item;
        }

        protected void DeleteChildObject()
        {
            for (int i = 0; i < ContentObj.transform.childCount; i++)
            {
                Destroy(ContentObj.transform.GetChild(i).gameObject);
            }
        }
    }
}
