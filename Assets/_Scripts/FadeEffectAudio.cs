using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffectAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public IEnumerator FadeAudio(float targetVolume, float duration)
    {
        float time = 0;
        float startVolume = audioSource.volume;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = targetVolume;
    }
}
