using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    void Start() { }

    public void StepUp()
    {
        this.transform.Translate(Vector3.forward, Space.World);
        this.transform.Rotate(Vector3.right, 90.0f, Space.World);
    }

    public void StepDown()
    {
        this.transform.Translate(Vector3.back, Space.World);
        this.transform.Rotate(Vector3.left, 90.0f, Space.World);
    }

    public void StepLeft()
    {
        this.transform.Translate(Vector3.left, Space.World);
        this.transform.Rotate(Vector3.forward, 90.0f, Space.World);
    }

    public void StepRight()
    {
        this.transform.Translate(Vector3.right, Space.World);
        this.transform.Rotate(Vector3.back, 90.0f, Space.World);
    }

    public void Step(EventAction input) {
        if (input == EventAction.Up) {
            this.StepUp();
        } else if (input == EventAction.Down) {
            this.StepDown();
        } else if (input == EventAction.Left) {
            this.StepLeft();
        } else if (input == EventAction.Right) {
            this.StepRight();
        }
    }
}
