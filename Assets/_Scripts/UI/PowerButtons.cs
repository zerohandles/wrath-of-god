using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerButtons : MonoBehaviour
{
    public GameObject ufo;
    public GameObject flood;
    public GameObject meteorStorm;
    private Player player;

    [SerializeField] private float tornadoTimer = 27;
    [SerializeField] private Button tornadoButton;
    [SerializeField] private float floodTimer = 39;
    [SerializeField] private Button floodButton;
    [SerializeField] private float ufoTimer = 60;
    [SerializeField] private Button ufoButton;
    [SerializeField] private float meteorStormTimer = 20;
    [SerializeField] private Button meteorStormButton;

    // Disable all power buttons at the start of the level
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(DisablePower(tornadoButton, tornadoTimer));
        StartCoroutine(DisablePower(floodButton, floodTimer));
        StartCoroutine(DisablePower(ufoButton, ufoTimer));
        StartCoroutine(DisablePower(meteorStormButton, meteorStormTimer));
    }

    public void ActivateTornado()
    {
        player.Tornado();
        StartCoroutine(DisablePower(tornadoButton, tornadoTimer));
    }

    public void ActivateFlood()
    {
        flood.SetActive(true);
        StartCoroutine(DisablePower(floodButton, floodTimer));
    }

    public void ActivateUFO()
    {
        ufo.SetActive(true);
        StartCoroutine(DisablePower(ufoButton, ufoTimer));
    }

    public void ActivateMeteorStorm()
    {
        // Ability to toggle off the meteors, was used in the secret level originally.
        // May be implemented again in the future
        if (meteorStorm.activeInHierarchy)
        {
            meteorStorm.SetActive(false);
            return;
        }

        meteorStorm.SetActive(true);
        StartCoroutine(DisablePower(meteorStormButton, meteorStormTimer));
    }

    // Disables the power button for the provided amount of time
    IEnumerator DisablePower(Button button, float timer)
    {
        button.interactable = false;
        yield return new WaitForSeconds(timer);
        button.interactable = true;
    }
}
