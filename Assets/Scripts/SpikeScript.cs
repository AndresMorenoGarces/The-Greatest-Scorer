using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public GameObject ball;


    void OnCollisionEnter2D(Collision2D colission)
    {
        Destroy(ball);
    }

    void Start()
    {
    }

}
