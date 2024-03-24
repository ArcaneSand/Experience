using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager I;
    public static PlayerState state;
    private void Awake() {
        I = this;
        state = PlayerState.stand;
    }

    public int Circle;
    public TextMeshProUGUI circleText;
    public GameObject AtkButton;
    public GameObject InventoryButton;
    //public GameObject WalkButton;

    public GameObject DeathCanvas;

    [Header("Internal Use")]
    public List<Skill> cloneSkills;
    public static Action CloneSequence;

    void Start()
    {
        Circle = 0;
        InitSkill();
        CloneSequence();
        DeathCanvas.SetActive(false);
    }

    public void changePlayerState(PlayerState s){
        state = s;
        switch (state){
            case PlayerState.home:
                break;
            case PlayerState.stand:
                SetButton(false,true,true);
                break;
            case PlayerState.combat:
                SetButton(true,false,true);
                break;
            case PlayerState.wait:
                break;
            case PlayerState.dead:
                SetButton(false,false,false);
                playDead();
                Invoke(nameof(CloneSequenceDelay),5f);
                break;
            default:
                break;
        }
    }
    public void CloneSequenceDelay(){
        CloneSequence();
    }
    public void SetButton(bool a,bool b,bool c){
        AtkButton.SetActive(a);
        InventoryButton.SetActive(b);
        //WalkButton.SetActive(c);
    }
    public void Save(List<Skill> skills){
        cloneSkills = skills.Select(skill => new Skill(skill.SkillXp, skill.SkillLv)).ToList(); 
        CharactorManager.I.addSanity(-50);
    }
    public void Clone()
    {
        state = PlayerState.stand;
        CharactorManager.I.ReClone(cloneSkills);
        Circle++;
        circleText.text = Circle.ToString();
    }
    private void OnEnable() {
        GameManager.CloneSequence += Clone;
    }
    private void OnDisable() {
        GameManager.CloneSequence -= Clone;
    }
    public void InitSkill(){
        for(int i=0;i<8;i++){
            cloneSkills.Add(new Skill());
        }
    }

    public void playDead(){
        DeathCanvas.SetActive(true);
        Invoke(nameof(stopPlayDead),5f);
    }
    public void stopPlayDead(){
        DeathCanvas.SetActive(false);
    }
}

public enum PlayerState{
    home,stand,combat,wait,dead
}