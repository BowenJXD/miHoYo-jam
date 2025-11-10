using System;
using Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhiteDiamond : MonoBehaviour
{
    public Inventory inventory;

    private void Start()
    {
        if (inventory == null)
        {
            inventory = FindAnyObjectByType<Inventory>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameObject go = FindInactiveByName("WhiteDiamond");
        if (go != null)
        {
            go.SetActive(true);
        }
        SceneManager.LoadScene("Format1");
    }
    
    GameObject FindInactiveByName(string name)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
                return obj;
        }
        return null;
    }
}