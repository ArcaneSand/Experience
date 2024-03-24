using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager I;

    public GameObject inventoryPrefab;
    private void Awake() {
        I = this;
    }
    public Slot[] slots;
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

    public bool AddLoot(GameObject loot){
        for(int i = 0; i < slots.Length; i++){
            Slot slot = slots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null){
                loot.transform.SetParent(slot.transform);
                loot.transform.position = slot.transform.position;
                return true;

            }
        }
        return false;
    }

    public void ClearInventory(){
        for(int i = 0; i < slots.Length; i++){
            Slot slot = slots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null){
                Destroy(itemInSlot.gameObject);
            }
        }
    }
    
    private void OnEnable() {
        GameManager.CloneSequence += ClearInventory;
    }
    private void OnDisable() {
        GameManager.CloneSequence -= ClearInventory;
    }

}
