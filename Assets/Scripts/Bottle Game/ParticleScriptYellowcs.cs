using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScriptYellow : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.name.Equals("3"))
        {
            other.GetComponent<Customer>().fill();
        }
    }
}
