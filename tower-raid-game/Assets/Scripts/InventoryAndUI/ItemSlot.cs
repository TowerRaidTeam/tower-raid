using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public bool slotFilled;
    public Vector3 ceneterLocation;

    private void Start()
    {
        ceneterLocation = GetComponent<RectTransform>().position;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log(eventData.pointerDrag.GetComponent<RectTransform>().name);
            Debug.Log("DROPPED");
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PRESSED ON THE SLOT");
    }
}
