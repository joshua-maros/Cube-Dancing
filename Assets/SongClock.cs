using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SongClock : MonoBehaviour
{
    // Divisible by 1, 2, 4, 8, 16, 32, 3, 6, 12, 24, 48, and 96.
    public const int TICKS_PER_MEASURE = 96;
    // How much the video lags behind the audio.
    public const float LATENCY = 0.00f;
    public AudioSource src;
    public Chart songChart;
    public static SongClock instance;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float GetCurrentTick()
    {
        float songTime = src.time + LATENCY;
        float currentTick = 0.0f;
        foreach (Segment seg in songChart.segments)
        {
            if (songTime >= seg.startTime && songTime <= seg.endTime)
            {
                float offset = (songTime - seg.startTime)
                    / (seg.endTime - seg.startTime)
                    * seg.numMeasures
                    * TICKS_PER_MEASURE;
                return currentTick + offset;
            }
            else
            {
                currentTick += seg.numMeasures * TICKS_PER_MEASURE;
            }
        }
        return -1.0f;
    }
}
