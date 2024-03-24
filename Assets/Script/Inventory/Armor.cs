using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Armor")]
public class Armor : Item
{
    public int armor;

    public override void Click()
    {
        EquipmentManager.I.SetArmor(this);
        
    }

    public override string ToolTip()
    {
        string tip = $"Study Armor\n\n Armor {armor}";
        return tip;
    }
}
