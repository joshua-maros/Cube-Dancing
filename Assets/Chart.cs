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
    public Event[] events;
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
    public Input input;

    [SerializeField]
    public Player player;
}

[Serializable]
public enum Input
{
    Up,
    Down,
    Left,
    Right,
}

[Serializable]
public enum Player
{
    A,
    B,
}
