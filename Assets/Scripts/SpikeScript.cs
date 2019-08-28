using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public GameObject ball;
   
    void OnCollisionEnter2D(Collision2D colission)
    {
        Destroy(ball);
        GameManager.instance.SaveTemporalScore();
        GameManager.instance.SaveBestScore();
    }
}
