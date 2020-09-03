using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject obstacle;
    public TextMeshProUGUI finishText;
    public TextMeshProUGUI scoreText;
    public Button startButton;
    public Button goTitleButton;
    public bool isGameActive;
    private float xRange = 2.0f;
    private float spawnRate = 1.0f;
    private float countTime;
    private int maxScore;
    private int currentScore;
    private float flashTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        maxScore = PlayerPrefs.GetInt("SCORE", 0);
        scoreText.SetText("Max Score: " + maxScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            countTime += Time.deltaTime;
            currentScore = ((int)(countTime * 10));
            scoreText.SetText("Score: " + currentScore);
        }
        if (finishText.IsActive())
        {
            finishText.color = GetAlphaColor(finishText.color);
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
            Instantiate(obstacle, new Vector3(xPos, 0.25f, 7), transform.rotation);
        }
    }

    public void Gameover()
    {
        goTitleButton.gameObject.SetActive(true);
        isGameActive = false;
        if (maxScore < currentScore)
        {
            maxScore = currentScore;
            PlayerPrefs.SetInt("SCORE", maxScore);
            PlayerPrefs.Save();
            finishText.SetText(currentScore + "\nUpdated max score!");
        }
        else
        {
            finishText.SetText(currentScore + "\nGameover!");
        }
        finishText.gameObject.SetActive(true);
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    Color GetAlphaColor(Color color)
    {
        flashTime += Time.deltaTime * 5.0f;
        color.a = Mathf.Sin(flashTime) * 0.5f + 0.5f;

        return color;
    }
}
