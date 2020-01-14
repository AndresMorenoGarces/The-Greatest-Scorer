
using UnityEngine;

public class SelectUnlockBalls : MonoBehaviour
{
    public int currentNumBall;

    public void ChangingCurrentBall()
    {
        GameManager.instance.ballNumber = this.currentNumBall;
    }
}
