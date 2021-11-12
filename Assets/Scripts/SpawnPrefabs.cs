using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabs : MonoBehaviour
{
    private GameObject PlayerClara;
    public GameObject empty_jar_prefab;
    // Start is called before the first frame update
    void Start()
    {
        PlayerClara = GameObject.Find("Clara");

        empty_jar_prefab.GetComponent<MeshCollider>().convex = true;
        empty_jar_prefab.GetComponent<MeshCollider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnThis(Item item)
    {
        switch (item.itemName)
        {
            default: break;
            case Item.ItemName.EmptyJar:
                Instantiate(empty_jar_prefab, PlayerClara.transform.position, Quaternion.Euler(new Vector3(-90,0,0)));
                break;
            case Item.ItemName.WaterJar:
                Instantiate(empty_jar_prefab, PlayerClara.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
                break;
        }
    }
}
