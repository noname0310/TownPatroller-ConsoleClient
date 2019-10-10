using UnityEngine;
using UnityEngine.UI;

namespace TownPatroller.Console
{
    public class ConsoleTextManager
    {
        private GameObject UILinker;
        private ScrollRect MainScrollRect;
        private InGameConsole MainConsole;

        // Start is called before the first frame update
        public ConsoleTextManager(GameObject MainScrollView, GameObject MainConsoleContent, Text MainTextPrefab, GameObject _UILinker)
        {
            MainScrollRect = MainScrollView.GetComponent<ScrollRect>();
            MainConsoleContent = GameObject.Find("MainConsoleContent");

            DeleteChildObject(MainConsoleContent);

            MainConsoleContent.AddComponent<InGameConsole>();
            MainConsole = MainConsoleContent.GetComponent<InGameConsole>();
            MainConsole._new(MainConsoleContent, MainTextPrefab, MainScrollRect);

            UILinker = _UILinker;

            UILinker.AddComponent<IGConsole>();
            IGConsole.Instance.Init(UILinker);
        }

        void DeleteChildObject(GameObject ParentObject)
        {
            for (int i = 0; i < ParentObject.transform.childCount; i++)
            {
                GameObject.Destroy(ParentObject.transform.GetChild(i).GetComponent<Text>());
            }
        }
    }
}