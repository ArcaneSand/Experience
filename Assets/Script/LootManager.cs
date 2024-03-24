using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager I;

    public GameObject inventoryPrefab;
    public Room currentRoom;
    private void Awake() {
        I = this;
    }
    public Slot[] slots;

    public void InitiateLoot(Room room){
        currentRoom = room;
        List<Item> items = currentRoom.lootItems;
        foreach(Item item in items){
            AddItem(item);
        }
    }

    public bool AddItem(Item item){
        for(int i = 0; i < slots.Length; i++){
            //Debug.Log(i);
            Slot slot = slots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null){
                SpawnNewItem(item,slot);
                return true;

            }
        }
        return false;
    }

    void SpawnNewItem(Item item,Slot slot){
        GameObject newItem = Instantiate(inventoryPrefab,slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public void RemoveLoot(){
        if(currentRoom==null)return;
        List<Item> listItems = new List<Item>();
        for(int i = 0; i < slots.Length; i++){
            Slot slot = slots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null){
                listItems.Add(itemInSlot.item);
                Destroy(itemInSlot.gameObject);
            }
        }
        currentRoom.lootItems = listItems;
    }
    
}
