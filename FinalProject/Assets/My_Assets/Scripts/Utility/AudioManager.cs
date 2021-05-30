using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public List<AudioClip> audioClips;

    private AudioSource audioSource;

    private float timer;

    void Start()
    {
        //DontDestroyOnLoad(this);
        if (audioClips.Count == 0)
        {
            Debug.LogError("No playable music in the list!!");
        }
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = audioClips[0];
        audioSource.Play();
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= audioSource.clip.length)
        {
            timer = 0;
            audioSource.Stop();
            audioSource.clip = null;
            int randomClip = Random.Range(0, audioClips.Count);
            audioSource.clip = audioClips[randomClip];
            audioSource.Play();
        }

        //StartCoroutine(SoundLenght(audioSource.clip.length));
    }


    IEnumerator SoundLenght(float lenght)
    {
        Debug.Log(lenght);
        yield return new WaitForSeconds(lenght);
        audioSource.Stop();
        audioSource.clip = null;
        int randomClip = Random.Range(0, audioClips.Count);
        audioSource.clip = audioClips[randomClip];
        audioSource.Play();
    }

}
