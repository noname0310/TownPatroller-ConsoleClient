using UnityEngine;
using UnityEngine.UI;

namespace TownPatroller.Console
{
    public class ConsoleTextManager
    {
        private CoreLinkerObj CoreLinkerObj;
        private ScrollRect MainScrollRect;
        private InGameConsole MainConsole;

        // Start is called before the first frame update
        public ConsoleTextManager(GameObject MainScrollView, GameObject MainConsoleContent, Text MainTextPrefab, CoreLinkerObj coreLinkerObj)
        {
            MainScrollRect = MainScrollView.GetComponent<ScrollRect>();

            DeleteChildObject(MainConsoleContent);

            MainConsoleContent.AddComponent<InGameConsole>();
            MainConsole = MainConsoleContent.GetComponent<InGameConsole>();
            MainConsole._new(MainConsoleContent, MainTextPrefab, MainScrollRect);

            CoreLinkerObj = coreLinkerObj;

            CoreLinkerObj.gameObject.AddComponent<IGConsole>();
            IGConsole.Instance.Init(MainConsole);
        }

        void DeleteChildObject(GameObject ParentObject)
        {
            for (int i = 0; i < ParentObject.transform.childCount; i++)
            {
                Object.Destroy(ParentObject.transform.GetChild(i).GetComponent<Text>());
            }
        }
    }
}