using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler , IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    public static GameObject crystalInHand;
    public static GameObject itemInHandUpgrade;
    WorldGeneration wg;
    BuildingController bc;
    GameManager gm;

    Vector3 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        wg = FindObjectOfType<WorldGeneration>();
        bc = FindObjectOfType<BuildingController>();
        gm = FindObjectOfType<GameManager>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.position;
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        crystalInHand = eventData.pointerDrag.GetComponent<RectTransform>().gameObject;
        itemInHandUpgrade = eventData.pointerDrag.GetComponent<RectTransform>().gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        rectTransform.position = startPosition;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        crystalInHand = null;
        itemInHandUpgrade = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.tag == "HexItem")
        {
            wg.SpawnChunkWithItem(gameObject.GetComponent<HexBought>().hexIndex);
            gm.spawnedCrystals.Remove(gameObject);
            gm.RefreshShopSlots(int.Parse(gameObject.transform.name));
            Destroy(this.gameObject);
        }
        else if (gameObject.tag == "TowerItem")
        {
            bc.BoughtATower();
            gm.spawnedCrystals.Remove(gameObject);
            gm.RefreshShopSlots(int.Parse(gameObject.transform.name));
            Destroy(this.gameObject);
        }
    }

    
}
