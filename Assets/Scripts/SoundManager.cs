using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource aSource;
    public AudioClip[] slap;
    public AudioClip[] wow;
    public AudioClip win;
    public AudioClip[] fail;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        aSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            PlaySound(name);
        }
    }

    public void PlaySound(string name)
    {
        if (PlayerPrefs.GetInt("sound", 1) == 0)
        {
            return;
        }

        switch (name)
        {
            case "slap":
                aSource.PlayOneShot(slap[Random.Range(0, slap.Length)]);
                break;
            case "wow":
                aSource.PlayOneShot(wow[Random.Range(0, wow.Length)]);
                break;
            case "win":
                aSource.PlayOneShot(win);
                break;
            case "fail":
                aSource.PlayOneShot(fail[Random.Range(0, fail.Length)]);
                break;
        }
    }
}