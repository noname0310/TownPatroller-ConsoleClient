using UnityEngine;

namespace TownPatroller.UI.ListManage
{
    public abstract class BaseListController : MonoBehaviour
    {
        public GameObject ContentObj { get; private set; }
        public GameObject ObjectPrefab { get; private set; }
        public float PrefabHeight { get; private set; }
        private const int BetweenSpace = 20;
        private const int startpos = -300;


        public void New(GameObject _ContentObj, GameObject Prefab)
        {
            ContentObj = _ContentObj;
            ObjectPrefab = Prefab;
            PrefabHeight = ObjectPrefab.GetComponent<RectTransform>().sizeDelta.y;
        }

        protected GameObject AddItemAt(int index)
        {
            GameObject item = Instantiate(ObjectPrefab, ContentObj.transform);
            float CreateYPos = startpos - (PrefabHeight * index) - (BetweenSpace * index);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, CreateYPos, item.transform.localPosition.z);

            return item;
        }

        protected void SetContentHeight(int itemcount)
        {
            ContentObj.GetComponent<RectTransform>().sizeDelta = new Vector2(ContentObj.GetComponent<RectTransform>().sizeDelta.x, - startpos + (itemcount * (PrefabHeight + BetweenSpace)));
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
