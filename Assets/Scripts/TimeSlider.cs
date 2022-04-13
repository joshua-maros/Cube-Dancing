using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TimeSlider : MonoBehaviour
{
    Slider slider;
    bool updatePlayback = true;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    void Update()
    {
        updatePlayback = false;
        slider.value = SongClock.instance.src.time / SongClock.instance.src.clip.length;
        updatePlayback = true;
    }

    public void OnValueChanged()
    {
        if (!updatePlayback) return;
        SongClock.instance.src.time = slider.value * SongClock.instance.src.clip.length;
    }
}