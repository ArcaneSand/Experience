using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrapManager : MonoBehaviour
{
   public static TrapManager I;
    private void Awake() {
        I = this;
    }
    
    [Header("SetUp")]
    public TextMeshProUGUI trapStat;
    public GameObject trapPrefab;
    public Transform trapParent;
 
}
