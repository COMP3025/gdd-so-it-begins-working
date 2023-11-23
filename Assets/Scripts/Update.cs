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
        List<GameObject> itens = GameObject.FindGameObjectsWithTag("Item").ToList();

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
            Bucket currentBucket = bucket.GetComponent<Bucket>();
            itens.ForEach(e =>
            {
                DragDrop dragDrop = e.GetComponent<DragDrop>();
                currentBucket.itensOnBucket.Add(dragDrop);
            });
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
        if (currentBucket != null & spawns != null)
        {
            spawns.ForEach(e =>
            {
                Debug.Log(e.GetComponent<Spawn>().itemOnSlot != null);
            });

            spawns.ForEach(e =>
            {
                Spawn curSpawn = e.GetComponent<Spawn>();
                if (curSpawn.itemOnSlot == null && currentBucket.itensOnBucket.Count > 0)
                {
                    // Gerando um índice aleatório com base no tamanho da lista
                    System.Random random = new System.Random();
                    int indiceAleatorio = random.Next(currentBucket.itensOnBucket.Count);

                    DragDrop item = currentBucket.itensOnBucket[indiceAleatorio];

                    curSpawn.itemOnSlot = item.GetComponent<DragDrop>();

                    item.GetComponent<RectTransform>().anchoredPosition = curSpawn.GetComponent<RectTransform>().anchoredPosition;
                    item.status = Status.Spawn;
                    item.itemSlot = curSpawn;
                    item.initialPosition = item.GetComponent<RectTransform>().anchoredPosition;
                    currentBucket.itensOnBucket.RemoveAt(indiceAleatorio);
                }
            });
        }

    }
}
