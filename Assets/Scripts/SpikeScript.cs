using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public Color[] colorSpikeList;
    public Transform[] LeftSpikes, RightSpikes, up_DownSpikes;

    private GameObject buttonManagerReference;
    private GameObject leftSpikesToMove, rightSpikesToMove;
    private GameObject uIManagerReference; 
    private Transform spikesContainer, leftSpikesHide, leftSpikesShow, rightSpikesHide, rightSpikesShow;
    private Transform leftSpikesDual, rightSpikesDual;  
    private float randomRightSpikesInt = 0, randomLeftSpikesInt = 0, constantRandomRightSpikesInt = 0, constantRandomLeftSpikesInt = 0;
    private int randomSpike = 0, colorIntSpikes = 0;
    private bool isLeftSpikeShowing = false;
    private bool isArcadeGame = false;

    private int GetLevelUp()
    {
        return GameManager.instance.SetLevelUp();
    }
    public void GetLoseState(bool _youLose)
    {
        uIManagerReference.GetComponent<UIScript>().GetLoseState(_youLose); 
    }
    public bool GetActiveGameType() 
    {
        return buttonManagerReference.GetComponent<ButtonsScript>().SetActiveGameType();
    }

    private void MovingSpikes()
    {
        leftSpikesDual = (isLeftSpikeShowing) ? leftSpikesShow : leftSpikesHide;
        rightSpikesDual = (!isLeftSpikeShowing) ? rightSpikesShow : rightSpikesHide;

        if (leftSpikesToMove.transform.position == new Vector3(leftSpikesHide.position.x, leftSpikesToMove.transform.position.y, leftSpikesToMove.transform.position.z) && isLeftSpikeShowing == false)
            for (int i = 0; i < LeftSpikes.Length; i++)
                LeftSpikes[i].transform.SetParent(spikesContainer);
        else if (rightSpikesToMove.transform.position == new Vector3(rightSpikesHide.position.x, rightSpikesToMove.transform.position.y, rightSpikesToMove.transform.position.z) && isLeftSpikeShowing == true)
            for (int i = 0; i < RightSpikes.Length; i++)
                RightSpikes[i].transform.SetParent(spikesContainer);

        rightSpikesToMove.transform.position = Vector3.MoveTowards(rightSpikesToMove.transform.position,
            new Vector3(rightSpikesDual.position.x, rightSpikesToMove.transform.position.y, rightSpikesToMove.transform.position.z), 1.5f * Time.deltaTime);
        leftSpikesToMove.transform.position = Vector3.MoveTowards(leftSpikesToMove.transform.position,
            new Vector3(leftSpikesDual.position.x, leftSpikesToMove.transform.position.y, leftSpikesToMove.transform.position.z), 1.5f * Time.deltaTime);
    }
    public void SelectCurrentSpikes(bool leftWall)
    {
        if (leftWall)
        {
            randomRightSpikesInt = constantRandomRightSpikesInt;
            for (int i = 0; i < randomRightSpikesInt; i++)
            {
                for (int j = 0; j < leftSpikesHide.childCount; j++)
                    LeftSpikes[j].SetParent(leftSpikesToMove.transform);

                randomSpike = Random.Range(0, RightSpikes.Length);
                RightSpikes[randomSpike].SetParent(rightSpikesToMove.transform);
            }
            isLeftSpikeShowing = false;
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
            isLeftSpikeShowing = true;
        }
    }
    public void ActiveUp_DownSpikes() 
    {
        for (int i = 0; i < up_DownSpikes.Length; i++)
            up_DownSpikes[i].gameObject.SetActive(true);
    }
    public void SpikesColor()
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

    private void Awake()
    {
        uIManagerReference = GameObject.Find("UIManager_Container");
        buttonManagerReference = GameObject.Find("ButtonManager_Container");

        leftSpikesToMove = GameObject.Find("leftSpikesToMove");
        rightSpikesToMove = GameObject.Find("rightSpikesToMove");
        spikesContainer = GameObject.Find("spikesContainer").transform;
        leftSpikesHide = GameObject.Find("leftSpikesHide").transform;
        leftSpikesShow = GameObject.Find("leftSpikesShow").transform;
        rightSpikesHide = GameObject.Find("rightSpikesHide").transform;
        rightSpikesShow = GameObject.Find("rightSpikesShow").transform;
    }
    private void Update()
    {
        constantRandomLeftSpikesInt = Random.Range(0 + GetLevelUp(), LeftSpikes.Length - 5 + GetLevelUp());
        constantRandomRightSpikesInt = Random.Range(0 + GetLevelUp(), RightSpikes.Length - 5 + GetLevelUp());
        MovingSpikes();
    }
}