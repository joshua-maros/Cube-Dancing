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
    public Text multiText;


    //MULTIPLIER SUCCESS

    
    //@ START
    void Start()
    {
           //EXTERNAL SCRIPT CALLS
        currentTick = GameObject.Find("FeelTheSame").GetComponent<SongClock>().GetCurrentTick();


        scoreText.text = "0";
        multiText.text = "X" + "0";


        scoreText.text = currentScore.ToString();
        multiText.text = "X" + currentMultiplier.ToString();
        //currentTick = songClock.GetCurrentTick();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int i = 0;

   

        Debug.Log(currentTick);
        //cycle this through events as they occur
        //take current event compare to nearest with distance
        if (currentTick ==-1)
        {
            NoteHit();
        }
        else
            NoteMissed();
    }
    


    public void NoteHit()
    {

        //if (Mathf.Abs(withinRange()) < .5f)
       // {
            Debug.Log("HIT ON TIME");
            currentScore += point;
            scoreText.text = currentScore.ToString();

       // }
    }

    public void NoteMissed()
    {

       // if (Mathf.Abs(withinRange()) > .5f)
       // {
            Debug.Log("MISSED BEAT");
            scoreText.text = currentScore.ToString();
       // }
    }


    public float withinRange()
    {

        return currentTick - nearestTick;

    }
}
