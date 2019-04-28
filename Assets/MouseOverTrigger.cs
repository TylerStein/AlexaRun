using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOverTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectToShow;
    public Button btn;

    public void OnPointerEnter(PointerEventData eventData) {
        if (!objectToShow.activeSelf) objectToShow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (objectToShow.activeSelf) objectToShow.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        objectToShow.SetActive(false);
    }

    
}
