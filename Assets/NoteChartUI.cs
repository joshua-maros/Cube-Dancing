using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteChartUI : MonoBehaviour
{
    public GameObject iconPrefab;
    public GameObject linePrefab;
    List<GameObject> icons = new List<GameObject>();
    List<GameObject> lines = new List<GameObject>();
    public Sprite up, down, left, right;
    int division = 4;
    public const int NUM_LINES = 16;
    float lastTick = -1.0f;
    public CubeController p1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MakeLinesAndIcons();
        MovePlayer();
        bool makePressed = false;
        EventAction pressedAction = EventAction.Up;
        bool clearPressed = false;
        if (Input.GetButtonDown("P1Left"))
        {
            makePressed = true;
            pressedAction = EventAction.Left;
        }
        else if (Input.GetButtonDown("P1Right"))
        {
            makePressed = true;
            pressedAction = EventAction.Right;
        }
        else if (Input.GetButtonDown("P1Up"))
        {
            makePressed = true;
            pressedAction = EventAction.Up;
        }
        else if (Input.GetButtonDown("P1Down"))
        {
            makePressed = true;
            pressedAction = EventAction.Down;
        }
        else if (Input.GetButtonDown("EditorClear"))
        {
            clearPressed = true;
        }

        float tick = Input.mousePosition.y - GetComponent<RectTransform>().offsetMin.y;
        // Each line is separated by 100 pixels.
        tick /= 100.0f;
        tick *= TicksPerLine();
        if (tick <= 0.0f) return;
        tick += SongClock.instance.GetCurrentTick();

        var events = SongClock.instance.songChart.events;
        int closest = -1;
        float closestDistance = 100.0f;
        for (int i = events.Count; i > 0; i--)
        {
            var e = events[i - 1];
            var distance = Mathf.Abs(e.tick - tick);
            if (distance <= 48.0f / division && distance < closestDistance)
            {
                closest = i - 1;
                closestDistance = distance;
            }
        }

        if (makePressed)
        {
            if (closest == -1)
            {
                tick = Mathf.Round(tick / TicksPerLine()) * TicksPerLine();
                events.Add(new Event(Mathf.RoundToInt(tick), pressedAction, Player.A));
            }
            else
            {
                var e = events[closest];
                e.input = pressedAction;
                events[closest] = e;
            }
        }
        else if (clearPressed && closest != -1)
        {
            events.RemoveAt(closest);
        }
    }

    float TicksPerLine()
    {
        return (96.0f / division);
    }

    void MakeLinesAndIcons()
    {
        var tickNow = SongClock.instance.GetCurrentTick();
        var firstLineTime = Mathf.Ceil(tickNow / TicksPerLine()) * TicksPerLine();
        var lastLineTime = firstLineTime + NUM_LINES * TicksPerLine();
        ClearLinesAndIcons();
        for (int i = 0; i < NUM_LINES; i++)
        {
            var time = firstLineTime + i * TicksPerLine();
            MakeLine(time);
        }
        foreach (var e in SongClock.instance.songChart.events)
        {
            if (e.tick >= tickNow && e.tick <= lastLineTime)
            {
                MakeEventIcon(e);
            }
        }
    }

    void MovePlayer()
    {
        var tickNow = SongClock.instance.GetCurrentTick();
        bool wentBackwards = tickNow < this.lastTick;
        foreach (var e in SongClock.instance.songChart.events)
        {
            if (wentBackwards)
            {
                if (e.tick > tickNow && e.tick <= this.lastTick)
                {
                    p1.Step(e.input.Reverse());
                }
            }
            else
            {
                if (e.tick > this.lastTick && e.tick <= tickNow)
                {
                    p1.Step(e.input);
                }
            }
        }
        this.lastTick = tickNow;
    }

    void ClearLinesAndIcons()
    {
        foreach (var icon in icons)
        {
            Destroy(icon.gameObject);
        }
        icons.Clear();
        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }
        lines.Clear();
    }

    void MakeLine(float tick)
    {
        GameObject line = Instantiate(linePrefab.gameObject, this.transform);
        MoveIcon(line, tick);
        var timeInMeasure = (tick / 96.0f) % 1.0f;
        var s = line.transform.localScale;
        if (timeInMeasure < 0.001f || timeInMeasure > 0.999f)
        {
            s.y = 10.0f;
        }
        else
        {
            s.y = 2.0f;
        }
        line.transform.localScale = s;
        lines.Add(line);
    }

    void MakeEventIcon(Event e)
    {
        GameObject icon = Instantiate(iconPrefab.gameObject, this.transform);
        MoveIcon(icon, e.tick);
        Image image = icon.GetComponent<Image>();
        switch (e.input)
        {
            case EventAction.Up:
                image.sprite = up;
                break;
            case EventAction.Down:
                image.sprite = down;
                break;
            case EventAction.Left:
                image.sprite = left;
                break;
            case EventAction.Right:
                image.sprite = right;
                break;
        }
        icons.Add(icon);
    }

    public void MoveIcon(GameObject icon, float tick)
    {
        RectTransform t = icon.GetComponent<RectTransform>();
        var p = t.anchoredPosition;
        p.y = PosForTick(tick);
        t.anchoredPosition = p;
    }

    public float PosForTick(float tick)
    {
        float tickNow = SongClock.instance.GetCurrentTick();
        return (tick - tickNow) * 100.0f * this.division / 96.0f;
    }
}
