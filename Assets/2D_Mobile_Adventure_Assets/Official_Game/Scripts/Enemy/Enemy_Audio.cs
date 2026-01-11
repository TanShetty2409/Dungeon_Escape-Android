using UnityEngine;

public class Enemy_Audio : MonoBehaviour
{
    public AudioClip footstep;
    public AudioClip attack;
    public AudioClip hit;
    public AudioClip death;

    private AudioSource _audio;
    private bool _deathPlayed = false;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _audio.playOnAwake = false;
    }

    public void PlayFootstep() => Play(footstep);
    public void PlayAttack()   => Play(attack);
    public void PlayHit()      => Play(hit);

    public void PlayDeath()
    {
        if (_deathPlayed) return;
        _deathPlayed = true;
        Play(death);
    }

    private void Play(AudioClip clip)
    {
        if (clip == null) return;  
        _audio.PlayOneShot(clip);
    }
}
