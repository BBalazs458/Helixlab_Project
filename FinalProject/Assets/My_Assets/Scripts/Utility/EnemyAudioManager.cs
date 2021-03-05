using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public interface IEnemyAudioManager
{
    void PlayAudio(AudioClip clip, bool loop);

    void StopAudio(AudioClip clip);



}//class
