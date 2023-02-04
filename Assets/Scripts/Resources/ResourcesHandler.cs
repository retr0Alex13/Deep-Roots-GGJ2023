using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHandler : MonoBehaviour
{
    [SerializeField] private int sunEnergyTick = 5;

    private void OnParticleCollision(GameObject other)
    {
        GameManager.Instance.AddResources(sunEnergyTick, 0);
    }
}
