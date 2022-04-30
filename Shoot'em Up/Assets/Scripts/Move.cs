using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public void Update()
    {
        transform.Translate(direction * speed*Time.deltaTime);
    }
}
