using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class RaceSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI resultTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;

    private float time = 0;
    private bool midPassed = false;
    public static bool isRacing = false;

    // Start is called before the first frame update
    void Start()
    {
        isRacing = false;
        time = 0;
        resultPanel.SetActive(false);
        StartCoroutine(CountDownCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (isRacing)
        {
            time += Time.deltaTime;
            timeText.SetText("Time: " + time.ToString("f1") + " s");
        }
    }

    public void RaceStart()
    {
        isRacing = true;
        midPassed = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator CountDownCoroutine()
    {
        countDownText.gameObject.SetActive(true);

        countDownText.text = "3";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "2";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "1";
        yield return new WaitForSeconds(1.0f);

        RaceStart();
        countDownText.text = "GO!";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "";
        countDownText.gameObject.SetActive(false);
    }

    private void RaceFinish()
    {
        isRacing = false;
        resultPanel.SetActive(true);
        resultTimeText.SetText("Time: " + time.ToString("f1") + " s");

        if (time < GameManager.bestTime)
        {
            bestTimeText.gameObject.SetActive(true);
            PlayerPrefs.SetFloat("BestTime", time);
            GameManager.bestTime = time;
        }
        else
        {
            bestTimeText.gameObject.SetActive(false);
        }
    }

    public void TriggerEnter(string colliderName)
    {
        if (isRacing && midPassed == true && colliderName == "GoalCollider")
        {
            Debug.Log("Goal");
            RaceFinish();
        }
        else if (colliderName == "MidCollider")
        {
            Debug.Log("midPassed");
            midPassed = true;
        }
    }
}
