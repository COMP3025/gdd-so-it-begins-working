/* 
------------------- Code Monkey -------------------

Thank you for downloading this package
I hope you find it useful in your projects
If you have any questions let me know
Cheers!

           unitycodemonkey.com
--------------------------------------------------
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;
    public static bool onSlot;
    public string status = "spawn";

    public ItemOn itemSlot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
        onSlot = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (!onSlot)
        {
            rectTransform.anchoredPosition = initialPosition;
        }
        else
        {
            initialPosition = rectTransform.anchoredPosition;

        }
        onSlot = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void ModificarEstadoDoObjeto(string novoEstado, ItemOn item)
    {
        status = novoEstado;
        if (itemSlot != null)
        {
            itemSlot.itemOnSlot = null;
        }

        itemSlot = item;
    }

    public void ModificarEstadoDoObjeto(string novoEstado)
    {
        status = novoEstado;
    }


    public string ObterStatus()
    {
        return status;
    }

}
