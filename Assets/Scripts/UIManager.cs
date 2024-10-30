using System;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    public enum PageType
    {
        Title,
        Settings,
        Pause,
        Loss,
        Win,
        InGameOverlay
    }

    [System.Serializable]
    public class Page
    {
        public PageType type;
        public GameObject obj;
    }

    // DO NOT CHANGE DURING RUNTIME WITHOUT UPDATING ENTRIES DICT
    // This is implemented like this because Unity does not support dictionaries natively in the inspector
    // (they are not serializable)
    [SerializeField]
    private List<Page> PagesList = new List<Page>();

    [HideInInspector]
    private Dictionary<PageType, GameObject> Pages = new Dictionary<PageType, GameObject>();

    private void Awake()
    {
        Pages.Clear();
        for (int i = 0; i < PagesList.Count; i++)
        {
            Pages.Add(PagesList[i].type, PagesList[i].obj);
        }
    }

    public void SetActivePage(PageType pageName)
    {
        foreach (KeyValuePair<PageType, GameObject> kvp in Pages)
        {
            if (kvp.Key == pageName)
            {
                kvp.Value.SetActive(true);
            } else
            {
                kvp.Value.SetActive(false);
            }
        }
    }
}
