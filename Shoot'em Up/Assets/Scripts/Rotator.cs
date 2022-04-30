using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotacion;
    public float speed;

    void Update()
    {
        transform.Rotate(rotacion * (speed * Time.deltaTime));
    }
}
