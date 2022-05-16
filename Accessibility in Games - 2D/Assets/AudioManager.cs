using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System; //for Array

public class AudioManager : MonoBehaviour
{
    public Sound[] soundArray;

    // Start is called before the first frame update

    private void Awake()
    {
        foreach(Sound sound in soundArray)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;

            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;

            sound.audioSource.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound soundVariable = Array.Find(soundArray, soundArray => soundArray.name == name);
        if (soundVariable == null)
        {
            Debug.Log($"Sound: {name} not found!");
            return; //don't play an audio if it's not there
        }
            

        soundVariable.audioSource.Play();
    }

    void Start()
    {
        Play("Background Theme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
