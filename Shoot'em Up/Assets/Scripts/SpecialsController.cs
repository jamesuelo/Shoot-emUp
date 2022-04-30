using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecialsController : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject shield;
    [SerializeField] private UnityEvent actionsOnFinish;

    private Coroutine laserCoroutine;
    private Coroutine shieldCoroutine;

    public void UnlockSpecial(PickUpConfig config)
    {
        switch (config.type)
        {
            case PickUpType.Laser:
                if (laserCoroutine != null)
                {
                    StopCoroutine(laserCoroutine);
                }
                laser.SetActive(true);
                laserCoroutine = StartCoroutine(DisableAfterSeconds(laser, config.durationInSeconds));
                break;
            case PickUpType.Shield:
                if (shieldCoroutine != null)
                {
                    StopCoroutine(shieldCoroutine);
                }
                shield.SetActive(true);
                shieldCoroutine = StartCoroutine(DisableAfterSeconds(shield, config.durationInSeconds, () => {
                    if (actionsOnFinish != null)
                    {
                        actionsOnFinish.Invoke();
                    }
                }));
                break;
            default:
                break;
        }
    }

    private IEnumerator DisableAfterSeconds(GameObject objectToDisable, float time, System.Action onFinish = null)
    {
        yield return new WaitForSeconds(time);
        objectToDisable.SetActive(false);

        if (onFinish != null)
        {
            onFinish.Invoke();
        }
    }
}
