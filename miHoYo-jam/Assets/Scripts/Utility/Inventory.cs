using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public List<String> items = new List<String>();
    public List<GameObject> itemUIs = new List<GameObject>();

    private void Start()
    {
        // SceneManager.sceneLoaded += OnSceneLoaded;
        Invoke(nameof(SetUpItemUIs), 0.1f);
        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SetUpItemUIs();
    }
    
    void SetUpItemUIs()
    {
        itemUIs.Clear();
        for (var i = 0; i < items.Count; i++)
        {
            var itemUI = GameObject.Find(items[i]);
            if (itemUI)
            {
                itemUIs.Add(itemUI);
                itemUI.SetActive(true);
            }
        }
    }

    public void AddItem(string item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
        }
        if (itemUIs.Find(i => i && i.name == item))
        {
            itemUIs.Find(i => i && i.name == item).SetActive(true);
        }
    }
    
    public void RemoveItem(string item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
        if (itemUIs.Find(i => i.name == item))
        {
            itemUIs.Find(i => i.name == item).SetActive(false);
        }
    }
    
    
}