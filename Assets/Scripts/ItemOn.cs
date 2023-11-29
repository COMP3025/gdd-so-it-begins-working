using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOn : MonoBehaviour
{
    public DragDrop itemOnSlot;

    // Start is called before the first frame update
    void Start()
    {
        if (itemOnSlot != null)
        {
            itemOnSlot.itemSlot = this;
            itemOnSlot.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
