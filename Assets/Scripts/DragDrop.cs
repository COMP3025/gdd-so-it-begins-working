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
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Status
{
    Bucket,
    Slot,
    Spawn,
}

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector2 initialPosition;
    public bool onSlot;
    public Status status = Status.Bucket;
    public ItemOn itemSlot;
    public GameObject target;
    public GameObject targetVida;

    public int ataque;
    public int vida;

    private void Start()
    {
        GameObject newInstantiateAtaque = Instantiate(target, transform.position, transform.rotation);
        newInstantiateAtaque.transform.SetParent(rectTransform.GetComponent<Transform>());
        newInstantiateAtaque.transform.localScale = new Vector2(1, 1);
        TextMeshProUGUI textMeshAtaque = newInstantiateAtaque.GetComponent<TextMeshProUGUI>();
        textMeshAtaque.text = ataque.ToString();

        GameObject newInstantiateVida = Instantiate(targetVida, transform.position, transform.rotation);
        newInstantiateVida.transform.SetParent(rectTransform.GetComponent<Transform>());
        newInstantiateVida.transform.localScale = new Vector2(1, 1);
        TextMeshProUGUI textMeshVida = newInstantiateVida.GetComponent<TextMeshProUGUI>();
        textMeshVida.text = vida.ToString();
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
        onSlot = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
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
        //Debug.Log("OnEndDrag");
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


    public void ModificarEstadoDoObjeto(Status novoEstado)
    {
        status = novoEstado;
    }

    public Status ObterStatus()
    {
        return status;
    }

    public void MoveDropDown(ItemOn item, Status[] validStatus, Status goTo)
    {
        if (validStatus.ToList().Contains(status))
        {
            GetComponent<RectTransform>().anchoredPosition = item.GetComponent<RectTransform>().anchoredPosition;

            if (itemSlot != null)
            {
                itemSlot.itemOnSlot = null;
            }
            itemSlot = item;
            itemSlot.itemOnSlot = this;

            onSlot = true;
            status = goTo;
        }
    }
}
