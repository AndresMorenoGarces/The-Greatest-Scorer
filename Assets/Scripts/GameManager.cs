using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject ballObjectReference;
    private GameObject wallPaper;
    private GameObject SpikeObjectReference;
    private int colorInt = 0, scorePoint = 0, bestScore, lastScore, levelUp = 1;
    private float gameVelocity = 0.05f, currentTimeScale = 1;
    private bool oneTime = false;

    public int SetScore()
    {
        return scorePoint;
    }
    public float SetCurrentTimeScale()
    {
        return currentTimeScale;
    }
    public int SetLevelUp() 
    {
        return levelUp;
    }

    public void UpdateScore()
    {
        scorePoint++;
    }
    public void SaveTemporalScore()
    {
        lastScore = scorePoint;
        PlayerPrefs.SetInt("Last_Score", lastScore);
    }
    public void SaveTemporalHardcoreScore()
    {
        lastScore = scorePoint;
        PlayerPrefs.SetInt("Hardcore_Last_Score", lastScore);
    }
    public void SaveBestScore()
    {
        if (scorePoint > PlayerPrefs.GetInt("Best_Score"))
        {
            bestScore = scorePoint;
            PlayerPrefs.SetInt("Best_Score", bestScore);
        }
    }
    public void SaveHardcoreBestScore()
    {
        if (scorePoint > PlayerPrefs.GetInt("Hardcore_Best_Score"))
        {
            bestScore = scorePoint;
            PlayerPrefs.SetInt("Hardcore_Best_Score", bestScore);
        }
    }
    public void HardcoreGame()
    {
        //TypeOfGame(2.15f, 4);
    }
    
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        wallPaper = GameObject.Find("WallPaper");
        SpikeObjectReference = GameObject.Find("SpikeManager_Container");
        ballObjectReference = GameObject.Find("BallObject_Container");
    }
}