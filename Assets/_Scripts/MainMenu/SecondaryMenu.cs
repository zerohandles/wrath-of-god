using System.Collections;
using UnityEngine;

public class SecondaryMenu : MonoBehaviour
{
    [Header("Menu Elements")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject secondaryMenu;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip scrollSound;
    
    Animator animator;

    void Start() => animator = GetComponent<Animator>();

    public void BackButton() => StartCoroutine(Back());

    // Play the scroll closing animation and display the main menu.
    private IEnumerator Back()
    {
        animator.SetTrigger("Close");
        PlaySFX();
        yield return new WaitForSeconds(1);
        mainMenu.SetActive(true);
        PlaySFX();
        secondaryMenu.SetActive(false);
    }

    public void PlaySFX() => audioSource.PlayOneShot(scrollSound);
}
