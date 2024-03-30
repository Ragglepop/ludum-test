using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject camera;
    public float speed;
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

        camera.transform.Translate(xMultiplier*speed, yMultiplier*speed, 0);
    }
}
