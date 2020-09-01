using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arcade : MonoBehaviour {
    public Image tapToPlayimage;
    private float currentTimeScale = 0, addingGameVelocity = 0.05f;

    [Header("Score attributes")]
    private Func<bool> scorePredicate;
    private int scoreGoalToChangeBallSprite = 5;
    private int score = 0;

    [Header("Background attributes")]
    public Image background;
    private Color[] backgroundColorList;
    private int backgroundColorListIndex;

    [Header("Ball attributes")]
    private Sprite[] collectionBallArray;
    private BallScript ball;
    private int collectionBallListIndex = 0;

    [Header("Particles attributes")]
    private Gradient[] colorParticlesList;
    private ParticleSystem ballParticleSystem;
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule;
    private int colorParticlesListIndex = 0;
    private int particlesQuantity = 0;

    [Header("Spikes attributes")]
    private int spikesSpawnAdding;
    private List<SpikeScript> spikeList = new List<SpikeScript>();
    private Color[] spikesColorList;
    private int spikeColorListIndex = 0;

    [Header("SeraizableObjects attributes")]
    public ColorListPreset colorListPreset;
    public CollectionBallsPreset collectionBallsPreset;

    private void Awake(){
        ball = GetComponentInChildren<BallScript>();
        ballParticleSystem = ball.GetComponentInChildren<ParticleSystem>();
        emissionModule = ballParticleSystem.emission;
        colorOverLifetimeModule = ballParticleSystem.colorOverLifetime;
    }
    private IEnumerator Start(){
        backgroundColorList = colorListPreset.backGroundColorList;
        spikesColorList = colorListPreset.spikeColorList;
        colorParticlesList = colorListPreset.particlesColorList;
        collectionBallArray = collectionBallsPreset.collectionBallsPreset;

        scorePredicate = (() => score % scoreGoalToChangeBallSprite == 0);
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        foreach (SpikeScript spike in GetComponentsInChildren<SpikeScript>())
            spikeList.Add(spike);
        yield return new WaitUntil(() => tapToPlayimage.gameObject.activeInHierarchy == false);
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Time.timeScale = 1.15f;
        spikesSpawnAdding = 1;
    }

    private void ChangeBallSprite() {
        scoreGoalToChangeBallSprite = 10;
        if (scorePredicate()){
            if (collectionBallListIndex != collectionBallArray.Length -1){
                ball.GetComponent<Image>().sprite = collectionBallArray[collectionBallListIndex];
                collectionBallListIndex++;
    }}}

    private void ChangeBallParticles() {
        scoreGoalToChangeBallSprite = 10;
        if (scorePredicate()){
            if (colorParticlesListIndex != colorParticlesList.Length -1){
                colorOverLifetimeModule.color = colorParticlesList[colorParticlesListIndex];
                colorParticlesListIndex++;
            }

            if (particlesQuantity < 100) { 
                particlesQuantity += 10;
                emissionModule.rateOverTime = particlesQuantity;
    }}}

    private void ChangeBackgroundColor(){
        scoreGoalToChangeBallSprite = 5;
        if (scorePredicate()) {
            if (backgroundColorListIndex != backgroundColorList.Length -1){
                background.color = backgroundColorList[backgroundColorListIndex];
                backgroundColorListIndex++;
            } else 
                backgroundColorListIndex = 0;
    }}

    private void ChangeSpikesColor() {
        scoreGoalToChangeBallSprite = 5;
        if (scorePredicate()){
            if (spikeColorListIndex != spikesColorList.Length -1){
                for (int i = 0; i < spikeList.Count; i++) 
                    spikeList[i].GetComponent<Image>().color = spikesColorList[spikeColorListIndex];
                    spikeColorListIndex++;
            } else 
                spikeColorListIndex = 0;
    }}

    private void ChangeGameTime() {
        scoreGoalToChangeBallSprite = 10;
        if (scorePredicate()){
            if (currentTimeScale <= 1f) {
                currentTimeScale = Time.timeScale + addingGameVelocity;
                Time.timeScale = currentTimeScale;
    }}}
}
