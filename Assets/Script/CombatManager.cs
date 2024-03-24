using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public static CombatManager I;
    private void Awake() {
        I = this;
        ToggleText(false);
    }
    [Header("SetUp")]
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI enemyStat;
    public TextMeshProUGUI enemyTip;
    public GameObject enemyPrefab;
    public Transform enemyParent;
    public int baseHitChance;
    public int baseBeHitChance;
    public EnemySO test;
    public int baseXp;

    [Header("Internal Use")]
    public Enemy enemy;
    public int hitChance;
    public int CritChance;
    public int beHitChance;
    public bool isEnemyDead;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(EnemySO eso){
        ToggleText(true);
        var instance = Instantiate(enemyPrefab,enemyParent,false);
        enemy = instance.GetComponent<Enemy>();
        enemy.setEnemy(eso);
        compareCombatLv();
        updateEnemyStat();
        enemyName.text =enemy.Name;
        enemyTip.text=enemy.tip;
        GameManager.I.changePlayerState(PlayerState.combat);
    }
    public void DeSpawnEnemy(){
        ToggleText(false);
        GameManager.I.changePlayerState(PlayerState.stand);
        enemy = enemyParent.GetComponentInChildren<Enemy>();
        if(enemy==null){return;}
        enemy.takeDamage(100000);
        enemy = null;
    }
    public void updateEnemyStat(){
        string t1 = $"HP:{enemy.currentHp}/{enemy.maxHp}     Atk:{enemy.Atk}\nCombatLV:{enemy.combatLevel}\nHitChance:{hitChance}/{beHitChance}";
        enemyStat.text = t1;
    }
    public void ToggleText(bool b){
        enemyName.gameObject.SetActive(b);
        enemyStat.gameObject.SetActive(b);
        enemyTip.gameObject.SetActive(b);
    }
    public void MonsterDead(){
        GameManager.I.changePlayerState(PlayerState.stand);
        ToggleText(false);
        int xp = baseXp;
        int dif = CharactorManager.I.skills[0].SkillLv - enemy.combatLevel;
        if(dif>2){
            xp = baseXp + 10;
        }else if(dif<=2&& dif>=-2){
            xp = baseXp;
        }else{
            xp = baseXp - 10;
        }
        //Debug.Log(xp);
        CharactorManager.I.IncreaseSkill(0,xp);
        enemy = null;
    }

    public void Attack(int damage){
        
        compareCombatLv();
        int RandomValue = Random.Range(0,101);
        //Debug.Log(RandomValue + " player " + hitChance);

        bool successHit = RandomValue<=hitChance;
        if(successHit){
            isEnemyDead = enemy.takeDamage(CharactorManager.I.Atk);
        }
        if(isEnemyDead){
            MonsterDead();
        }else{
            updateEnemyStat();
            StartCoroutine(AttackCircle());
        }

    }

    public void EnemyAttack(){
        
        compareCombatLv();
        int RandomValue = Random.Range(0,101);
        //Debug.Log(RandomValue + " enemy " + beHitChance);
        bool successHit = RandomValue<=beHitChance;
        if(successHit){
            int reduce = enemy.Atk - CharactorManager.I.Def;
            if(reduce<=5)reduce = 5;
            CharactorManager.I.addHP(-reduce);
        }else{
            CharactorManager.I.IncreaseSkill(3,20);
        }
    }

    public void compareCombatLv(){
        int diff = CharactorManager.I.skills[0].SkillLv - enemy.combatLevel;
        if(diff > 0){
            CritChance = 10 + (diff*5);
            if(diff>4){
                diff = 4;
            }
        }else{
            CritChance = 10;
            if(diff<-4){
                diff = -4;
            }
        }
        hitChance = baseHitChance + (10*diff);
        beHitChance = baseBeHitChance + (-10*diff) - dodgeChance();
        
    }
    IEnumerator AttackCircle(){
        yield return new WaitForSeconds(1f);
        EnemyAttack();
    }
    public int dodgeChance(){
        return 2*CharactorManager.I.skills[3].SkillLv;
    }
}
