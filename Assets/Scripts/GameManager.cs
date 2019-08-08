using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ball;
    public Transform[] LeftSpikes;
    public Transform[] RightSpikes;
    int randomSpike = 0;
    [SerializeField]
    float adittionForScore = 0;
    public Button buttonRestart;
    public Text counter;
    int score = 1;



    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void UpdateScore()
    {
        counter.text = "" + score;
        score++;

    }

    public void ChangeSpikePosition(bool leftWall)
    {
        if (leftWall == true)
        {
            for (int i = 0; i < Random.Range(1 + adittionForScore, LeftSpikes.Length -2); i++)
            {
                for (int j = 0; j < RightSpikes.Length; j++)
                {
                    LeftSpikes[j].gameObject.SetActive(false);
                }
                randomSpike = Random.Range(0, LeftSpikes.Length - 1);
                RightSpikes[randomSpike].gameObject.SetActive(true);
            }
        }
        else if (leftWall == false)
        {
            for (int i = 0; i < Random.Range(1 + adittionForScore, LeftSpikes.Length -2); i++)
            {
                for (int j = 0; j < LeftSpikes.Length; j++)
                {
                    RightSpikes[j].gameObject.SetActive(false);
                }
                randomSpike = Random.Range(0, RightSpikes.Length - 1);
                LeftSpikes[randomSpike].gameObject.SetActive(true);
            }
            
        }

        if (score == score%10 && adittionForScore < 3)
        {
            adittionForScore++;       
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        buttonRestart.gameObject.SetActive(false);
        
    }

    void Update()
    {
        if (ball == null)
        {
            counter.text = "You Lose";
            buttonRestart.gameObject.SetActive(true);
        }
    }
    
}
