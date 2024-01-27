using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class jokes : MonoBehaviour
{
    ComedyMinigame controller;
    string emojiName;

    void Start()
    {
        controller = GameObject.Find("comedyController").GetComponent<ComedyMinigame>();
    }

    public void setEmoji(string emoji)
    {
        emojiName = emoji;
        this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(emoji);
    }

    void OnMouseDown()
    {
        controller.jokeClicked(emojiName);
    }
}
