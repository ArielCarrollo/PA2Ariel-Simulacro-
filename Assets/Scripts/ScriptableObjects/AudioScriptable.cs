using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Audio/Audio Settings")]
public class AudioSettings : ScriptableObject
{
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
}
