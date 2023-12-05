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
            Status[] validStatus = { Status.Spawn };
            dragDropInstance.MoveDropDown(this, validStatus, Status.Spawn);
        }
    }
}
