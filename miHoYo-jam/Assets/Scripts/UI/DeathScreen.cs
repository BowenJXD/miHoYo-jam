using System;
using DG.Tweening;
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
        player.GetComponent<Rigidbody>()?.DOKill();
        enemy.GetComponent<Rigidbody>()?.DOKill();
        enemy.SetActive(false);
        new LoopTask()
        {
            interval = 3f, finishAction = () =>
            {
                enemy.transform.position = savePoint.transform.position;
                enemy.SetActive(true);
            }
        }.Start();
        player.SetActive(true);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}