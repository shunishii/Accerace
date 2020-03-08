using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI timeText;
    public Button startButton;
    public Button goTitleButton;
    public bool isGameActive;
    private float xRange = 2.0f;
    private float spawnRate = 1.0f;
    private float countTime;
    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            countTime += Time.deltaTime;
            timeText.SetText("Time: " + (Mathf.Round(countTime * 10) / 10).ToString("F1"));
        }
    }

    public void GameStart()
    {
        isGameActive = true;
        startButton.gameObject.SetActive(false);
        countTime = 0.0f;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        StartCoroutine(SpawnBall());
    }

    IEnumerator SpawnBall()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            if (spawnRate > 0.3f)
            {
                spawnRate -= 0.01f;
            }
            float xPos = Random.Range(-xRange, xRange);
            Instantiate(ballPrefab, new Vector3(xPos, -10, 7), transform.rotation);
        }
    }

    public void Gameover()
    {
        gameoverText.gameObject.SetActive(true);
        goTitleButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
