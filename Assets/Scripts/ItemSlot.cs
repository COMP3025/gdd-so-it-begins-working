using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : ItemOn, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop Item on slot");

        if (eventData.pointerDrag != null)
        {
            DragDrop dragDropInstance = eventData.pointerDrag.GetComponent<DragDrop>();
            Status[] validStatus = { Status.Spawn, Status.Slot };
            dragDropInstance.MoveDropDown(this, validStatus, Status.Slot);
        }
    }
}
