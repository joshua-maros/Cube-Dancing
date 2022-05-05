using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;

    //players( protaganists) score at any given time;
    public int currentScore = 0;

    //Point types that can be gotten
    //private int point = 1;
    public int currentMultiplier = 0;
    public int[] multiplierThresholds = {0 ,5,10,50,100,250, 500, 1000, 1500, 10000,100000 };

    //current playhead in location/time
    private int currentLocation;

    //nearest event
    private float nearestTick;
    private float currentTick;

    //CANVAS
    public Text scoreText;
    float scorePulse = 0.0f;
    public Text multiText;

    //MULTIPLIER SUCCESS

    //COMBO
    bool comboNum = false;
    
    void Start() {
        instance = this;
        currentTick = SongClock.instance.GetCurrentTick();

        scoreText.text = "0";
        multiText.text = "X" + "0";
        if (currentMultiplier <= 1)
        {
            multiText.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreText.text = currentScore.ToString();
        scoreText.gameObject.transform.localScale = new Vector3(1, 1, 1) * (0.3f * this.scorePulse + 1.0f);
        this.scorePulse = Mathf.Max(this.scorePulse - Time.deltaTime * 8.0f, 0.0f);
        multiText.text = "X" + multiplierAmount().ToString();
    }

    int multiplierAmount() {
        return Mathf.FloorToInt(Mathf.Sqrt(currentMultiplier) / 1.5f) + 1;
    }

    public static int PerfectScore(int difficultyOutOfTen, int stepQuantity) {
        var multiplier = difficultyOutOfTen + 4.0f;
        var total = 0;
        for (int i = 0; i < stepQuantity; i++) {
            total += (int) (10 * multiplier * Mathf.FloorToInt(Mathf.Sqrt(i) / 1.5f + 1.0f));
        }
        return total;
    }

    public void NoteHit(float tick)
    {
        var time = Mathf.Abs(tick - SongClock.instance.GetCurrentTick());
        var multiplier = 1.0f;
        multiplier *= SongClock.instance.songChart.difficultyOutOfTen + 4.0f;
        multiplier *= multiplierAmount();
        if (time < 1.6f) {
            currentScore += (int) (10 * multiplier);
            scorePulse = 4.0f;
            comboNum = true;
        } else if (time < 3.0f) {
            currentScore += (int) (3 * multiplier);
            scorePulse = 1.0f;
            comboNum = true;
        } else {
            currentScore += (int) (1 * multiplier);
            scorePulse = 0.3f;
            comboNum = true;
        }
        comboScore();
    }

    public void NoteMissed()
    {
        comboNum = false;
        Effects.instance.Error();
        comboNumZed();
    }

    // chain combo
    public void comboScore()
    {
        if (comboNum)
        {
            multiText.enabled = true;
            currentMultiplier += 1;
        }
        else
        {
            comboNumZed();
        }
    }

    public void comboNumZed()
    {
        if (!comboNum)
        {
            currentMultiplier = 0;
            //currentMultiplier++;
        }
    }
}