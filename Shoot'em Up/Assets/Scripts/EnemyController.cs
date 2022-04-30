using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyConfig config;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Instantiators multipleInstantiator;
    private Move mover;
    public bool IsBoss;
    private Shooter[] shooters;
    private HealthController healthController;

    private void Start()
    {
        mover = GetComponent<Move>();
        if (mover != null)
        {
            mover.speed = config.moverSpeed;
        }

        if (config.sprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = config.sprite;
        }

        shooters = GetComponentsInChildren<Shooter>();
        if (shooters != null && shooters.Length > 0)
        {
            foreach (var shooter in shooters)
            {
                StartCoroutine(ShootForever(shooter));
            }
        }
        healthController = GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.health = config.health;
        }
    }

    public void OnDie()
    {
       if (config != null && multipleInstantiator != null && config.ShouldThrowPickup())
        {
            if (multipleInstantiator.InstantiatorsCount > 1)
            {
                for (int i = 0; i < multipleInstantiator.InstantiatorsCount; i++)
                {
                    if (Dice.IsChanceSuccess(config.pickupChance))
                    {
                        multipleInstantiator.InstantiateByIndex(i);
                    }
                }
            }
            else
            {
                multipleInstantiator.InstantiateInSequence();
            }
        }

        StopAllCoroutines();
        GameController.Instance.OnDie(gameObject, config.score);
    }
    public void DestroyAllShootedShoots()
    {
        if (shooters != null && shooters.Length > 0)
        {
            foreach (var shooter in shooters)
            {
                shooter.DestroyAllShootedShoots();
            }
        }
    }

    private IEnumerator ShootForever(Shooter shooter)
    {
        if (shooter == null || shooter.ShootingConfig == null)
        {
            yield break;
        }
        yield return new WaitForSeconds(shooter.ShootingConfig.shootInitialWaitTime);
        while (true)
        {
            if (shooter == null || shooter.ShootingConfig == null)
            {
                yield break;
            }
            shooter.DoShoot();
            //StatisticsManager.SetStatisticValue(StatisticId.EnemyShoots, 1, true);
            yield return new WaitForSeconds(shooter.ShootingConfig.shootCadence);
        }//
    }
}
