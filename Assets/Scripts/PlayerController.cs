using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float speed = 20f;
    [SerializeField] float rotSpeed = 20f;

    [Header("Components")]
    [SerializeField] GameObject packageInsideCar;
    bool hasPackage = false;
    public bool hasGameFinished;

    GameManager gameManager;

    void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        HandlePlayerInput();
    }

    void HandlePlayerInput() {
        if(!hasGameFinished) {
            // if(Input.GetKey(KeyCode.LeftShift)) {
            //     speed += 10;
            // }
            float speedAmount = Time.deltaTime * speed * Input.GetAxis("Vertical");
            float rotAmount = Time.deltaTime * rotSpeed * Input.GetAxis("Horizontal");
            transform.Translate(0, speedAmount, 0);
            transform.Rotate(0, 0, -rotAmount);
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Package" && !hasPackage) {
            hasPackage = true;
            packageInsideCar.SetActive(true);
            gameManager.TeleportPackage(other.gameObject);
        } else if (other.gameObject.tag == "Customer" && hasPackage) {
            hasPackage = false;
            packageInsideCar.SetActive(false);
            gameManager.IncrementScore();
            gameManager.TeleportCustomer(other.gameObject);
        } else if (other.gameObject.tag == "Border") {
            this.gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }
}
