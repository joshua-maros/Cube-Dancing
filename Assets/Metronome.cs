using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    // Default to flashing on quarter notes
    public int division = 4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float period = SongClock.TICKS_PER_MEASURE / this.division;
        float phase = SongClock.instance.GetCurrentTick() % period / period;
        phase = phase < 0.5f ? 0.0f : 1.0f;
        float scale = Mathf.Lerp(1.0f, 0.2f, phase);
        this.transform.localScale = scale * new Vector3(1.0f, 1.0f, 1.0f);
    }
}
