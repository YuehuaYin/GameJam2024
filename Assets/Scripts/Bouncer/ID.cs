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

    [SerializeField] public bool acceptable;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text ageText;
    [SerializeField] private TMP_Text nationalityText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.SetText(name);
        ageText.SetText(age);
        nationalityText.SetText(nationality);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {

    }

    // Called when dragged into Accept box
    public void Accept() {
        if (acceptable) {
            Debug.Log("Correct!");
        } else {
            Debug.Log("Incorrect!");
        }
    }

    // Called when dragged into Deny box
    public void Deny() {
        if (acceptable) {
            Debug.Log("Incorrect!");
        } else {
            Debug.Log("Correct!");
        }
    }
}
