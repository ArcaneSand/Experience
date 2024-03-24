using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room 
{
    public Room left;
    public Room right;
    public Room previous;
    public RoomSO data;
    public int dept;
    public bool isVisited = false;
    public RoomType type;

    public EnemySO enemy;
    public TrapSO trap;
    public List<Item> lootItems;

    public Room(RoomSO rso,int roomDept,Room previousRoom){
        data = rso;
        dept = roomDept;
        previous = previousRoom;
        if(data.RoomType.Length!=0){
            InitiateRoom();
        }
    }

    public int rv(int maxExclude){
        return Random.Range(0,maxExclude);
    }
    void InitiateRoom(){
        //Debug.Log(rv(data.RoomType.Length)+"random value/" +data.RoomType.Length);
        type = data.RoomType[rv(data.RoomType.Length)];
        MonsterRoom();
    }

    void MonsterRoom(){
        if(data.enemyTable!=null&&data.enemyTable.Length!=0){
        if(type == RoomType.combat){
            enemy = data.enemyTable[rv(data.enemyTable.Length)];
        }else{
            enemy = null;
        }
        }
        TrapRoom();
    }

    void TrapRoom(){
        if(data.trapTable!=null&&data.trapTable.Length!=0){
        if(type == RoomType.trap){
            if(rv(11)>3){
                trap = data.trapTable[rv(data.trapTable.Length)];
            }
        }else if(type == RoomType.loot){
            if(rv(11)>7){
                trap = data.trapTable[rv(data.trapTable.Length)];
            }
        }else{
            trap = null;
        }
        }
        LootRoom();
    }
    
    void LootRoom(){
        if(data.itemTable!=null&&data.itemTable.Length!=0){
        if(type == RoomType.trap)return;
        int lootNum = 0;
        if(type == RoomType.combat){
            lootNum = 1;
        }else{
            lootNum = Random.Range(1,4);
        }
        lootItems = new List<Item>();
        for(int i=0;i<lootNum;i++){
            lootItems.Add(data.itemTable[rv(data.itemTable.Length)]);
        }
        }
    }
}
