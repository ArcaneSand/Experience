
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager I;
    private void Awake() {
        I = this;
        root = new Room(startRoom,0,null);
    }
    [Header("RoomGenerate")]
    public Room root;
    public RoomSO[] BluePrintListDept1to5;
    public RoomSO[] BluePrintListDept6to10;
    public RoomSO[] BluePrintListDept11to15;
    public RoomSO[] BluePrintListDept15to20;
    public RoomSO startRoom;
    public RoomSO exitRoom;
    public List<Room> RoomList;
    public int exitDept;

    [Header("LoadRoom")]
    public TextMeshProUGUI RoomName;
    public Image RoomImage;
    public Room currentRoom;
    private void Start() {
        
    }
    public void Reset(){
        currentRoom = root;
        BuildRoom();
        LoadRoom(currentRoom);
    }
    private void OnEnable() {
        GameManager.CloneSequence += Reset;
    }
    private void OnDisable() {
        GameManager.CloneSequence -= Reset;
    }
    public void BuildRoom(){
        root.left = BuildRoomRecursive(1,root);
        root.right = BuildRoomRecursive(1,root);
    }
    public Room BuildRoomRecursive(int dept,Room preRoom){
        if(dept >= exitDept){
            return null;
        }

        Room room = new Room(GetRandomBluePrint(dept),dept,preRoom);
        room.left = BuildRoomRecursive(dept+1,room);
        room.right = BuildRoomRecursive(dept+1,room);

        return room;
    }
    public RoomSO GetRandomBluePrint(int dept){
        if(dept<6){
            int index = UnityEngine.Random.Range(0,BluePrintListDept1to5.Length);
            return BluePrintListDept1to5[index];
        }else if(dept<11){
            int index = UnityEngine.Random.Range(0,BluePrintListDept6to10.Length);
            return BluePrintListDept6to10[index];
        }else if(dept<16){
            int index = UnityEngine.Random.Range(0,BluePrintListDept11to15.Length);
            return BluePrintListDept11to15[index];
        }else{
            return exitRoom;
        }
    }
    public void LoadRoom(Room room){
        RoomImage.sprite = room.data.RoomSprite;
        RoomName.text = $"{room.data.roomName} {room.dept}";

        CombatManager.I.DeSpawnEnemy();
        LootManager.I.RemoveLoot();

        if(!room.isVisited){
            room.isVisited = true;
        }

        if(room.enemy!=null){
            CombatManager.I.SpawnEnemy(room.enemy);
        }
        if(room.lootItems!=null&&room.lootItems.Count!=0){
            LootManager.I.InitiateLoot(room);
        }
    }
    public void WalkLeft(){
         exit(currentRoom.dept);
        Step(10);
        currentRoom = currentRoom.left;
        LoadRoom(currentRoom);
        
    }
    public void WalkRight(){
        exit(currentRoom.dept);
        Step(10);
        currentRoom = currentRoom.right;
        LoadRoom(currentRoom);
        
    }
    public void WalkBack(){
        if(currentRoom.dept==0)return;
        Step(10);
        currentRoom = currentRoom.previous;
        LoadRoom(currentRoom);
        
    }
    public void WalkRoot(){
        Step(10*currentRoom.dept);
        currentRoom = root;
        LoadRoom(currentRoom);
        GameManager.I.Save(CharactorManager.I.skills);
        
    }

    public void Step(int value){
        CharactorManager.I.IncreaseSkill(2,Convert.ToInt32(value));
        CharactorManager.I.addHunger(-value);
        if(GameManager.state == PlayerState.combat){
            CombatManager.I.EnemyAttack();
        }
    }

    public void exit(int d){
        if(d+1==exitDept){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            return;
        }
    }

}
