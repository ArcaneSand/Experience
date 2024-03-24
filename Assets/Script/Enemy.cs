using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemySO data;
    public string Name;
    public int Atk;
    public int maxHp;
    public int currentHp;
    public int combatLevel;
    public Sprite sprite;
    public string tip;

    public Image s;

    public void setEnemy(EnemySO eso){
        data = eso;
        Name = eso.Name;
        Atk = eso.Atk;
        maxHp = eso.maxHp;
        currentHp = maxHp;    
        combatLevel = eso.combatLv;
        tip = eso.tip;
        sprite = eso.image;;
        s.sprite = sprite;
    }
    private void Awake() {
        s = GetComponent<Image>();
    }
    public bool takeDamage(int damage) {
        currentHp -=damage;
        if(currentHp<=0){
            Destroy(gameObject);
            return true;
        }else{
            return false;
        }
    }
}
