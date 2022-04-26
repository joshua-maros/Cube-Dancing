using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class SongClock : MonoBehaviour
{
    // Divisible by 1, 2, 4, 8, 16, 32, 3, 6, 12, 24, 48, and 96.
    public const int TICKS_PER_MEASURE = 96;
    // How much the video lags behind the audio.
    public const float LATENCY = 0.05f;
    public AudioSource src;
    public Chart songChart;
    public TextMeshProUGUI countdownText;
    public static SongClock instance;
    private float ticksUntilSongStart = TICKS_PER_MEASURE * 1.5f;
    private bool songStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.clip = songChart.song;
        instance = this;
        songChart.AnnotatePositions();
    }

    public void PlaySong() {
        src.time = songChart.segments[0].startTime - ticksUntilSongStart / songChart.TicksPerSecond();
        src.Play();
        songStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        ticksUntilSongStart -= Time.deltaTime * songChart.TicksPerSecond();
        if (!songStarted) {
            if (ticksUntilSongStart < songChart.segments[0].startTime * songChart.TicksPerSecond()) {
                PlaySong();
            }
        }
        if (ticksUntilSongStart > 0.0f && ticksUntilSongStart <= TICKS_PER_MEASURE) {
            float countoff = 4.0f - (ticksUntilSongStart / (TICKS_PER_MEASURE / 4));
            countdownText.text = Mathf.CeilToInt(countoff).ToString();
            var scale = 1.0f - countoff % 1.0f;
            countdownText.rectTransform.localScale = new Vector3(scale, scale, scale);
        } else {
            countdownText.rectTransform.localScale = Vector3.zero;
        }
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
        if (songStarted) {
            return (songTime - songChart.segments[0].startTime) * songChart.TicksPerSecond();
        } else {
            return -ticksUntilSongStart;
        }
    }
}
