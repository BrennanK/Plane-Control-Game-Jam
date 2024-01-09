using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance { get; private set; }
    // Start is called before the first frame update

    float gameTime;
    int score;

    [SerializeField]
    private TMP_Text timerText;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text expText;

    [SerializeField]
    private int xpForNextLevelup;

    private int currentXP;

    [SerializeField]
    private float xpMultiplierPerLevelUp;

    private void Awake()
    {
        if(instance!= null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        score = 0;
        scoreText.text = "SCORE: " + score;
        currentXP = 0;
        //expText.text = "XP: " + currentXP + "/" + xpForNextLevelup;
    }


    private void Update()
    {
        gameTime += Time.deltaTime;
        updateTimer();
    }

    private void updateTimer()
    {
        int seconds = ((int)gameTime % 60);
        int minutes = ((int)gameTime / 60);
        timerText.text=string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void updateScore()
    {
        score++;
        scoreText.text = "SCORE: " + score;
    }

    public void updateXP(int xpEarned)
    {
        currentXP += xpEarned;

        if(currentXP>=xpForNextLevelup)
        {
            xpForNextLevelup = (int)(xpMultiplierPerLevelUp * xpForNextLevelup);
        }

       // expText.text = "XP: " + currentXP + "/" + xpForNextLevelup;

        /*
         * For Beard Retro if I remeber correctly Unity Sliders only go from 0 to 1
         * You could use (float)(currentXp/xpForNextLevelUp) for Slider Value
         */
    }
}
