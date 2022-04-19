
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Effects : MonoBehaviour
{
    public static Effects instance;
    public List<Ripple> ripples = new List<Ripple>();
    public int lastRippleTriggered = -1;
    public PostProcessVolume postProcessing;
    const float FLASH_DURATION = 0.25f;
    float errorFlash = 0.0f;

    void Start() {
        instance = this;
    }

    public void Error() {
        errorFlash = FLASH_DURATION;
    }

    void Update() {
        foreach (Ripple ripple in ripples) {
            ripple.progress += Time.deltaTime;
        }
        ripples = ripples.Where(x => x.progress < 20.0f).ToList();

        var ft = SongClock.instance.GetCurrentTick();
        var cycle = ft % (SongClock.TICKS_PER_MEASURE / 2) / (SongClock.TICKS_PER_MEASURE / 2);
        var placeInCycle = cycle >= 0.5f;
        cycle = cycle % 0.5f / 0.5f;
        var settings = postProcessing.profile.GetSetting<ColorGrading>();
        if (cycle < FLASH_DURATION) {
            cycle = cycle / FLASH_DURATION;
            if (placeInCycle) {
                settings.postExposure.value = 1.0f - cycle;
            } else {
                settings.postExposure.value = 2.0f * (cycle - 1.0f);
            }
        } else {
            settings.postExposure.value = 0.0f;
        }
        var vignette = postProcessing.profile.GetSetting<Vignette>();
        vignette.intensity.value = errorFlash / FLASH_DURATION;
        errorFlash -= Time.deltaTime;
        if (errorFlash < 0.0f) errorFlash = 0.0f;

        var t = (int) ft;
        if (t % (SongClock.TICKS_PER_MEASURE / 2) == SongClock.TICKS_PER_MEASURE / 4) {
            if (lastRippleTriggered != t) {
                ripples.Add(new Ripple(CubeController.instance.gameObject.transform.position));
                lastRippleTriggered = t;
            }
        }
    }
}

public class Ripple 
{
    public Vector3 center = Vector3.zero;
    public float progress = 0.0f;

    public Ripple(Vector3 center) {
        this.center = center;
    }
}
