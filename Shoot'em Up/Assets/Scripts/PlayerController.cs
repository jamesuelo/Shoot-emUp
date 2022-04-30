using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Limite
{
    public float xMin, xMax, yMin, yMax;
}
public class PlayerController : MonoBehaviour
{
    public Move moveComponent;
    public Limite limite;
    public float speed;
    [SerializeField] private List<Shooter> shooter;
    [SerializeField] private PlayerConfig config;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private OnTriggerEnterDo triggerer;
    [SerializeField] private SpecialsController specialsController;
    private int powerLevel;
    private int unlockedCannons = 1;

    public delegate void PowerChanged(int currentPower, int totalPower);
    public event PowerChanged OnPowerChanged;
    void Start()
    {
        moveComponent.speed = speed;
        InputProvider.OnHasShoot += OnHasShoot;
        InputProvider.OnDirection += OnDirection;
    }

    private void OnHasShoot()
    {
        for (int i = 0; i < unlockedCannons; i++)
        {
            var s = shooter[i];
            s.DoShoot();
            //StatisticsManager.SetStatisticValue(StatisticId.PlayerShoots, 1, true);
        }
    }

    private void OnDirection(Vector3 direction)
    {
        moveComponent.direction = direction;
    }
    public void AddToPowerLevel(int powerToAdd)
    {
        powerLevel += powerToAdd;
        var powerConfig = config.GetPowerConfig(powerLevel);
        unlockedCannons = powerConfig.cannonAmount;
 
        if (OnPowerChanged != null)
        {
            OnPowerChanged.Invoke(powerLevel, config.GetMaxPowerValue());
        } 
    }

    public void OnPlayerDie()
    {
        //GameController.Instance.OnPlayerDie();
    }

    public void UnlockSpecial(PickUpConfig pickupConfig)
    {
        specialsController.UnlockSpecial(pickupConfig);
        if (pickupConfig.type == PickUpType.Shield)
        {
            EnableCollider(false);
        }
    }
    public void EnableCollider(bool shouldEnable)
    {
        playerCollider.enabled = shouldEnable;
        triggerer.IsEnabled = shouldEnable;
    }

    public int GetCurrentPowerLevel()
    {
        return powerLevel;
    }
    public int GetMaxPowerLevel()
    {
        return config.GetMaxPowerValue();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Clamp(transform.position.x, limite.xMin, limite.xMax);
        float y = Mathf.Clamp(transform.position.y, limite.yMin, limite.yMax);
        transform.position = new Vector3(x, y);
    }
}
