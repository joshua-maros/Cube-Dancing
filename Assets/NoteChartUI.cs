using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteChartUI : MonoBehaviour
{
    public GameObject iconPrefab;
    public GameObject tickPrefab;
    List<GameObject> icons = new List<GameObject>();
    List<GameObject> ticks = new List<GameObject>();
    public Sprite up, down, left, right;
    int division = 4;
    public const int NUM_TICKS = 16;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var timeNow = SongClock.instance.GetCurrentTick();
        var timePerTick = (96.0f / division);
        var firstTickTime = Mathf.Ceil(timeNow / timePerTick) * timePerTick;
        var lastTickTime = firstTickTime + NUM_TICKS * timePerTick;
        ClearTicksAndIcons();
        for (int i = 0; i < NUM_TICKS; i++)
        {
            var time = firstTickTime + i * timePerTick;
            MakeTick(time);
        }
        foreach (var e in SongClock.instance.songChart.events) {
            if (e.tick >= timeNow && e.tick <= lastTickTime) {
                MakeEventIcon(e);
            }
        }
    }

    void ClearTicksAndIcons()
    {
        foreach (var icon in icons)
        {
            Destroy(icon.gameObject);
        }
        icons.Clear();
        foreach (var tick in ticks)
        {
            Destroy(tick.gameObject);
        }
        ticks.Clear();
    }

    void MakeTick(float time)
    {
        GameObject tick = Instantiate(tickPrefab.gameObject, this.transform);
        MoveIcon(tick, time);
        var timeInMeasure = (time / 96.0f) % 1.0f;
        var s = tick.transform.localScale;
        if (timeInMeasure < 0.001f || timeInMeasure > 0.999f)
        {
            s.y = 10.0f;
        }
        else
        {
            s.y = 2.0f;
        }
        tick.transform.localScale = s;
        ticks.Add(tick);
    }

    void MakeEventIcon(Event e)
    {
        GameObject icon = Instantiate(iconPrefab.gameObject, this.transform);
        MoveIcon(icon, e.tick);
        Image image = icon.GetComponent<Image>();
        switch (e.input)
        {
            case Input.Up:
                image.sprite = up;
                break;
            case Input.Down:
                image.sprite = down;
                break;
            case Input.Left:
                image.sprite = left;
                break;
            case Input.Right:
                image.sprite = right;
                break;
        }
        icons.Add(icon);
    }

    public void MoveIcon(GameObject icon, float time) {
        RectTransform t = icon.GetComponent<RectTransform>();
        var p = t.anchoredPosition;
        p.y = PosForTick(time);
        t.anchoredPosition = p;
    }

    public float PosForTick(float tick)
    {
        float timeNow = SongClock.instance.GetCurrentTick();
        return (tick - timeNow) * 100.0f * this.division / 96.0f;
    }
}
