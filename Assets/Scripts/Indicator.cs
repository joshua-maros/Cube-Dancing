using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private GridCoordinate coord;
    public GameObject inside;
    const float LOOKAHEAD = 36.0f;

    // Start is called before the first frame update
    void Start()
    {
        coord = new GridCoordinate(
            ((int) (transform.position.x - 0.5f)),
            ((int) (transform.position.z - 0.5f))
        );
    }

    // Update is called once per frame
    void Update()
    {
        var start = SongClock.instance.GetCurrentTick();
        var end = start + LOOKAHEAD;
        var futureEvents = SongClock.instance.songChart.GetEventsInRange(start, end);
        var pos = CubeController.instance.coord.Clone();
        foreach (var eventt in futureEvents) {
            Debug.Log(pos.GetX());
            pos.Offset(eventt.input);
            if (pos.Equals(coord)) {
                var timeUntilHappening = (eventt.tick - start) / LOOKAHEAD;
                inside.transform.localScale = new Vector3(1, 1, 1) * (1.0f - timeUntilHappening);
                break;
            } else {
                inside.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }
}
