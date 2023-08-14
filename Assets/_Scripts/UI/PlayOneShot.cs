using UnityEngine;


// Play Menu UI sound effects
public class PlayOneShot : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip pressedSound;


    public void PlayHoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void PlayPressedSound()
    {
        audioSource.PlayOneShot(pressedSound);
    }
}
