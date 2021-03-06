using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public List<AudioClip> audioClips;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if (audioClips.Count == 0)
        {
            Debug.LogError("No playable music in the list!!");
        }
    }


    void Update()
    {
        
    }
}
