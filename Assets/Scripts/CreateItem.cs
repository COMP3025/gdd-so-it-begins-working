using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    public Camera cam = null;
    public GameObject target;
    public GameObject targetArea;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InstantiateObject();
    }

    public void InstantiateObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Transform parentTransform = GameObject.FindGameObjectWithTag("HierarchyItens").transform;

            GameObject newInstantiate = Instantiate(target, Vector2.zero, Quaternion.identity);

            newInstantiate.transform.SetParent(parentTransform);
            newInstantiate.transform.localScale = new Vector2(1, 1); // change its local scale in x y z format
        }
    }
}
