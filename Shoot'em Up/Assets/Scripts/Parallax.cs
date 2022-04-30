using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed = 0.5f;
    [SerializeField] private MeshRenderer mesh;

    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * speed);
        mesh.material.mainTextureOffset = offset;
    }
}
