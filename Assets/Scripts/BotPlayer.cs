using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeController))]
public class BotPlayer : MonoBehaviour
{
    private CubeController cc;
    private float lastTick = -99999.0f;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CubeController>();
    }

    // Update is called once per frame
    void Update()
    {
        var tickNow = SongClock.instance.GetCurrentTick();
        bool wentBackwards = tickNow < this.lastTick;
        foreach (var e in SongClock.instance.songChart.events)
        {
            if (wentBackwards)
            {
                if (e.tick > tickNow && e.tick <= this.lastTick)
                {
                    if (e.player == player)
                        cc.Step(e.input.Reverse());
                }
            }
            else
            {
                if (e.tick > this.lastTick && e.tick <= tickNow)
                {
                    if (e.player == player)
                        cc.Step(e.input);
                }
            }
        }
        this.lastTick = tickNow;
    }
}
