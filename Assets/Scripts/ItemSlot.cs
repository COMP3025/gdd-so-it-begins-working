/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

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
            if (dragDropInstance != null)
            {
                string status = dragDropInstance.ObterStatus();

                if (status == "spawn" | status == "slot")
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    itemOnSlot = eventData.pointerDrag;
                    DragDrop.onSlot = true;

                    dragDropInstance.ModificarEstadoDoObjeto("slot", this);
                }
            }
        }
    }

}
