using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Sprite[] ballSprites;
    public Gradient[] colorParticlesList;
    public ParticleSystem particleSystemBall;
    public Transform particlesTransform;

    [HideInInspector] public int ballSpriteNum = 0;

    private GameObject spikeObjectReference;
    public GameObject buttonObjectReference;
    private Rigidbody2D ballRigidBody;
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule;
    private int  particlesColorInt = 0, ballDirection = 1;
    private float numberParticles = 0;

    private void GetSpikePosition(bool hitLeftWall) 
    {
        spikeObjectReference.GetComponent<SpikeScript>().SelectCurrentSpikes(hitLeftWall);
    }

    public void StartSpriteBall()
    {
        if (ballSpriteNum < ballSprites.Length)
        {
            GetComponent<SpriteRenderer>().sprite = ballSprites[ballSpriteNum];
            UpgradeParticlesBall();
        }
    }
    public void ChangeBallSprite()
    {
        if (GameManager.instance.SetScore() >= (ballSpriteNum + 1) * 10)
        {
            ballSpriteNum++;
            if (ballSpriteNum < ballSprites.Length)
                GetComponent<SpriteRenderer>().sprite = ballSprites[ballSpriteNum];
        }
    }
    public void UpgradeParticlesBall()
    {
        particlesColorInt = ballSpriteNum;
        if (particlesColorInt < colorParticlesList.Length)
            colorOverLifetimeModule.color = colorParticlesList[particlesColorInt];
        if (numberParticles <= 100)
            numberParticles = 10 * particlesColorInt;
        emissionModule.rateOverTime = numberParticles;
    }
    private void BallMove()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale != 0)
        {
            ballRigidBody.AddForce(new Vector2(0, 200));
            ballRigidBody.velocity = Vector2.zero;
            AudioScript.instance.BallSound();
        }
        ballRigidBody.velocity = new Vector2(1.5f * ballDirection, ballRigidBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D colission)
    {
        if (colission.transform.tag == "Left Wall" || colission.transform.tag == "Right Wall")
        {
            if (colission.transform.tag == "Left Wall")
                GetSpikePosition(true);
            else if (colission.transform.tag == "Right Wall")
                GetSpikePosition(false);
            ballDirection *= -1;
            GameManager.instance.UpdateScore();
            AudioScript.instance.WallHitSound();
        }
    }
    private void Awake()
    {
        spikeObjectReference = GameObject.Find("SpikeManager_Container");
        emissionModule = particleSystemBall.emission;
        colorOverLifetimeModule = particleSystemBall.colorOverLifetime;
    }
    private void Start()
    {
        ballRigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        BallMove();
        print(ballSpriteNum);
    }
}