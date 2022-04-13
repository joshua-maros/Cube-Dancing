using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CubeController : MonoBehaviour
{

    public ScoreSystem scoreSystem;
    public static CubeController instance;
    public GridCoordinate coord = new GridCoordinate(0, 0);
 



    void Awake()
    {
       
        

    }


    void Start() {
      instance = this;
    }

    public void StepUp()
    {
        this.transform.Translate(Vector3.forward, Space.World);
        this.transform.Rotate(Vector3.right, 90.0f, Space.World);
        scoreSystem.onInput(EventAction.Up);
     }

    public void StepDown()
    {
        this.transform.Translate(Vector3.back, Space.World);
        this.transform.Rotate(Vector3.left, 90.0f, Space.World);
        scoreSystem.onInput(EventAction.Down);
    }

    public void StepLeft()
    {
        this.transform.Translate(Vector3.left, Space.World);
        this.transform.Rotate(Vector3.forward, 90.0f, Space.World);
        scoreSystem.onInput(EventAction.Left);
    }

    public void StepRight()
    {
        this.transform.Translate(Vector3.right, Space.World);
        this.transform.Rotate(Vector3.back, 90.0f, Space.World);
        scoreSystem.onInput(EventAction.Right);
    }

    public void Step(EventAction input) {
        coord.Offset(input);
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
