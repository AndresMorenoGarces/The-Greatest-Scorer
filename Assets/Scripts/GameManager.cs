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

    int score = 1;
    int randomSpike = 0;

    [HideInInspector]
    public int colorInt = 5;

    public float adittionForScore = 0;

    public Button buttonBegin;
    public Button buttonRestart;

    public Text beginText;
    public Text restartText;

    public Text title;
    public Text credits;
    public Text counter;

    public Image counterImage;

    public Color[] colorList = new Color[5];

    public void BeginGame()
    {
        ball.SetActive(true);
        buttonBegin.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        counter.gameObject.SetActive(true);
        counterImage.gameObject.SetActive(true);
    }

    void LoseGame()
    {
        if (ball == null)
        {
            counter.text = "You Lose";
            restartText.text = "RESTART";
            buttonRestart.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        beginText.text = "START";
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
                RandomSpikeColor(RightSpikes[randomSpike].gameObject);
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
                RandomSpikeColor(LeftSpikes[randomSpike].gameObject);
            }

        }

        

        if (score % 5 == 0)
        {
            if (score % 10 == 0 && adittionForScore < 4)
            {
                adittionForScore++;
            }

            colorInt = Random.Range(0, 5);           
        }

        void RandomSpikeColor(GameObject _spike)
        {
            switch (colorInt)
            {
                case 0:
                    _spike.GetComponent<SpriteRenderer>().color = colorList[0];
                    break;
                case 1:
                    _spike.GetComponent<SpriteRenderer>().color = colorList[1];
                    break;
                case 2:
                    _spike.GetComponent<SpriteRenderer>().color = colorList[2];
                    break;
                case 3:
                    _spike.GetComponent<SpriteRenderer>().color = colorList[3];
                    break;
                case 4:
                    _spike.GetComponent<SpriteRenderer>().color = colorList[4];
                    break;
            }
        }
    }

    

    void Awake()
    {
        title.text = " The \n Greatest \nScorer";
        credits.text = "Developer \n Andrés F. Moreno Garcés";

        ball.SetActive(false);

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
        counter.gameObject.SetActive(false);
        counterImage.gameObject.SetActive(false);
        buttonBegin.gameObject.SetActive(true);
        buttonRestart.gameObject.SetActive(false);

    }

    void Update()
    {
        LoseGame();
    }
    
}
