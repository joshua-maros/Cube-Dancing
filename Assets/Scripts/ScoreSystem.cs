using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreSystem : MonoBehaviour
{
 
    //players( protaganists) score at any given time;
    private int currentScore = 0;


    //Point types that can be gotten
    private int point = 1;
    private int currentMultiplier = 1;
    public int multiTracker;
    public int[] multiplierThresholds = { 500, 1000, 1500, 10000 };

    //current playhead in location/time
    private int currentLocation;

    //nearest event
    private float nearestTick;
    private float currentTick;
    
    private int currentEventTick;
  

    //CANVAS
    public Text scoreText;
    float scorePulse = 0.0f;
    public Text multiText;


    //MULTIPLIER SUCCESS


    
    //@ START
    void Start()
    {
        //EXTERNAL SCRIPT CALLS
        currentTick = SongClock.instance.GetCurrentTick();


        scoreText.text = "0";
        multiText.text = "X" + "0";


       
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        scoreText.text = currentScore.ToString();
        scoreText.gameObject.transform.localScale = new Vector3(1, 1, 1) * (0.3f * this.scorePulse + 1.0f);
        this.scorePulse = Mathf.Max(this.scorePulse - Time.deltaTime * 8.0f, 0.0f);
        multiText.text = "X" + currentMultiplier.ToString();
        Debug.Log(currentTick);

    }
    


    public void NoteHit()
    {
            scorePulse = 1.0f;
            Debug.Log("HIT ON TIME");
            currentScore += point;
            scoreText.text = currentScore.ToString();

    }

    public void NoteMissed()
    {
            Debug.Log("MISSED BEAT");
            scoreText.text = currentScore.ToString();
    }


  

    public void onInput(EventAction input)
    {

        var closestEvent = SongClock.instance.songChart.getter(SongClock.instance.GetCurrentTick());

        if (closestEvent.input == input)
        {
            NoteHit();
        }
        else {
            NoteMissed();
        }



    }
}
