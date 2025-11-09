using System;
using Unity;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject savePoint;
    
    public void Restart()
    {
        player.transform.position = savePoint.transform.position;
        enemy.SetActive(false);
        player.SetActive(true);
    }
}