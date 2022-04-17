using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public CubeController cc;

    void Start() { }

    void Update(){
        if(Input.GetButtonDown("P1Left")){
            cc.Step(EventAction.Left);
        }
        if(Input.GetButtonDown("P1Right")){
            cc.Step(EventAction.Right);
        }
        if(Input.GetButtonDown("P1Up")){
            cc.Step(EventAction.Up);
        }
        if(Input.GetButtonDown("P1Down")){
            cc.Step(EventAction.Down);
        }
        if(Input.GetButtonDown("P2Left")){
            cc.Step(EventAction.Left);
        }
        if(Input.GetButtonDown("P2Right")){
            cc.Step(EventAction.Right);
        }
        if(Input.GetButtonDown("P2Up")){
            cc.Step(EventAction.Up);
        }
        if(Input.GetButtonDown("P2Down")){
            cc.Step(EventAction.Down);
        }
    }
}