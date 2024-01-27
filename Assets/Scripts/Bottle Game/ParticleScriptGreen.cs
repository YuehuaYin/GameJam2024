using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScriptGreen : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.name.Equals("1"))
        {
            other.GetComponent<Customer>().fill();
        }
    }
}
