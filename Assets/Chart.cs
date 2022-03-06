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

    public Event(int tick, EventAction input, Player player)
    {
        this.tick = tick;
        this.input = input;
        this.player = player;
    }
}

[Serializable]
public enum EventAction
{
    Up,
    Down,
    Left,
    Right,
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
            default: // Right
                return EventAction.Left;
        }
    }
}

[Serializable]
public enum Player
{
    A,
    B,
}
