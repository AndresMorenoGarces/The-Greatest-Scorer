using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    public Transform[] up_DownSpikes;
    public Transform leftSpikesHide;
    public Transform leftSpikesShow;
    Transform leftSpikesDual;
    public Transform rightSpikesHide;
    public Transform rightSpikesShow;
    Transform rightSpikesDual;
    public Transform particlesTransform;
    public GameObject rightSpikesToMove;
    public GameObject leftSpikesToMove;

    int score = 0;
    int randomSpike = 0;
    int bestScore;
    int lastScore;

    [HideInInspector]
    public int colorInt = 0;

    int particlesColorInt = -1;
    int ballNumber = 0;
    float randomRightSpikesInt;
    float randomLeftSpikesInt;
    float constantRandomRightSpikesInt;
    float constantRandomLeftSpikesInt;
    float adittionForScore = 0;
    float gameVelocity = 0.075f;
    float currentTimeScale = 1;
    float numberParticles = 0;

    public Button buttonBegin;
    public Button buttonRestart;
    public Button buttonPause;
    public Button exitButton;
    public Button instructionButton;

    public Text beginText;
    public Text restartText;
    public TextMeshPro title;
    public TextMeshPro credits;
    public TextMeshPro tapText;
    public TextMeshPro counter;
    public TextMeshPro instructionText;
    public TextMeshPro lastScoreText;
    public TextMeshPro bestScoreText;

    public GameObject pauseObject;
    public Image counterImage;

    public Color[] colorList;
    public Gradient[] colorParticlesList;

    public Sprite[] ballSprites;
    public AudioClip ballSound;
    public AudioClip wallSound;
    public AudioClip loseSound;
    public AudioClip firstSong;
    public AudioClip secondSong;

    AudioSource audioSource;

    bool loadFirstSong = false;
    bool active = false;
    bool isLeftSpikeMoving = false;
    bool isRightSpikeMoving = false;

    public ParticleSystem particleSystemBall;
    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule;

    public void FunctionsActivator()
    {
        if (score % 5 == 0)
        {
            ChanceQuadMaterial();
            if (gameVelocity <= 1f)
            {
                Time.timeScale += gameVelocity;
                currentTimeScale = Time.timeScale;
            }
            if (score % 10 == 0)
            {
                ChanceBallSprite();
                UpgradeParticlesBall();
                if (adittionForScore < 6)
                    adittionForScore++;
            }
        }
    }

    public void BeginGame()
    {
        Time.timeScale = 1.15f;
        ball.SetActive(true);
        for (int i = 0; i < up_DownSpikes.Length; i++)
            up_DownSpikes[i].gameObject.SetActive(true);
        buttonBegin.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        tapText.gameObject.SetActive(true);
        counter.gameObject.SetActive(true);
        counterImage.gameObject.SetActive(true);
        buttonPause.gameObject.SetActive(true);
        instructionButton.gameObject.SetActive(false);
    }

    void LoseGame()
    {
        if (ball == null)
        {
            if (score >= 10 && score < 20)
                counter.text = "Good \n" + score;
            else if (score >= 20 && score < 30)
                counter.text = "Cool \n" + score;
            else if (score >= 30 && score < 40)
                counter.text = "Amazing \n" + score;
            else if (score >= 40 && score < 50)
                counter.text = "Boss \n" + score;
            else if (score >= 50 && score < 60)
                counter.text = "Titan \n" + score;
            else if (score >= 60 && score < 70)
                counter.text = "Unreal \n" + score;
            else if (score >= 70 && score < 80)
                counter.text = "Real Pro \n" + score;
            else if (score >= 80 && score < 90)
                counter.text = "Wizard \n" + score;
            else if (score >= 90 && score < 100)
                counter.text = "The Best \n" + score;
            else if (score >= 100 && score < 200)
                counter.text = "Legend \n" + score;
            else if (score >= 200)
                counter.text = "God \n" + score;
            else
                counter.text = "Again \n" + score;

            buttonRestart.gameObject.SetActive(true);
            buttonPause.gameObject.SetActive(false);
        }
    }

    public void PauseMode()
    {
        active = !active;

        counter.gameObject.GetComponent<TextMeshPro>().sortingOrder = (active) ? -1 : 0;
        Time.timeScale = (active) ? 0 : currentTimeScale;
        pauseObject.SetActive(active);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Instructions()
    {
        instructionText.gameObject.SetActive(true);
        instructionButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
        title.gameObject.SetActive(false);
        buttonBegin.gameObject.SetActive(false);
        lastScoreText.gameObject.SetActive(false);
        bestScoreText.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
    }
    public void Exit()
    {
        instructionText.gameObject.SetActive(false);
        instructionButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
        title.gameObject.SetActive(true);
        buttonBegin.gameObject.SetActive(true);
        lastScoreText.gameObject.SetActive(true);
        bestScoreText.gameObject.SetActive(true);
        credits.gameObject.SetActive(true);
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
            randomRightSpikesInt = constantRandomRightSpikesInt;

            for (int i = 0; i < randomRightSpikesInt; i++)
            {
                for (int j = 0; j < LeftSpikes.Length; j++)
                    LeftSpikes[j].SetParent(leftSpikesToMove.transform);
                randomSpike = Random.Range(0, RightSpikes.Length);
                RightSpikes[randomSpike].SetParent(rightSpikesToMove.transform);
                SpikesColor(RightSpikes[randomSpike].gameObject);
            }

            isRightSpikeMoving = true;
            isLeftSpikeMoving = false;

            if (leftSpikesToMove.transform.position != new Vector3(leftSpikesHide.position.x, leftSpikesToMove.transform.position.y, leftSpikesToMove.transform.position.z))
            {
                leftSpikesToMove.transform.position = Vector3.MoveTowards(leftSpikesToMove.transform.position,
                    new Vector3(leftSpikesHide.position.x, leftSpikesToMove.transform.position.y, leftSpikesToMove.transform.position.z), 1);
            }

            for (int i = 0; i < LeftSpikes.Length; i++)
            {
                LeftSpikes[i].transform.SetParent(null);
                LeftSpikes[i].transform.position = new Vector3(leftSpikesHide.position.x, LeftSpikes[i].transform.position.y, LeftSpikes[i].transform.position.z);
            }
        }
        else if (leftWall == false)
        {
            randomLeftSpikesInt = constantRandomLeftSpikesInt;

            for (int i = 0; i < randomLeftSpikesInt; i++)
            {
                for (int j = 0; j < RightSpikes.Length; j++)
                    RightSpikes[j].SetParent(rightSpikesToMove.transform);
                randomSpike = Random.Range(0, LeftSpikes.Length);
                LeftSpikes[randomSpike].SetParent(leftSpikesToMove.transform);
                SpikesColor(LeftSpikes[randomSpike].gameObject);
            }

            isLeftSpikeMoving = true;
            isRightSpikeMoving = false;

            if (rightSpikesToMove.transform.position != new Vector3(rightSpikesHide.position.x, rightSpikesToMove.transform.position.y, rightSpikesToMove.transform.position.z))
            {
                rightSpikesToMove.transform.position = Vector3.MoveTowards(rightSpikesToMove.transform.position,
                    new Vector3(rightSpikesHide.position.x, rightSpikesToMove.transform.position.y, rightSpikesToMove.transform.position.z), 1);
            }

            for (int i = 0; i < RightSpikes.Length; i++)
            {
                RightSpikes[i].transform.SetParent(null);
                RightSpikes[i].transform.position = new Vector3(rightSpikesHide.position.x, RightSpikes[i].transform.position.y, RightSpikes[i].transform.position.z);

            }
        }
    }

    void MovingSpikes()
    {
        leftSpikesDual = (isLeftSpikeMoving) ? leftSpikesShow : leftSpikesHide;
        rightSpikesDual = (isRightSpikeMoving) ? rightSpikesShow : rightSpikesHide;

        rightSpikesToMove.transform.position = Vector3.MoveTowards(rightSpikesToMove.transform.position,
            new Vector3(rightSpikesDual.position.x, rightSpikesToMove.transform.position.y, rightSpikesToMove.transform.position.z), 1f * Time.deltaTime);

        leftSpikesToMove.transform.position = Vector3.MoveTowards(leftSpikesToMove.transform.position,
            new Vector3(leftSpikesDual.position.x, leftSpikesToMove.transform.position.y, leftSpikesToMove.transform.position.z), 1f * Time.deltaTime);
    }

    void SpikesColor(GameObject _spike)
    {
        if (colorInt >= colorList.Length)
            colorInt = 0;
        else
            _spike.GetComponent<Renderer>().material.color = colorList[(colorInt)];
    }

    void ChanceBallSprite()
    {
        if (ballNumber < ballSprites.Length - 1)
        {
            ballNumber++;
            ball.GetComponent<SpriteRenderer>().sprite = ballSprites[ballNumber];
        }
        else
            ballNumber = 0;
    }

    void UpgradeParticlesBall()
    {
        particlesColorInt++;

        if (numberParticles <= 100)
            numberParticles += 10;
        emissionModule.rateOverTime = numberParticles;
        colorOverLifetimeModule.color = colorParticlesList[particlesColorInt];

        if (particlesColorInt == colorParticlesList.Length)
            particlesColorInt = 0;
    }

    void ChanceQuadMaterial()
    {
        if (colorInt < colorList.Length - 1)
        {
            colorInt++;
            quad.GetComponent<SpriteRenderer>().material.color = colorList[colorInt];
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
            loadFirstSong = !loadFirstSong;
            audioSource.clip = loadFirstSong ? firstSong  :  secondSong;
            audioSource.Play();
        }
    }

    void Awake()
    {
        audioSource = musicContainer.GetComponent<AudioSource>();
        ball.SetActive(false);
        emissionModule = particleSystemBall.emission;
        colorOverLifetimeModule = particleSystemBall.colorOverLifetime;       

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        LoadScore();
        counter.gameObject.SetActive(false);
        tapText.gameObject.SetActive(false);
        buttonPause.gameObject.SetActive(false);
        pauseObject.SetActive(false);
        counterImage.gameObject.SetActive(false);
        buttonBegin.gameObject.SetActive(true);
        buttonRestart.gameObject.SetActive(false);
    }

    void Update()
    {
        SongsChange();
        LoseGame();
        constantRandomRightSpikesInt = Random.Range(0 + adittionForScore, RightSpikes.Length - 5 + adittionForScore);
        constantRandomLeftSpikesInt = Random.Range(0 + adittionForScore, LeftSpikes.Length - 5 + adittionForScore);

        MovingSpikes();
    }
    
}
