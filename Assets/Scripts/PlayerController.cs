using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speed = 30.0f;
    private float rangeX = 2.0f;
    private float rangeZ = 4.3f;
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
        Vector3 val = Input.acceleration;
        Vector3 dir = Vector3.zero;

        dir.x = val.x;
        dir.z = val.y;

        if ((transform.position.x > rangeX && dir.x > 0) || (transform.position.x < -rangeX && dir.x < 0))
        {
            dir.x = 0;
        }

        if ((transform.position.z > rangeZ && dir.z > 0) || (transform.position.z < -rangeZ && dir.z < 0))
        {
            dir.z = 0;
        }

        if (gameManager.isGameActive)
        {
            dir *= Time.deltaTime;
            transform.position += (dir * speed);
        }
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            isGameover = true;
            playerRb.AddForce(Vector3.up * 8, ForceMode.Impulse);
            playerRb.AddTorque(Vector3.right * speed, ForceMode.Impulse);
            gameManager.Gameover();
        }
    }
}
