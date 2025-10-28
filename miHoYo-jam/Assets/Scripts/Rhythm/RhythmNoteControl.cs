using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.UI;

public class RhythmNoteControl : MonoBehaviour
{
    public List<Image> noteImages;

    private void Start()
    {
        RhythmManager.Instance.OnBeat += OnReceiveBeat;
    }

    public void OnReceiveBeat(int beatIndex)
    {
        for (int i = 0; i < noteImages.Count; i++)
        {
            if (i <= beatIndex)
            {
                noteImages[i].color = Color.white;
            }
            else
            {
                noteImages[i].color = Color.clear;
            }
        }
    }
}