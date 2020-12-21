using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    private bool isPlayingHubMusic = false;

    private bool isPlayingOverworld = false;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.outputAudioMixerGroup;
        }
    }

    void Start()
    {
       
    }

    private void Update()
    {
        CheckPlayHubScene();
        CheckPlayOverworld();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void StopAll()
    {
        foreach (var sound in sounds)
        {
            sound.source.Stop();
        }
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return false;
        }
        return s.source.isPlaying;
    }

    private void CheckPlayHubScene()
    {
        if (SceneManager.GetActiveScene().name == "Hub" && !isPlayingHubMusic)
        {
            StopAll();
            isPlayingHubMusic = true;
            isPlayingOverworld = false;
            Play("HubPartA");
        }
    }

    private void CheckPlayOverworld()
    {
        if (SceneManager.GetActiveScene().name == "FirstLevel" && !isPlayingOverworld)
        {
            StopAll();
            isPlayingOverworld = true;
            isPlayingHubMusic = false;
            Play("Overworld");
        }
    }
}
