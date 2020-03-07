using UnityEngine;

public class SelectUnlockBalls : MonoBehaviour
{
    public GameObject[] lockAdornsBallon = new GameObject[10];
    private GameObject buttonManagerReference;
    public GameObject ballManagerReference;

    public void SetCurrentNumBall(int _currentNumBall)
    {
        ballManagerReference.GetComponent<BallScript>().ballSpriteNum = _currentNumBall;
    }

    private void UnlockBallAdorns()
    {
        if (GetInMenuValue())
        {
            if (PlayerPrefs.GetInt("Best_Score") >= 1)
                lockAdornsBallon[0].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 10)
                lockAdornsBallon[1].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 20)
                lockAdornsBallon[2].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 30)
                lockAdornsBallon[3].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 40)
                lockAdornsBallon[4].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 50)
                lockAdornsBallon[5].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 60)
                lockAdornsBallon[6].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 70)
                lockAdornsBallon[7].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 80)
                lockAdornsBallon[8].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 90)
                lockAdornsBallon[9].SetActive(true);
        }
        else
            for (int i = 0; i < lockAdornsBallon.Length; i++)
                lockAdornsBallon[i].SetActive(false);
    }
    private bool GetInMenuValue() 
    {
        return buttonManagerReference.GetComponent<ButtonsScript>().SetInMenuValue();
    }

    private void Awake()
    {
        buttonManagerReference = GameObject.Find("ButtonManager_Container");
        UnlockBallAdorns();
    }
    private void Update()
    {
        UnlockBallAdorns();
    }
}
