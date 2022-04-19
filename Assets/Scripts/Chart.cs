using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chart", menuName = "Chart", order = 1)]
[Serializable]


public class Chart : ScriptableObject
{

 

    [SerializeField]
    public Segment[] segments;
    public List<Event> events = new List<Event>();

    public Event getter(float tick)
    {
        Event closest = events[0];

       
        foreach(Event eventt in events )
        {
            if (eventt.player != Player.A) continue;

            var  temp = tick - closest.tick;
            temp = Mathf.Abs(temp);

            var temp2 = tick - eventt.tick;
            temp2 = Mathf.Abs(temp2);

            if (temp > temp2)
            {
                closest = eventt;
            }

        }

        return closest;

    }

    public Event NextEventAfterTick(float tick)
    {
        Event closest = events[0];
       
        foreach(Event eventt in events )
        {
            if (eventt.player != Player.A) continue;

            var previousDelta = closest.tick - tick;
            var currentDelta = eventt.tick - tick;

            if (
                (currentDelta < previousDelta || previousDelta <= 0)
                && currentDelta > 0.1f
            ) {
                closest = eventt;
            }
        }

        return closest;
    }

    public List<Event> GetEventsInRange(float startTick, float endTick) {
        List<Event> result = new List<Event>();
        foreach (Event eventt in events) {
            if (eventt.tick >= startTick && eventt.tick <= endTick) {
                result.Add(eventt);
            }
        }
        return result;
    }

    public void AnnotatePositions() {
        events.Sort((x, y) => x.tick.CompareTo(y.tick));
        GridCoordinate p1 = new GridCoordinate(0, 0);
        GridCoordinate p2 = new GridCoordinate(0, -2);
        for(int i = 0; i < events.Count; i++) {
            Event eventt = events[i];
            if (eventt.player == Player.A) {
                p1.Offset(eventt.input);
                eventt.position = p1.Clone();
            } else {
                p2.Offset(eventt.input);
                eventt.position = p2.Clone();
            }
            events[i] = eventt;
        }
    }
}

[Serializable]
public struct Segment
{
    [SerializeField]
    public float startTime, endTime;
    [SerializeField]
    public int numMeasures;
}

[Serializable]
public struct Event
{
    [SerializeField]
    // Which tick this event falls on. A measure has 96 ticks.
    public int tick;

    [SerializeField]
    public EventAction input;

    [SerializeField]
    public Player player;

    public GridCoordinate position;

    public Event(int tick, EventAction input, Player player)
    {
        this.tick = tick;
        this.input = input;
        this.player = player;
        this.position = new GridCoordinate(0, 0);
    }
}

[Serializable]
public enum EventAction
{
       Up,
    Down,
    Left,
    Right,
    Nothing
}

public static class InputExtensions
{
    public static EventAction Reverse(this EventAction input)
    {
        switch (input)
        {
            case EventAction.Up:
                
                return EventAction.Down;
            case EventAction.Down:
                
                return EventAction.Up;

            case EventAction.Left:
                return EventAction.Right;
                
            case EventAction.Right:
                return EventAction.Left;

            default: // Nothing
                return EventAction.Nothing;
        }
    }
}

[Serializable]
public enum Player
{
    A,
    B,
}

