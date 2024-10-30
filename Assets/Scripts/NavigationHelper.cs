using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationHelper : MonoBehaviour
{
    public GameObject firstToSelect;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstToSelect);
    }
}
