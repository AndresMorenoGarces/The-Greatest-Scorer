using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public static AudioScript instance;
    public AudioSource audioSourceMusic, audioSourceFX;
    public AudioClip ballSound, wallSound, loseSound;
    public AudioClip[] Songs;
    private int currentSongInt = 0;
    private bool loadFirstSong = false;

    public void BallSound()
    {
        PlayAndDestroy(ballSound);
    }
    public void WallHitSound()
    {
        PlayAndDestroy(wallSound);
    }
    public void LoseHitSound()
    {
        PlayAndDestroy(loseSound);
    }
    private void SongsChange()
    {
        if (audioSourceMusic.isPlaying == false)
        {
            loadFirstSong = !loadFirstSong;
            audioSourceMusic.clip = Songs[currentSongInt];
            audioSourceMusic.Play();
            if (currentSongInt < Songs.Length)
                currentSongInt++;
            else
                currentSongInt = 0;
        }
    }
    private void PlayAndDestroy(AudioClip _audioClip)
    {
        audioSourceFX.PlayOneShot(_audioClip);
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Update()
    {
        SongsChange();
    }
}