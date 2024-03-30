using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject PlayerCamera;
    private float axisRatio = Mathf.Sqrt(1f/2f);

    private void FixedUpdate(){
        float xMultiplier = Input.GetAxis("Horizontal");
        float yMultiplier = Input.GetAxis("Vertical");

        if(xMultiplier!=0 && yMultiplier!=0){//Normalize values if going diagonally
            xMultiplier *= axisRatio;
            yMultiplier *= axisRatio;
        }

        PlayerCamera.transform.Translate(xMultiplier*State.instance.player.speed, yMultiplier*State.instance.player.speed, 0);
    }
}
