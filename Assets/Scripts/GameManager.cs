using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    [Header("Params TP")]
    [SerializeField] float rangeXPackage;
    [SerializeField] float rangeYPackage;
    [SerializeField] float rangeXCustomer;
    [SerializeField] float rangeYCustomer;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] GameObject endingScreen;
    [Header("SFX")]
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip theme;
    [SerializeField] public bool restingPhase = true;
    [Header("Components")]
    AudioSource audioGM;
    Timer timer;
    PlayerController player;

    int score;
    void Awake() {
        audioGM = GetComponent<AudioSource>();
        timer = FindObjectOfType<Timer>();
        player = FindObjectOfType<PlayerController>();
        scoreText.text = "Packages delivered : 0";
    }

    void Start() {
        audioGM.PlayOneShot(intro);
    }
    int GetScore() {
        return score;
    }

    public void IncrementScore() {
        score++;
        scoreText.text = "Packages delivered : " + score;
        if(score > 0 && restingPhase) {
            restingPhase = false;
            audioGM.Stop();
            audioGM.PlayOneShot(theme);
        }
    }

    public void TeleportPackage(GameObject package) {
        float randX, randY;
        randX = Random.Range(-20, 20);
        randY = Random.Range(-10, 10);
        package.transform.position = new Vector3(randX, randY, 0);
    }

    public void TeleportCustomer(GameObject customer) {
        float randomRange = Random.value > 0.5 ? 1.0f : -1.0f;
        if(randomRange == 1.0f) {
            float randX, randY;
            randX = Random.Range(-20, 20);
            randY = Random.Range(-5, 5);
            customer.transform.position = new Vector3(randX, randY, 0);

        }
    }

    public void HandleEndgame() {
        Debug.Log("Fin du timer");
        player.hasGameFinished = true;
        endingScreen.SetActive(true);
        audioGM.Stop();
        audioGM.PlayOneShot(intro);
        finalScoreText.text = "Final score: \n \n" + score + " package.s delivered.";

    }

    public void Replay() {
        Invoke("ReloadScene", 1.0f);
    }

    public void ReloadScene() {
        SceneManager.LoadScene(0);
    }

}
