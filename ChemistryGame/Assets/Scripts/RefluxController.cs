using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RefluxController : MonoBehaviour
{
    [SerializeField] private ClampController clamp;
    [SerializeField] private CondenserController condenser;
    [SerializeField] private FlaskController flask;
    [SerializeField] private DialInteractable heatDial;
    [SerializeField] private ParticleSystem boilingEffect;
    [SerializeField] private AudioSource successSFX;
    [SerializeField] private AudioSource boilingSFX;
    private bool refluxBegun;

    private void Update()
    {
        HandleReflux();
    }

    private void HandleReflux()
    {
        if (!refluxBegun && ReactionIsReady())
        {
            refluxBegun = true;
            StartCoroutine(Reflux());
        }
        else if (refluxBegun && !ReactionIsReady())
        {
            boilingEffect.gameObject.SetActive(false);
            boilingSFX.Pause();
        }
    }

    private bool ReactionIsReady()
    {
        return clamp.IsReady() &&
               flask.IsReady() &&
               condenser.IsReady() &&
               heatDial.Value <= 0.5 &&
               heatDial.Value >= 0.2;
    }

    IEnumerator Reflux()
    {
        yield return new WaitForSeconds(1.5f);
        successSFX.Play();
        boilingSFX.Play();
        boilingEffect.gameObject.SetActive(true);
    }
}
