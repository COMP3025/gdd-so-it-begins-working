using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Update : MonoBehaviour, IPointerDownHandler
{
    private GameObject bucket;
    private List<GameObject> spawns;

    // Start is called before the first frame update
    void Start()
    {
        bucket = GameObject.FindWithTag("Bucket");
        spawns = GameObject.FindGameObjectsWithTag("Spawn").ToList();
        // Verificar se o objeto foi encontrado
        if (spawns != null)
        {
            // Fazer alguma coisa com o objeto encontrado
            Debug.Log("Objeto encontrado: spawns ");
        }
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

    public void OnPointerDown(PointerEventData eventData)
    {
        Bucket currentBucket = bucket.GetComponent<Bucket>();
        if (currentBucket != null)
        {
            Debug.Log(currentBucket.itensOnBucket.Count);
            spawns.ForEach(e =>
            {
                Debug.Log(e.GetComponent<Spawn>().itemOnSlot != null);
            });
        }

    }
}
