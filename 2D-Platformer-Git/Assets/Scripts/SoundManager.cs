using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip JumpSound,CoinSound,LandSound,LaserSound,EnemyDeathSound;

    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        CharacterController2D.JumpEvent += PlayJumpSound;
        CollectItems.coinHit += PlayCoinSound;
    }
    void PlayJumpSound()
    {
        audioSource.PlayOneShot(JumpSound);
    }
   public void PlayLandSound()
    {
        audioSource.PlayOneShot(LandSound);
    }
    public void PlayEnemyDeathSound()
    {
        audioSource.PlayOneShot(EnemyDeathSound);
    }
    public void PlayCoinSound()
    {
        audioSource.PlayOneShot(CoinSound);
    }
}
