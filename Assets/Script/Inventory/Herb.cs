using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Herb")]
public class Herb : Item
{
    public int herbalistLevel;
    public int healAmount;
    public override void Click()
    {
        CharactorManager.I.addHP(healAmount);
        if(herbalistLevel > CharactorManager.I.skills[1].SkillLv){
            CharactorManager.I.IncreaseSkill(1,20);
        }else if(herbalistLevel ==CharactorManager.I.skills[1].SkillLv){
            CharactorManager.I.IncreaseSkill(1,10);

        }
    }

    public override string ToolTip()
    {
        string tip = "";
        string prefix = "";
        string detail = "???";
        if(CharactorManager.I.skills[1].SkillLv >= herbalistLevel){
            if(IsHerb()){
                prefix = "Healing ";
                detail = $"heal: {healAmount}";
            }else{
                prefix = "Poisonious ";
                detail = $"damage: {healAmount}";

            }
        }
        tip = $"{prefix}Herb\n\nHerbalistLV: {herbalistLevel}\n\n{detail}";
        return tip;
    }

    private bool IsHerb(){
        if(healAmount > 0){
            return true;
        }
        return false;
    }
}
