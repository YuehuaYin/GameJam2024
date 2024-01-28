using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class ID : MonoBehaviour
{
    [SerializeField] public string name;
    [SerializeField] public string age;
    [SerializeField] public string nationality;
    [SerializeField] public string food;

    [SerializeField] public bool acceptable;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text ageText;
    [SerializeField] private TMP_Text nationalityText;
    [SerializeField] private TMP_Text foodText;
    [SerializeField] private AudioSource correct;
    [SerializeField] private AudioSource incorrect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nameText.SetText(name);
        ageText.SetText(age);
        nationalityText.SetText(nationality);
        foodText.SetText(food);
    }

    void FixedUpdate() {

    }

    // Called when dragged into Accept box
    public void Accept() {
        if (acceptable) {
            Debug.Log("Correct!");
            correct.Play();
            AudioSource.PlayClipAtPoint(correct.clip, Vector3.zero);
            GameManager.winGame();
        } else {
            Debug.Log("Incorrect!");
            AudioSource.PlayClipAtPoint(incorrect.clip, Vector3.zero);
            GameManager.loseGame();
        }
        Destroy(gameObject);
    }

    // Called when dragged into Deny box
    public void Deny() {
        if (acceptable) {
            Debug.Log("Incorrect!");
                 AudioSource.PlayClipAtPoint(incorrect.clip, Vector3.zero);
            GameManager.money += BouncerController.moneyDenyIncorrect;
        } else {
            Debug.Log("Correct!");
            AudioSource.PlayClipAtPoint(correct.clip, Vector3.zero);
            GameManager.money += BouncerController.moneyDenyCorrect;
        }
        Destroy(gameObject);
    }
}
