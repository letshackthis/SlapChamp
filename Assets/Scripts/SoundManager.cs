using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SoundManager : Singleton<SoundManager>
{
    public static Action OnSoundCheck;
    private AudioSource aSource;
    public AudioClip[] slap;
    public AudioClip[] wow;
    public AudioClip win;
    public AudioClip[] fail;
    [SerializeField] private AudioSource city;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource fire;
    [SerializeField] private RainController rainController;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        CheckSounds();
        OnSoundCheck += CheckSounds;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        OnSoundCheck -= CheckSounds;
    }

    private void CheckSounds()
    {
        bool sound = ES3.Load("sound", true);
        bool musicStatus = ES3.Load("music", true);

        if (SceneManager.GetActiveScene().name == "CharacterHouse")
        {
            if (sound)
            {
                fire.Play();
                rainController.StartTimer();
            }
            else
            {
                fire.Stop();
                rainController.Stop();
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (sound)
            {
                city.Play();
            }
            else
            {
                city.Stop();
            }

            fire.Stop();
            rainController.Stop();
        }

        if (musicStatus)
        {
            if(!music.isPlaying)
                music.Play();
        }
        else
        {
            music.Stop();
        }
    }

    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        if (ES3.Load("sound", true) == false)
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