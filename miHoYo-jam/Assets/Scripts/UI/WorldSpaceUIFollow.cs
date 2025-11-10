using System;
using Unity;
using UnityEngine;

using UnityEngine;

public class WorldSpaceUIFollow : MonoBehaviour
{
    public Transform target;           // The world object to follow
    public Vector3 worldOffset = new Vector3(0, 2f, 0); // offset above head

    Camera cam;
    RectTransform rectTransform;

    void Awake()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if (target == null || cam == null) return;

        // World position to screen position
        Vector3 worldPos = target.position + worldOffset;
        Vector3 screenPos = cam.WorldToScreenPoint(worldPos);

        // If behind camera, hide (optional)
        if (screenPos.z < 0)
        {
            rectTransform.gameObject.SetActive(false);
            return;
        }
        else if (!rectTransform.gameObject.activeSelf)
        {
            rectTransform.gameObject.SetActive(true);
        }

        rectTransform.position = screenPos;
    }
}
