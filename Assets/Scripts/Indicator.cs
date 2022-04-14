using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private GridCoordinate coord;
    public GameObject inside;
    const float LOOKAHEAD = SongClock.TICKS_PER_MEASURE / 4;

    // Start is called before the first frame update
    void Start()
    {
        coord = new GridCoordinate(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        var start = SongClock.instance.GetCurrentTick();
        var end = start + LOOKAHEAD;
        var futureEvents = SongClock.instance.songChart.GetEventsInRange(start, end);
        var scale = 0.0f;
        foreach (var eventt in futureEvents) {
            if (eventt.position.Equals(coord)) {
                var timeUntilHappening = (eventt.tick - start) / LOOKAHEAD;
                scale = Mathf.Pow(1.0f - timeUntilHappening, 3.0f);
                break;
            }
        }
        inside.transform.localScale = new Vector3(1, 1, 1) * scale;
        float height = 0.0f;
        foreach (var ripple in Ripples.instance.ripples) {
            float factor = 0.5f * (30.0f * ripple.progress - (ripple.center - transform.position).magnitude);
            if (factor >= -1.0f && factor <= 1.0f) {
                height += 0.1f * (Mathf.Cos(Mathf.PI * factor) + 1.0f);
            }
        }
        Vector3 p = transform.localPosition;
        p.y = height;
        transform.localPosition = p;
    }
}
