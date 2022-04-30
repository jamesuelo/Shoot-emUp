using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public GameObject prefab;

    public void DoInstantiate()
    {
        DoInstantiate(transform.position);
    }

    public void DoInstantiate(Vector3 pos)
    {
        Instantiate(prefab, pos, transform.rotation);
    }
}
