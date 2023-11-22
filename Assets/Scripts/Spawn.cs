using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spawn : ItemOn, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop on Spawn");

        if (eventData.pointerDrag != null)
        {
            DragDrop dragDropInstance = eventData.pointerDrag.GetComponent<DragDrop>();
            if (dragDropInstance != null)
            {
                string status = dragDropInstance.ObterStatus();

                if (status == "spawn")
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    itemOnSlot = eventData.pointerDrag;
                    DragDrop.onSlot = true;

                    dragDropInstance.ModificarEstadoDoObjeto("spawn", this);
                }
            }
        }
    }
}
