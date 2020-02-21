using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    private Planet attractor;
    private Rigidbody rigidBody;

    public bool placeOnSurface = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        attractor = Planet.instance;
    }

    void FixedUpdate()
    {
        if (placeOnSurface)
            attractor.PlaceOnSurface(rigidBody);
        else
            attractor.Attract(rigidBody);
    }

}