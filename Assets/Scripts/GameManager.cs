using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("GameObjects")]
    public GameObject ball;
    public GameObject quad;
    public GameObject musicContainer;
    public GameObject menuInterface;
    public GameObject instructionsInterface;
    public GameObject gameInterface;
    public GameObject pauseInterface;
    public GameObject exitInterface;

    [Header("Transforms")]
    public Transform particlesTransform;
    public Transform spikes;
    public Transform leftSpikesHide;
    public Transform leftSpikesShow;
    public Transform rightSpikesHide;
    public Transform rightSpikesShow;
    public GameObject rightSpikesToMove;
    public GameObject leftSpikesToMove;

    [Header("Buttons")]
    public Button buttonRestart;
    public Button buttonPause;
    public Button backButton;
    public Button ExitButton;

    [Header("TextMeshPro")]
    public TextMeshPro beginText;
    public TextMeshPro counter;
    public TextMeshPro lastScoreText;
    public TextMeshPro bestScoreText;

    [Header("AudioClip")]
    public AudioClip ballSound;
    public AudioClip wallSound;
    public AudioClip loseSound;
    public AudioClip firstSong;
    public AudioClip secondSong;

    [Header("ParticleSystem")]
    public ParticleSystem particleSystemBall;

    [Header("Transform Arrays")]

    [Header("Arrays")]
    public Transform[] LeftSpikes;
    public Transform[] RightSpikes;
    public Transform[] up_DownSpikes;
    [Header("Sprite Arrays")]
    public Sprite[] ballSprites;
    [Header("GameObject Arrays")]
    public GameObject[] lockAdornsBallon;
    [Header("Color Arrays")]
    public Color[] colorList;
    public Color[] colorSpikeList;
    [Header("Gradient Arrays")]
    public Gradient[] colorParticlesList;

    [HideInInspector]
    public int ballNumber = 0;
    int score = 0;
    int bestScore;
    int lastScore;
    int colorInt = 0;
    int randomSpike = 0;
    int colorIntSpikes = 0;
    int particlesColorInt = -1;

    float randomRightSpikesInt;
    float randomLeftSpikesInt;
    float constantRandomRightSpikesInt;
    float constantRandomLeftSpikesInt;
    float adittionForScore = 0;
    float gameVelocity = 0.075f;
    float currentTimeScale = 1;
    float numberParticles = 0;

    Transform leftSpikesDual;
    Transform rightSpikesDual;

    bool loadFirstSong = false;
    bool active = false;
    bool isLeftSpikeMoving = false;
    bool isRightSpikeMoving = false;
    bool inGame = false;

    AudioSource audioSource;

    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule;

    
    public void Instructions()
    {
        instructionsInterface.gameObject.SetActive(true);
        menuInterface.gameObject.SetActive(false);
    }
    public void Back()
    {
        menuInterface.gameObject.SetActive(true);
        instructionsInterface.gameObject.SetActive(false);
    }
    public void BeginGame()
    {
        ChanceBallSprite();
        UpgradeParticlesBall();
        Time.timeScale = 1.15f;
        for (int i = 0; i < up_DownSpikes.Length; i++)
            up_DownSpikes[i].gameObject.SetActive(true);
        menuInterface.gameObject.SetActive(false);
        gameInterface.gameObject.SetActive(true);
        ball.SetActive(true);
        buttonPause.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(false);
        inGame = true;
    }
    public void HardcoreGame()
    {
        ChanceBallSprite();
        UpgradeParticlesBall();
        Time.timeScale = 2.15f;
        adittionForScore = 4;
        for (int i = 0; i < up_DownSpikes.Length; i++)
            up_DownSpikes[i].gameObject.SetActive(true);
        menuInterface.gameObject.SetActive(false);
        gameInterface.gameObject.SetActive(true);
        ball.SetActive(true);
        buttonPause.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(false);
        inGame = true;
    }
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
                SpikesColor();
                if (adittionForScore < 5)
                    adittionForScore++;
            }
        }
    }
    public void ChangeSpikePosition(bool leftWall)
    {
        if (leftWall == true)
        {
            randomRightSpikesInt = constantRandomRightSpikesInt;

            for (int i = 0; i < randomRightSpikesInt; i++)
            {
                for (int j = 0; j < leftSpikesHide.childCount; j++)
                    LeftSpikes[j].SetParent(leftSpikesToMove.transform);

                randomSpike = Random.Range(0, RightSpikes.Length);
                RightSpikes[randomSpike].SetParent(rightSpikesToMove.transform);
            }
            isRightSpikeMoving = true;
            isLeftSpikeMoving = false;
        }
        else if (leftWall == false)
        {
            randomLeftSpikesInt = constantRandomLeftSpikesInt;

            for (int i = 0; i < randomLeftSpikesInt; i++)
            {
                for (int j = 0; j < rightSpikesHide.childCount; j++)
                    RightSpikes[j].SetParent(rightSpikesToMove.transform);
                randomSpike = Random.Range(0, LeftSpikes.Length);
                LeftSpikes[randomSpike].SetParent(leftSpikesToMove.transform);
            }
            isLeftSpikeMoving = true;
            isRightSpikeMoving = false;
        }
    }
    void MovingSpikes()
    {
        leftSpikesDual = (isLeftSpikeMoving) ? leftSpikesShow : leftSpikesHide;
        rightSpikesDual = (isRightSpikeMoving) ? rightSpikesShow : rightSpikesHide;

        if (leftSpikesToMove.transform.position == new Vector3(leftSpikesHide.position.x, leftSpikesToMove.transform.position.y, leftSpikesToMove.transform.position.z) && isLeftSpikeMoving == false)
        {
            for (int i = 0; i < LeftSpikes.Length; i++)
                LeftSpikes[i].transform.SetParent(spikes);
        }
        if (rightSpikesToMove.transform.position == new Vector3(rightSpikesHide.position.x, rightSpikesToMove.transform.position.y, rightSpikesToMove.transform.position.z) && isRightSpikeMoving == false)
        {
            for (int i = 0; i < RightSpikes.Length; i++)
                RightSpikes[i].transform.SetParent(spikes);
        }

        rightSpikesToMove.transform.position = Vector3.MoveTowards(rightSpikesToMove.transform.position,
            new Vector3(rightSpikesDual.position.x, rightSpikesToMove.transform.position.y, rightSpikesToMove.transform.position.z), 1.5f * Time.deltaTime);

        leftSpikesToMove.transform.position = Vector3.MoveTowards(leftSpikesToMove.transform.position,
            new Vector3(leftSpikesDual.position.x, leftSpikesToMove.transform.position.y, leftSpikesToMove.transform.position.z), 1.5f * Time.deltaTime);
    }
    void SpikesColor()
    {
        if (colorIntSpikes < colorSpikeList.Length)
        {
            for (int i = 0; i < up_DownSpikes.Length; i++)
                up_DownSpikes[i].GetComponent<SpriteRenderer>().color = colorSpikeList[(colorIntSpikes)];
            for (int j = 0; j < LeftSpikes.Length; j++)
                LeftSpikes[j].GetComponent<SpriteRenderer>().color = colorSpikeList[(colorIntSpikes)];
            for (int l = 0; l < RightSpikes.Length; l++)
                RightSpikes[l].GetComponent<SpriteRenderer>().color = colorSpikeList[(colorIntSpikes)];
            colorIntSpikes++;
        }
        else
            colorIntSpikes = 0;
    }
    void ChanceBallSprite()
    {
        if (score >= (ballNumber + 1) * 10)
        {
            if (ballNumber < ballSprites.Length)
            {
                if (score > 0)
                    ballNumber++;
            }
            else
                ballNumber = ballSprites.Length;
        }
        ball.GetComponent<SpriteRenderer>().sprite = ballSprites[ballNumber];
    }
    void UpgradeParticlesBall()
    {
        particlesColorInt = ballNumber;

        if (numberParticles <= 100)
            numberParticles = 10 * ballNumber;
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
        else
            colorInt = 0;
    }
    public void UpdateScore()
    {
        counter.text = "" + (score + 1);
        score++;
    }
    public void PauseMode()
    {
        active = !active;

        counter.gameObject.GetComponent<TextMeshPro>().sortingOrder = (active) ? -1 : 0;
        Time.timeScale = (active) ? 0 : currentTimeScale;
        pauseInterface.SetActive(active);
    }
    void LoseGame()
    {
        if (ball == null)
        {
            counter.gameObject.GetComponent<TextMeshPro>().sortingOrder = 3;
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
    public void RestartGame()
    {
        inGame = false;
        SceneManager.LoadScene("SampleScene");
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
    public void ExitGame()
    {
        Application.Quit();
    }
    public void CancelExitGame()
    {
        exitInterface.SetActive(false);
        menuInterface.SetActive(true);
    }
    public void ExitQuestion()
    {
        exitInterface.SetActive(true);
        menuInterface.SetActive(false);
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

    void UnlockBallAdorns()
    {
        if (inGame == false)
        {
            if (PlayerPrefs.GetInt("Best_Score") >= 10)
                lockAdornsBallon[0].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 20)
                lockAdornsBallon[1].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 30)
                lockAdornsBallon[2].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 40)
                lockAdornsBallon[3].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 50)
                lockAdornsBallon[4].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 60)
                lockAdornsBallon[5].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 70)
                lockAdornsBallon[6].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 80)
                lockAdornsBallon[7].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 90)
                lockAdornsBallon[8].SetActive(true);
            if (PlayerPrefs.GetInt("Best_Score") >= 100)
                lockAdornsBallon[9].SetActive(true);
        }
        else
            for (int i = 0; i < lockAdornsBallon.Length; i++)
                lockAdornsBallon[i].SetActive(false);
    }

    void Awake()
    {
        audioSource = musicContainer.GetComponent<AudioSource>();
        ball.SetActive(false);
        emissionModule = particleSystemBall.emission;
        colorOverLifetimeModule = particleSystemBall.colorOverLifetime;
        //PlayerPrefs.SetInt("Best_Score", 0);
        //PlayerPrefs.SetInt("Last_Score", 0);
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        LoadScore();
        gameInterface.gameObject.SetActive(false);
        buttonPause.gameObject.SetActive(false);
        pauseInterface.SetActive(false);
        menuInterface.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        buttonRestart.gameObject.SetActive(false);
    }

    void Update()
    {
        constantRandomLeftSpikesInt = Random.Range(0 + adittionForScore, LeftSpikes.Length - 5 + adittionForScore);
        constantRandomRightSpikesInt = Random.Range(0 + adittionForScore, RightSpikes.Length - 5 + adittionForScore);
        SongsChange();
        LoseGame();
        MovingSpikes();
        UnlockBallAdorns();
    }
}
