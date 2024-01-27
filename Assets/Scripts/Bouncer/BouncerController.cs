using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BouncerController : MonoBehaviour
{
    [SerializeField] GameObject idPrefab;

    // Spawn Time (seconds), name, age, nationality, whether the ID should be accepted.
    [SerializeField] private List<(float, string, string, string, bool)> level3 = new List<(float, string, string, string, bool)>();
    [SerializeField] private List<(float, string, string, string, bool)> level4 = new List<(float, string, string, string, bool)>();
    [SerializeField] private List<(float, string, string, string, bool)> level5 = new List<(float, string, string, string, bool)>();
    [SerializeField] private List<(float, string, string, string, bool)> level6 = new List<(float, string, string, string, bool)>();
    [SerializeField] private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        // setupLevel1();
        // setupLevel2();
        setupLevel3();
        setupLevel4();
        setupLevel5();
        setupLevel6();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        currentTime += Time.deltaTime;

        switch(GameManager.level) {
            case 1:
                break;
            case 2:
                break;
            case 3:
                tickLevel(level3);
                break;
            case 4:
                tickLevel(level4);
                break;
            case 5:
                tickLevel(level5);
                break;
            case 6:
                tickLevel(level6);
                break;
        }
    }

    public void tickLevel(List<(float, string, string, string, bool)> levelList){
        for(int i = levelList.Count-1; i >= 0 ; i--)
        {
            var (time, name, age, nationality, acceptable) = levelList[i];
            if(time <= currentTime){
                Debug.Log("Trying to spawn an ID");
                createID(name, age, nationality, acceptable);
                levelList.RemoveAt(i);
            }
        }
    }

    private void createID(string name, string age, string nationality, bool acceptable){
        var go = Instantiate(idPrefab, GetComponent<Transform>().position, Quaternion.identity);
        ID newID = go.GetComponent<ID>();
        newID.name = name;
        newID.age = age;
        newID.nationality = nationality;
        newID.acceptable = acceptable;
        newID.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-50, 50);
        Debug.Log("Spawned new ID");
    }

    // private void setupLevel1() {}

    // private void setupLevel2() {}

    private void setupLevel3() {
        level3 = new List<(float, string, string, string, bool)>() {
            // Spawn time,  Name,       Age,    Nationality,    Acceptable?
            (5f,            "Barry",    "63",   "English",      true),
        };
    }

    private void setupLevel4() {
        level4 = new List<(float, string, string, string, bool)>() {
            // Spawn time,  Name,       Age,    Nationality,    Acceptable?
            (5f,            "Barry",    "63",   "English",      true),
        };
    }

    private void setupLevel5() {
        level5 = new List<(float, string, string, string, bool)>() {
            // Spawn time,  Name,       Age,    Nationality,    Acceptable?
            (5f,            "Barry",    "63",   "English",      true),
        };
    }

    private void setupLevel6() {
        level6 = new List<(float, string, string, string, bool)>() {
            // Spawn time,  Name,       Age,    Nationality,    Acceptable?
            (5f,            "Barry",    "63",   "English",      true),
        };
    }
}
