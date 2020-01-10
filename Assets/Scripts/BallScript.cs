using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody2D ballRigidBody;
    public int dir = 1;
    

    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ballRigidBody.AddForce(transform.up * 200);
            ballRigidBody.velocity = new Vector2(0, 0);
            GameManager.instance.BallSound();

        }
        ballRigidBody.velocity = new Vector2(1.5f * dir, ballRigidBody.velocity.y);
        
    }

    void OnCollisionEnter2D(Collision2D colission)
    {
        if (colission.transform.tag == "Left Wall")
        {
            dir *= -1;
            GameManager.instance.UpdateScore();
            GameManager.instance.ChangeSpikePosition(true);
            GameManager.instance.FunctionsActivator();
            GameManager.instance.WallHitSound();
        }
        else if (colission.transform.tag == "Right Wall")
        {
            dir *= -1;
            GameManager.instance.UpdateScore();
            GameManager.instance.ChangeSpikePosition(false);
            GameManager.instance.FunctionsActivator();
            GameManager.instance.WallHitSound();
        }
    }
}
