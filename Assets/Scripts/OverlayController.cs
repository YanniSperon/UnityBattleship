using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    [System.Serializable]
    public enum PopupType
    {
        YourTurn,
        EnemyTurn,
        PlaceDestroyer,
        PlaceSubmarine,
        PlaceCruiser,
        PlaceBattleship,
        PlaceCarrier
    }

    [System.Serializable]
    public class Popup
    {
        public PopupType type;
        public GameObject obj;
    }

    // DO NOT CHANGE DURING RUNTIME WITHOUT UPDATING ENTRIES DICT
    // This is implemented like this because Unity does not support dictionaries natively in the inspector
    // (they are not serializable)
    [SerializeField]
    private List<Popup> PopupsList = new List<Popup>();

    [HideInInspector]
    private Dictionary<PopupType, GameObject> Popups = new Dictionary<PopupType, GameObject>();

    private void Awake()
    {
        Popups.Clear();
        for (int i = 0; i < PopupsList.Count; i++)
        {
            Popups.Add(PopupsList[i].type, PopupsList[i].obj);
        }
    }

    public void SetActivePopup(PopupType type)
    {
        foreach (KeyValuePair<PopupType, GameObject> kvp in Popups)
        {
            if (kvp.Key == type)
            {
                kvp.Value.SetActive(true);
            }
            else
            {
                kvp.Value.SetActive(false);
            }
        }
    }
}
