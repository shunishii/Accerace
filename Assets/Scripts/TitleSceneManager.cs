using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTimeText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            GameManager.bestTime = PlayerPrefs.GetFloat("BestTime");
            bestTimeText.gameObject.SetActive(true);
            bestTimeText.text = "Best Time: " + GameManager.bestTime.ToString("f1");
        }
        else
        {
            GameManager.bestTime = float.PositiveInfinity;
            bestTimeText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
