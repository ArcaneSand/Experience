using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharactorManager : MonoBehaviour
{
    public static CharactorManager I;

    public int maxHP;
    public int HP;
    public Image hpBar;
    public TextMeshProUGUI hpText;
    public int maxHunger;
    public int Hunger;
    public Image hungerBar;
    public TextMeshProUGUI hungerText;
    public int maxSanity;
    public int Sanity;
    public Image sanityBar;
    public TextMeshProUGUI sanityText;
    public int xpModifier;

    private void Awake() {
        if(I == null){
            I = this;
        }else{
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = (float)HP/maxHP;
        hungerBar.fillAmount = (float)Hunger/maxHunger;
        sanityBar.fillAmount = (float)Sanity/maxSanity;

        hpText.text = $"{HP}/{maxHP}";
        hungerText.text = $"{Hunger}/{maxHunger}";
        sanityText.text = $"{Sanity}/{maxSanity}";

        UpdateSkill();
    }

    public void addHP(float value){
        HP= Mathf.RoundToInt(HP+value);
        if(HP>maxHP)HP=maxHP;
        if(HP<=0){
            GameManager.I.changePlayerState(PlayerState.dead);
        }
    }
    public void addHunger(float value){
        Hunger= Mathf.RoundToInt(Hunger+value);
        if(Hunger>maxHunger)Hunger=maxHunger;
        if(Hunger<0){
            addHP(Hunger);
            Hunger = 0;
        }
    }
    public void addSanity(float value){
        Sanity= Mathf.RoundToInt(Sanity+value);
        if(Sanity>maxSanity)Sanity=maxSanity;
        if(Sanity<0){
            addHP(Sanity*10);
            Sanity=0;
        }
    }
    public void ReClone(){
        HP = maxHP;
        Hunger = maxHunger;
        Sanity = maxSanity;
        UpdateSkill();
    }
    
    private void OnEnable() {
        GameManager.CloneSequence += ReClone;
    }
    private void OnDisable() {
        GameManager.CloneSequence -= ReClone;
    }
//--------------------------------------------------------------------------------------------------SKILL Manager---------------------------------------------
    [Header("SKILL Manager")]
    /*public int Combat;
    public TextMeshProUGUI CombatLevel;
    public int CombatXP;
    public int Herbalist;
    public TextMeshProUGUI HerbalistLevel;
    public int HerbalistXP;
    public int Stamina;
    public TextMeshProUGUI StaminaLevel;
    public int StaminaXP;
    public int Agility;
    public TextMeshProUGUI AgilityLevel;
    public int AgilityXP;
    public int Crafting;
    public TextMeshProUGUI CraftingLevel;
    public int CraftignXP;
    public int WillPower;
    public TextMeshProUGUI WillPowerLevel;
    public int WillPowerXP;
    public int Mapping;
    public TextMeshProUGUI MappingLevel;
    public int MappingXP;
    public int Trap;
    public TextMeshProUGUI TrapLevel;
    public int TrapXP;*/
    public List<Image> xpBar;
    public List<TextMeshProUGUI> skillTexts;
    public List<Skill> skills;
    public void UpdateSkill(){
        for(int i=0; i<xpBar.Count; i++){
            xpBar[i].fillAmount = (float)skills[i].SkillXp / 100;
            skillTexts[i].text = skills[i].SkillLv.ToString();
        }
    }



    public void IncreaseSkill(int pos,int value){
        Skill s = skills[pos];
        s.IncreaseSkill(value*xpModifier);
        xpBar[pos].fillAmount = s.SkillXp/100f;

        if(pos==2){
            maxHunger = 100 + (skills[2].SkillLv*20);
        }
    }
    public void ReClone(List<Skill> skills)
    {
        Debug.Log(skills[2].SkillLv);
        this.skills = skills.Select(skill => new Skill(skill.SkillXp, skill.SkillLv)).ToList(); 
        UpdateSkill();
    }
//------------------------------------Equipment-------------------------------------------------
    public int Atk;
    public int Def;
}
[Serializable]
public class Skill{
    public int SkillXp;
    public int SkillLv;
    public Skill(){
        SkillXp = 0;
        SkillLv = 0;
    }
    public Skill(int x,int y){
        SkillXp = x;
        SkillLv = y;
    }
    public void IncreaseSkill(int value){
        SkillXp += value;
        if(SkillXp>=100){
            SkillLv++;
            SkillXp -=100;
        }
    }

}


