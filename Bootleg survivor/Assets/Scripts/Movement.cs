using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject camera;
    public float speed;
    private float xInput, yInput;

    private void FixedUpdate(){
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        camera.transform.Translate(xInput*speed, yInput*speed, 0);
    }
}
