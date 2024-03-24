using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/room")]
public class RoomSO : ScriptableObject
{
    public string roomName;
    public Sprite RoomSprite;
    public RoomType[] RoomType;
    public EnemySO[] enemyTable;
    public Item[] itemTable;
    public  TrapSO[] trapTable;
}
[Serializable]
public enum RoomType{
    combat,loot,trap
}
