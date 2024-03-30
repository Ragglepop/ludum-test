using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject camera;
    private float axisRatio;

    private void Start(){
        axisRatio = Mathf.Sqrt(1f/2f);
    }

    private void FixedUpdate(){
        float xMultiplier = Input.GetAxis("Horizontal");
        float yMultiplier = Input.GetAxis("Vertical");

        if(xMultiplier!=0 && yMultiplier!=0){//Normalize values if going diagonally
            xMultiplier *= axisRatio;
            yMultiplier *= axisRatio;
        }

        camera.transform.Translate(xMultiplier*State._.player.speed, yMultiplier*State._.player.speed, 0);
    }
}
