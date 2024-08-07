using UnityEngine;

// Play Menu UI sound effects
public class PlayOneShot : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip pressedSound;

    public void PlayHoverSound() => audioSource.PlayOneShot(hoverSound);

    public void PlayPressedSound() => audioSource.PlayOneShot(pressedSound);
}
