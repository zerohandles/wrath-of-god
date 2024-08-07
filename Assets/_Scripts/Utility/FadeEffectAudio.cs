using System.Collections;
using UnityEngine;

public class FadeEffectAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    // Fade audio towards the target volume over the duration in seconds.
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
