using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject ball;
    public GameObject quad;
    public GameObject musicContainer;

    public Transform[] LeftSpikes;
    public Transform[] RightSpikes;

    int score = 0;
    int randomSpike = 0;
    int bestScore;
    int lastScore;

    [HideInInspector]
    public int colorInt = 0;
    int ballNumber = 0;

    float adittionForScore = 0;
    float gameVelocity = 0.075f;

    public Button buttonBegin;
    public Button buttonRestart;
    //public Button buttonPause;

    public Text beginText;
    public Text restartText;

    public Text title;
    public Text credits;
    public Text counter;

    public Text lastScoreText;
    public Text bestScoreText;

    public Image counterImage;

    public Color[] colorList;

    public Sprite[] ballSprites;

    public AudioClip ballSound;

    public AudioClip wallSound;  

    public AudioClip loseSound;

    public AudioClip firstSong;

    public AudioClip secondSong;

    AudioSource audioSource;
    

    bool active;


    public void FunctionsActivator()
    {
        if (score % 5 == 0)
        {
            ChanceQuadMaterial();
            if (gameVelocity <= 1f)
            {
                Time.timeScale += gameVelocity;
            }
            if (score % 10 == 0)
            {
                ChanceBallSprite();
                if (adittionForScore < 6)
                {
                    adittionForScore++;    
                }
            }
        } 
    }

    public void BeginGame()
    {
        Time.timeScale = 1f;
        ball.SetActive(true);
        buttonBegin.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        counter.gameObject.SetActive(true);
        counterImage.gameObject.SetActive(true);
        //buttonPause.gameObject.SetActive(true);
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

    //public void PauseMode()
    //{
    //        active = !active;
    //        Time.timeScale = (active) ? 0 : 1;
    //}

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        beginText.text = "START";
    }

    public void UpdateScore()
    {
        counter.text = "" + (score + 1);
        score++;

    }

    public void SaveTemporalScore()
    {      
        lastScore = score;
        lastScoreText.text = "Last Score:\n" + PlayerPrefs.GetInt("Last_Score");
        PlayerPrefs.SetInt("Last_Score", lastScore);
    }

    public void SaveBestScore()
    {
        if (score > PlayerPrefs.GetInt("Best_Score"))
        {
            bestScore = score;
            Debug.Log("Score" + (score));
            Debug.Log(bestScore);
            PlayerPrefs.SetInt("Best_Score", bestScore);
        }
        bestScoreText.text = "Best Score:\n" + PlayerPrefs.GetInt("Best_Score");
    }

    void LoadScore()
    {
       lastScoreText.text = "Last Score:\n" + PlayerPrefs.GetInt("Last_Score");
       bestScoreText.text = "Best Score:\n" + PlayerPrefs.GetInt("Best_Score");
    }

    public void ChangeSpikePosition(bool leftWall)
    {
        if (leftWall == true)
        {
            for (int i = 0; i < Random.Range(1 + adittionForScore, RightSpikes.Length - 6 + adittionForScore); i++)
            {
                for (int j = 0; j < LeftSpikes.Length; j++)
                {
                    LeftSpikes[j].gameObject.SetActive(false);
                }
                randomSpike = Random.Range(0, RightSpikes.Length);
                RightSpikes[randomSpike].gameObject.SetActive(true);
                SpikesColor(RightSpikes[randomSpike].gameObject);
            }
        }
        else if (leftWall == false)
        {
            for (int i = 0; i < Random.Range(1 + adittionForScore, LeftSpikes.Length -6 + adittionForScore); i++)
            {
                for (int j = 0; j < RightSpikes.Length; j++)
                {
                    RightSpikes[j].gameObject.SetActive(false);
                }
                randomSpike = Random.Range(0, LeftSpikes.Length);
                LeftSpikes[randomSpike].gameObject.SetActive(true);
                SpikesColor(LeftSpikes[randomSpike].gameObject);
            }
        }    
    }

    void SpikesColor(GameObject _spike)
    {
        if (colorInt < colorList.Length - 1)
        {
            _spike.GetComponent<Renderer>().material.color = colorList[(colorInt + 1)];
        }
    }

    void ChanceBallSprite()
    {
        if (ballNumber < ballSprites.Length - 1)
        {
            ballNumber++;
            ball.GetComponent<SpriteRenderer>().sprite = ballSprites[ballNumber];
        }
        else 
        {
            ballNumber = 0;
        }
    }

    void ChanceQuadMaterial()
    {
        if (colorInt < colorList.Length - 1)
        {
            colorInt++;
            quad.GetComponent<MeshRenderer>().material.color = colorList[colorInt];
        }
    }

    public void BallSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(ballSound);
    }

    public void WallHitSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(wallSound);
    }

    public void LoseHitSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(loseSound);
    }

    public void SongsChange()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.clip = firstSong;
            audioSource.Play();
        }
    }

    void Awake()
    {
        title.text = " The \n Greatest \nScorer";
        credits.text = "Developer \nAndrés F. Moreno Garcés";
        audioSource = musicContainer.GetComponent<AudioSource>();
        ball.SetActive(false);
        //buttonPause.gameObject.SetActive(false);


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
        LoadScore();
        counter.gameObject.SetActive(false);
        counterImage.gameObject.SetActive(false);
        buttonBegin.gameObject.SetActive(true);
        buttonRestart.gameObject.SetActive(false);
    }

    void Update()
    {
        SongsChange();
        LoseGame();
    }
    
}
