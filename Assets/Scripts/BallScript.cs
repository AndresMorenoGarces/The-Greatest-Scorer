using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody2D rB;
    public int dir = 1;
    

    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rB.AddForce(transform.up * 200);
            rB.velocity = new Vector2(0, 0);

        }
        rB.velocity = new Vector2(1 * dir, rB.velocity.y);
        
    }

    void OnCollisionEnter2D(Collision2D colission)
    {
        if (colission.transform.tag == "Left Wall")
        {
            dir *= -1;
            GameManager.instance.UpdateScore();
            GameManager.instance.ChangeSpikePosition(true);

        }
        else if (colission.transform.tag == "Right Wall")
        {
            dir *= -1;
            GameManager.instance.UpdateScore();
            GameManager.instance.ChangeSpikePosition(false);

        }
    }
}
