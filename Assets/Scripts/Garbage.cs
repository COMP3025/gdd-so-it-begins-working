using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Garbage : MonoBehaviour, IDropHandler
{
    private GameObject bucket;
    // Start is called before the first frame update
    void Start()
    {
        bucket = GameObject.FindWithTag("Bucket");

        // Verificar se o objeto foi encontrado
        if (bucket != null)
        {
            // Fazer alguma coisa com o objeto encontrado
            Debug.Log("Objeto encontrado: " + bucket.name);
        }
        else
        {
            // Avisar se o objeto não foi encontrado
            Debug.LogWarning("Nenhum objeto encontrado com a tag 'MinhaTag'");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop Garbage");
        if (eventData.pointerDrag != null)
        {
            DragDrop dragDropInstance = eventData.pointerDrag.GetComponent<DragDrop>();
            if (dragDropInstance != null)
            {
                string status = dragDropInstance.ObterStatus();
                if (status == "slot")
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = bucket.GetComponent<RectTransform>().anchoredPosition;
                    dragDropInstance.ModificarEstadoDoObjeto("bucket");

                    Bucket currentBucket = bucket.GetComponent<Bucket>();

                    currentBucket.itensOnBucket.Add(dragDropInstance);
                }
            }
        }
    }

}
