using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTimer : MonoBehaviour
{
    public SpriteRenderer timer;
    public Transform transform;

    public void updateTimer(float current, float max)
    {
        float percentage = 1 - (current / max);
        transform.localScale = new Vector3(transform.localScale.x, percentage * 2);
        timer.color = new Color32(255 , (byte)(int) (percentage * 255) ,  (byte)(int) (percentage * 255), 153);
    }
}
