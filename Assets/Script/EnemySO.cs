using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "Mon",menuName ="SO/Enemy")]
public class EnemySO : ScriptableObject
{
    public string Name;
    public int maxHp;
    public int Atk;
    public int combatLv;
    public Sprite image;
    [TextArea(5,10)]
    public string tip;
    
}
