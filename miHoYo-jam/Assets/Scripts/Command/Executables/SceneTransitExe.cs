using System;
using Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitExe : ExecutableBehaviour
{
    public string sceneName;
    
    protected override void OnStart()
    {
        base.OnStart();
        SceneManager.LoadScene(sceneName);
    }
}