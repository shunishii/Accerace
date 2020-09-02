using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speed = 30.0f;
    private float rangeX = 1.8f;
    public bool isGameover = false;
    public Button resetButton;
    public GameManager gameManager;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;

#if UNITY_EDITOR
        Vector3 keyArrow = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            keyArrow.x = -0.1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            keyArrow.x = 0.1f;
        }
        dir = keyArrow;
#else
        dir = Input.acceleration;
#endif

        if ((transform.position.x > rangeX && dir.x > 0) || (transform.position.x < -rangeX && dir.x < 0))
        {
            dir.x = 0;
        }

        if (gameManager.isGameActive)
        {
            dir *= Time.deltaTime;
            transform.position += (dir * speed);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !isGameover)
        {
            isGameover = true;
            playerRb.AddForce(Vector3.up * 8000, ForceMode.Impulse);
            playerRb.AddTorque(Vector3.right * 1000, ForceMode.Impulse);
            gameManager.Gameover();
        }
    }
}
