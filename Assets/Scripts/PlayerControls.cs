using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public CubeController cc;
    public ScoreSystem scoreSystem;
    public Event nextEvent = new Event(-1, EventAction.Down, Player.A);
    public Event previousEvent = new Event(-2, EventAction.Down, Player.A);

    void Start() {
        cc = GetComponent<CubeController>();
        scoreSystem = ScoreSystem.instance;
        AdvanceNextEvent();
    }

    void AdvanceNextEvent() {
        previousEvent = nextEvent;
        var eventt = SongClock.instance.songChart.NextEventAfterTick(nextEvent.tick);
        Debug.Log(eventt.input);
        nextEvent = eventt;
    }

    void Update(){
        if (SongClock.instance.GetCurrentTick() < 0) return;
        if (nextEvent.tick == -1) return;
        if(Input.GetButtonDown("P1Left")){
            DoInput(EventAction.Left);
        }
        if(Input.GetButtonDown("P1Right")){
            DoInput(EventAction.Right);
        }
        if(Input.GetButtonDown("P1Up")){
            DoInput(EventAction.Up);
        }
        if(Input.GetButtonDown("P1Down")){
            DoInput(EventAction.Down);
        }
        if(Input.GetButtonDown("P2Left")){
            DoInput(EventAction.Left);
        }
        if(Input.GetButtonDown("P2Right")){
            DoInput(EventAction.Right);
        }
        if(Input.GetButtonDown("P2Up")){
            DoInput(EventAction.Up);
        }
        if(Input.GetButtonDown("P2Down")){
            DoInput(EventAction.Down);
        }
        var closestEvent = SongClock.instance.songChart.getter(SongClock.instance.GetCurrentTick());
        if (closestEvent.tick > nextEvent.tick || SongClock.instance.GetCurrentTick() - nextEvent.tick > SongClock.TICKS_PER_MEASURE / 8) {
            cc.Step(nextEvent.input);
            scoreSystem.NoteMissed();
            AdvanceNextEvent();
        }
    }

    void DoInput(EventAction action) {
        if (nextEvent.input == action) {
            if (SongClock.instance.GetCurrentTick() <= previousEvent.tick) {
                scoreSystem.NoteMissed();
            } else {
                scoreSystem.NoteHit(nextEvent.tick);
                cc.Step(nextEvent.input);
                AdvanceNextEvent();
            }
        } else {
            scoreSystem.NoteMissed();
        }
    }
}