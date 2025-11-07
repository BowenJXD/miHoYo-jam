using System;
using Unity;
using UnityEngine;

public class ColorManager : Singleton<ColorManager>
{
    public SerializableDictionary<EColor, Color> colorMap;
    
    public Color GetColor(EColor eColor)
    {
        if (colorMap.TryGetValue(eColor, out Color color))
        {
            return color;
        }
        Debug.LogWarning($"Color for {eColor} not found. Returning white as default.");
        return Color.white;
    }
}