using System;
using Unity;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public DeathScreen deathScreen;

    private void Start()
    {
        if (deathScreen == null)
        {
            deathScreen = FindFirstObjectByType<DeathScreen>(FindObjectsInactive.Include);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (deathScreen != null)
            {
                deathScreen.savePoint = gameObject;
            }
        }
    }
}