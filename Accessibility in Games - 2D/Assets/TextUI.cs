using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //to access Text
using TMPro; //Actually, because I'm using TextMeshPro instead of base "Text"
using UnityEngine.SceneManagement; //to reload Current Scene

public class TextUI : MonoBehaviour
{
    Player PlayerObject;
    public static int count, totalCount;
    public TextMeshProUGUI collectiblesText, timerText, playerActionsText;
    
    public float timeValue = 300; //5 minutes or 300 seconds

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        totalCount = GameObject.FindGameObjectsWithTag("ColourChanger").Length;
        PlayerObject = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        collectiblesText.text = $"Collectibles: {count}/{totalCount}";
        playerActionsText.text = $"Player Action: {PlayerObject.message}";
        //collectiblesText.SetText($"Collectibles: {count}/{totalCount}");

        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }
        if (timeValue <= 0)
        {
            //Reload current scene if timer reaches 0
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Game Over: You ran out of time! Reloading current scene.");
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); //divide by 60 seconds
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); //modulo 60

        timerText.text = $"Timer: {string.Format("{0:00}:{1:00}", minutes, seconds)}"; //time format in "minutes:seconds"
    }
}
