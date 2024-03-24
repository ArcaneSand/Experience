using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;
    private void OnEnable() {
        text1.SetActive(true);
        text2.SetActive(false);
        Invoke("Timer",3f);
    }
    private void Timer(){
        text1.SetActive(false);
        text2.SetActive(true);
    }


}
