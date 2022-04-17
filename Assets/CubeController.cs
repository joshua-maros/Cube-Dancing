using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    void Start() { }
//commit was implementing the: void Update(){}
    void Update(){
        if(Input.GetButtonDown("P1Left")){
            Step(EventAction.Left);
        }
        if(Input.GetButtonDown("P1Right")){
           Step(EventAction.Right);
        }
        if(Input.GetButtonDown("P1Up")){
           Step(EventAction.Up);
        }
        if(Input.GetButtonDown("P1Down")){
           Step(EventAction.Down);
        }
        if(Input.GetButtonDown("P2Left")){
            Step(EventAction.Left);
        }
        if(Input.GetButtonDown("P2Right")){
           Step(EventAction.Right);
        }
        if(Input.GetButtonDown("P2Up")){
            Step(EventAction.Up);
        }
        if(Input.GetButtonDown("P2Down")){
            Step(EventAction.Down);
        }


    }
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
