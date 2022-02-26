using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private float timer = 0.0f;
    private float lastTick = 0.0f;
    private float tickInterval = 1.0f;
    private int sequence = 0;

    void Start() { }

    void Update() { 
        timer += Time.deltaTime;
        if (timer - lastTick > tickInterval) {
            lastTick = timer;
            sequence += 1;
            if (sequence % 2 == 1) {
                this.StepLeft();
            } else {
                this.StepDown();
            }
        }
    }

    public void StepUp() {
        this.transform.Translate(Vector3.forward, Space.World);
        this.transform.Rotate(Vector3.right, 90.0f, Space.World);
    }

    public void StepDown() {
        this.transform.Translate(Vector3.back, Space.World);
        this.transform.Rotate(Vector3.left, 90.0f, Space.World);
    }

    public void StepLeft() {
        this.transform.Translate(Vector3.left, Space.World);
        this.transform.Rotate(Vector3.forward, 90.0f, Space.World);
    }

    public void StepRight() {
        this.transform.Translate(Vector3.right, Space.World);
        this.transform.Rotate(Vector3.back, 90.0f, Space.World);
    }
}
