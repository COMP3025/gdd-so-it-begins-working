using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Battle : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    private List<GameObject> enemySlots;
    private List<GameObject> playerSlots;

    void Start()
    {
        enemySlots = GameObject.FindGameObjectsWithTag("EnemySlots").ToList();
        playerSlots = GameObject.FindGameObjectsWithTag("Slot").ToList();
        if (playerSlots != null)
        {
            Debug.Log("playerSlots vazio");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playerSlots.ForEach(e =>
        {
            var curSlot = e.GetComponent<ItemOn>();
            if (curSlot != null)
            {
                if (curSlot.itemOnSlot != null)
                {
                    Debug.Log("curSlot cheion no player");
                }
            }
            else
            {
                Debug.Log("nao achou slot");
            }
        });

        enemySlots.ForEach(e =>
        {
            var curSlot = e.GetComponent<ItemOn>();
            if (curSlot != null)
            {
                if (curSlot.itemOnSlot != null)
                {
                    Debug.Log("curSlot cheion no enemy player");
                }
            }
            else
            {
                Debug.Log("nao achou slot");
            }
        });

    }
}
