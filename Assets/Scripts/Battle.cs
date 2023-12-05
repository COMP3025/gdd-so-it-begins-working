using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Battle : MonoBehaviour, IPointerDownHandler
{
    private List<ItemOn> enemySlots = new List<ItemOn>();
    private List<ItemOn> playerSlots = new List<ItemOn>();

    void Start()
    {
        playerSlots = PopulateSlots("Slot");
        enemySlots = PopulateSlots("EnemySlots");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        List<DragDrop> playerItens = new List<DragDrop>();
        PopulateItensOnSlots(playerSlots, playerItens);

        List<DragDrop> enemyItens = new List<DragDrop>();
        PopulateItensOnSlots(enemySlots, enemyItens);

        StartCoroutine(MoveToTargetPositionAndFight(playerItens, enemyItens));
    }

    IEnumerator MoveToTargetPositionAndFight(List<DragDrop> playerItens, List<DragDrop> enemyItens)
    {
        bool EnemyTurn = false;
        while (enemyItens.Count() > 0 && playerItens.Count() > 0)
        {
            DragDrop primeiroItemPlayer = playerItens.ElementAt(0);
            DragDrop primeiroItemInimigo = enemyItens.ElementAt(0);
            if (EnemyTurn)
            {
                EnemyTurn = false;
                // Mova para a posição antes de lutar
                yield return StartCoroutine(MoveToTargetPosition(primeiroItemInimigo.GetComponent<RectTransform>(), primeiroItemPlayer.GetComponent<RectTransform>()));

                yield return StartCoroutine(ShakeItem(primeiroItemInimigo.GetComponent<RectTransform>(), 0.5f, 5f)); // Ajuste a duração e intensidade conforme necessário
                yield return StartCoroutine(ShakeItem(primeiroItemPlayer.GetComponent<RectTransform>(), 0.5f, 5f));
            }
            else
            {
                EnemyTurn = true;
                // Mova para a posição antes de lutar
                yield return StartCoroutine(MoveToTargetPosition(primeiroItemPlayer.GetComponent<RectTransform>(), primeiroItemInimigo.GetComponent<RectTransform>()));

                yield return StartCoroutine(ShakeItem(primeiroItemPlayer.GetComponent<RectTransform>(), 0.5f, 5f)); // Ajuste a duração e intensidade conforme necessário
                yield return StartCoroutine(ShakeItem(primeiroItemInimigo.GetComponent<RectTransform>(), 0.5f, 5f));
            }

            Fight(primeiroItemPlayer, primeiroItemInimigo);

            if (primeiroItemPlayer.CurrentVida < 1)
            {
                // Remova da lista e desative o objeto
                playerItens.RemoveAt(0);
                primeiroItemPlayer.gameObject.SetActive(false);
            }
            else
            {
                primeiroItemPlayer.GetComponent<RectTransform>().anchoredPosition = primeiroItemPlayer.initialPosition;
            }
            if (primeiroItemInimigo.CurrentVida < 1)
            {
                // Remova da lista e desative o objeto
                enemyItens.RemoveAt(0);
                primeiroItemInimigo.gameObject.SetActive(false);
            }
            else
            {
                primeiroItemInimigo.GetComponent<RectTransform>().anchoredPosition = primeiroItemInimigo.initialPosition;
            }
        }

        if (enemyItens.Count() > 0)
        {
            Debug.Log("Vitória do inimigo");
        }
        else if (playerItens.Count() > 0)
        {
            Debug.Log("Vitória do jogador");
        }
        else
        {
            Debug.Log("Empate");
        }

        // Ao final da batalha, retorne todos os itens à posição inicial
        ReturnItemsToInitialPosition();
    }

    IEnumerator MoveToTargetPosition(RectTransform playerTransform, RectTransform enemyTransform)
    {
        float t = 0f;
        Vector3 from = playerTransform.anchoredPosition;
        Vector3 to = enemyTransform.anchoredPosition;

        // Distância mínima para evitar sobreposição
        float minDistance = 100f;

        while (t < 1f)
        {
            t += Time.deltaTime / 1.0f;
            playerTransform.anchoredPosition = Vector3.Lerp(from, to, t);

            // Verifique a distância entre os itens
            float distance = Vector2.Distance(playerTransform.anchoredPosition, enemyTransform.anchoredPosition);

            // Se a distância for menor que a distância mínima, ajuste a posição final
            if (distance < minDistance)
            {
                Vector3 direction = (to - from).normalized;
                playerTransform.anchoredPosition = to - direction * minDistance * t;
            }

            yield return null;
        }
    }

    private void Fight(DragDrop player, DragDrop inimigo)
    {
        player.atualizarVida(inimigo.ataque);
        inimigo.atualizarVida(player.ataque);
    }

    public void ReturnItemsToInitialPosition()
    {
        List<DragDrop> items = new List<DragDrop>();
        PopulateItensOnSlots(playerSlots, items);
        PopulateItensOnSlots(enemySlots, items);

        foreach (DragDrop item in items)
        {
            RectTransform itemTransform = item.GetComponent<RectTransform>();
            Vector2 initialPosition = item.initialPosition;

            // Reinicie a vida e reative o objeto
            item.resetarVida();
            item.gameObject.SetActive(true);
            itemTransform.anchoredPosition = initialPosition;
        }
    }

    public List<ItemOn> PopulateSlots(string Tag)
    {
        List<ItemOn> ListToPopulate = new List<ItemOn>();
        List<GameObject> SlotsObject = GameObject.FindGameObjectsWithTag(Tag).ToList();
        foreach (GameObject slot in SlotsObject)
        {
            ItemOn curSlot = slot.GetComponent<ItemOn>();
            if (curSlot != null)
            {
                ListToPopulate.Add(curSlot);
            }
        }
        return ListToPopulate.OrderBy(e => e.position).ToList();
    }

    public void PopulateItensOnSlots(List<ItemOn> Slots, List<DragDrop> ListToPopulate)
    {
        foreach (ItemOn slot in Slots)
        {
            if (slot.itemOnSlot != null)
            {
                ListToPopulate.Add(slot.itemOnSlot);
            }
        }
    }

    IEnumerator ShakeItem(RectTransform itemTransform, float duration, float intensity)
    {
        Vector3 originalPosition = itemTransform.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + Mathf.Sin(Time.time * intensity) * 10f; // Ajuste o valor "10f" para controlar a intensidade do shake
            float y = originalPosition.y;

            itemTransform.anchoredPosition = new Vector2(x, y);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Certifique-se de definir a posição exata para evitar pequenos desvios
        itemTransform.anchoredPosition = originalPosition;
    }
}
