using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float playtime = 60f;
    [SerializeField] TextMeshProUGUI timerText;

    GameManager gM;
    PlayerController player;

    void Awake() {
        gM = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
    }


    void Update()
    {
        if(!gM.restingPhase) {
            playtime -= Time.deltaTime;
            timerText.text = "Time left : " + Mathf.Round(playtime) + " secs.";
            if(playtime <= 0) {
                gM.restingPhase = true;
                gM.HandleEndgame();
            }
        } 
    }

}
