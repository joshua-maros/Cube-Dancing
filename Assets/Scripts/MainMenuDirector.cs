using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuDirector : MonoBehaviour
{
    public static Chart theChart;
    public static GameObject theSkin;
    public static bool theAutoplay = false;

    public Renderer cover;
    public List<Chart> charts;
    public List<Renderer> covers;
    public int selected = 0;
    public Camera mainCamera;
    public GameObject title;
    int phase = 0;
    public GameObject settingsUi;
    public GameObject currentSkin;
    public List<GameObject> skins;
    int selectedSkin = 0;
    bool autoplay = false;
    public TextMeshPro autoplaySelection;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < charts.Count; i++) {
            LoadHighScore(i);
            InstantiateCover(i);
            InstantiateTitle(i);
        }
        theChart = charts[0];
        theSkin = skins[0];
        theAutoplay = false;
    }

    void InstantiateCover(int index) {
        GameObject obj = Instantiate(cover.gameObject, new Vector3(0, index / 100.0f, index * 2), Quaternion.identity);
        var rend = obj.GetComponent<Renderer>();
        rend.material.SetTexture("_EmissionMap", charts[index].cover);
        rend.material.color = new Color(0, 0, 0, 0);
        covers.Add(rend);
    }

    void InstantiateTitle(int index) {
        var chart = charts[index];
        GameObject obj = Instantiate(title, this.gameObject.transform);
        obj.transform.localPosition = new Vector3(0.0f, 2, index * 8.0f);
        TextMeshPro songName = obj.transform.Find("Song").GetComponent<TextMeshPro>();
        songName.text = chart.title;
        TextMeshPro bandName = obj.transform.Find("Band").GetComponent<TextMeshPro>();
        bandName.text= "By " + chart.band;
        TextMeshPro highScore = obj.transform.Find("Score").GetComponent<TextMeshPro>();
        highScore.text= "High Score: " + chart.highScore;
        TextMeshPro perfectScore = obj.transform.Find("PerfectScore").GetComponent<TextMeshPro>();
        perfectScore.text= "Perfect Score: " + ScoreSystem.PerfectScore(chart.difficultyOutOfTen, chart.events.Count);
        TextMeshPro difficulty = obj.transform.Find("Difficulty").GetComponent<TextMeshPro>();
        difficulty.text = new string('*', chart.difficultyOutOfTen);
    }

    void LoadHighScore(int index) {
        charts[index].highScore = PlayerPrefs.GetInt("hs." + charts[index].title);
    }

    void SetSkin(GameObject newSkin) {
        var newSkinInstance = Instantiate(newSkin, currentSkin.transform.parent);
        Destroy(currentSkin);
        currentSkin = newSkinInstance;
    }

    // Update is called once per frame
    void Update()
    {
        var left = Input.GetButtonDown("P1Left") || Input.GetButtonDown("P2Left");
        var right = Input.GetButtonDown("P1Right") || Input.GetButtonDown("P2Right");
        var up = Input.GetButtonDown("P1Up") || Input.GetButtonDown("P2Up");
        var down = Input.GetButtonDown("P1Down") || Input.GetButtonDown("P2Down");
        if (right && phase == 0) {
            phase = 1;
        } else if (left && phase == 1) {
            phase = 0;
        } else if (right && phase == 1) {
            phase = 2;
        }
        if (phase == 0) {
            if (down && selected < covers.Count - 1) {
                selected += 1;
            } else if (up && selected > 0) {
                selected -= 1;
            } 
        } else if (phase == 1) {
            if (up) {
                selectedSkin = (selectedSkin + 1) % skins.Count;
                SetSkin(skins[selectedSkin]);
            } else if (down) {
                autoplay = !autoplay;
                autoplaySelection.text = autoplay ? "Yes" : "No";
            } 
        }

        for (int i = 0; i < charts.Count; i++) {
            var alpha = covers[i].material.color.a;
            if (phase == 2) {
                alpha = Mathf.Max(alpha - Time.deltaTime, 0.0f);
            } else if (i == selected) {
                alpha = Mathf.Min(alpha + Time.deltaTime, 0.5f);
            } else {
                alpha = Mathf.Max(alpha - Time.deltaTime, 0.0f);
            }
            if (i == selected && alpha == 0.0f && phase == 2) {
                SceneManager.LoadScene("Player");
            }
            covers[i].material.color = new Color(0, 0, 0, alpha);
        }

        var targetX = 2.0f - 4.0f * phase;
        var targetZ = selected * 2.0f;
        var current = mainCamera.gameObject.transform.localPosition;
        current.z = Mathf.Lerp(current.z, targetZ, Mathf.Min(Time.deltaTime, 0.1f) * 5.0f);
        current.x = Mathf.Lerp(current.x, targetX, Mathf.Min(Time.deltaTime, 0.1f) * 5.0f);
        this.transform.localPosition = new Vector3(-3.0f * (current.x - 2.0f), 0, -3.0f * current.z);
        settingsUi.transform.localPosition = new Vector3(-3.0f * (current.x + 2.0f), 0, current.z);
        mainCamera.gameObject.transform.localPosition = current;

        theChart = charts[selected];
        theSkin = skins[selectedSkin];
        theAutoplay = autoplay;
    }
}
