using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Weapon")]
public class Weapon : Item
{
    public int damage;
    public override void Click()
    {
        EquipmentManager.I.SetWeapon(this);
    }

    public override string ToolTip()
    {
        string tip = $"Sharp Weapon\n\n damage {damage}";
        return tip;
    }
}
