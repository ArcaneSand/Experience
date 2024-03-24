using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/itemSO")]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite[] sprite;

    public virtual void Click(){}

    public virtual string ToolTip(){
        return null;
    }
}
