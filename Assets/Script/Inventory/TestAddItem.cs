using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TestAddItem : MonoBehaviour
{
    public Item[] item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawnItem(){
        int Rv= Random.Range(0,item.Length);
        InventoryManager.I.AddItem(item[Rv]);
    }
}
