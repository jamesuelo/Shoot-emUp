using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpType
{
    None,
    Laser,
    Shield
}

[CreateAssetMenu(fileName = "�PickupConfig", menuName = "Player/Pickups", order = 1)]
public class PickUpConfig : ScriptableObject
{
    public PickUpType type;
    public int score;
    public float durationInSeconds;
}
