using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScriptBlue : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.name.Equals("2"))
        {
            other.GetComponent<Customer>().Fill();
        }
    }
}
