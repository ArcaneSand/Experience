using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionManager : MonoBehaviour
{   
    public static ActionManager I;
    [SerializeField] private Button[] _button;
    // Start is called before the first frame update
    private void Awake() {
        I = this;
    }
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackButton(){
        CombatManager.I.Attack(CharactorManager.I.Atk);
    }
}
