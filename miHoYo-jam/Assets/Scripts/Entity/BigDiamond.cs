using System;
using System.Linq;
using DG.Tweening;
using Sirenix.Utilities;
using Unity;
using UnityEngine;

public class BigDiamond : MonoBehaviour
{
    public GameObject[] conditions;
    public GameObject victoryScreen;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (conditions.All(c => c.activeSelf))
        {
            Time.timeScale = 0;
            victoryScreen.SetActive(true);
        }
        else
        {
            transform.DOShakePosition(0.5f, 0.3f, 20, 90, false, true);
        }
    }
}