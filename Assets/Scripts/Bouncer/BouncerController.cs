using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BouncerController : MonoBehaviour
{
    [SerializeField] GameObject idPrefab;
    [SerializeField] TMP_Text money;
    [SerializeField] TMP_Text rules;

    // Spawn Time (seconds), name, age, nationality, favourite food, whether the ID should be accepted.
    // Spawn time is seconds from game start.
    [SerializeField] private List<(float, string, string, string, string, bool)> level3 = new List<(float, string, string, string, string, bool)>();
    [SerializeField] private List<(float, string, string, string, string, bool)> level4 = new List<(float, string, string, string, string, bool)>();
    [SerializeField] private List<(float, string, string, string, string, bool)> level5 = new List<(float, string, string, string, string, bool)>();
    [SerializeField] private List<(float, string, string, string, string, bool)> level6 = new List<(float, string, string, string, string, bool)>();
    [SerializeField] private float currentTime = 0;

    public static int moneyAcceptCorrect = 10;
    public static int moneyAcceptIncorrect = -10;
    public static int moneyDenyCorrect = 5;
    public static int moneyDenyIncorrect = 0;

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
        money.SetText("£" + Convert.ToString(GameManager.money));
    }

    void FixedUpdate() {
        currentTime += Time.deltaTime;

        switch(GameManager.level) {
            case 1:
                break;
            case 2:
                break;
            case 3:
                rules.SetText("No under 18s.\nNo Frenchmen");
                tickLevel(level3);
                break;
            case 4:
                rules.SetText("No Humans under 18\nNo Space-Elves over 197\nNo Daleks. End of.");
                tickLevel(level4);
                break;
            case 5:
                rules.SetText("No square-number ages\nNo Greek gods except Hades");
                tickLevel(level5);
                break;
            case 6:
                rules.SetText("No even-lettered names\n");
                tickLevel(level6);
                break;
        }
    }

    public void tickLevel(List<(float, string, string, string, string, bool)> levelList){
        for(int i = levelList.Count-1; i >= 0 ; i--)
        {
            var (time, name, age, nationality, food, acceptable) = levelList[i];
            if(time <= currentTime){
                Debug.Log("Trying to spawn an ID");
                createID(name, age, nationality, food, acceptable);
                levelList.RemoveAt(i);
            }
        }
    }

    private void createID(string name, string age, string nationality, string food, bool acceptable){
        var go = Instantiate(idPrefab, GetComponent<Transform>().position, Quaternion.identity);
        ID newID = go.GetComponent<ID>();
        newID.name = name;
        newID.age = age;
        newID.nationality = nationality;
        newID.food = food;
        newID.acceptable = acceptable;
        newID.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-50, 50);
        Debug.Log("Spawned new ID with values: " + name + " " + age + " " + nationality + " " + food);
    }

    // private void setupLevel1() {}

    // private void setupLevel2() {}

    // Arena
    private void setupLevel3() {
        level3 = new List<(float, string, string, string, string, bool)>() {
            // Spawn time,  Name,       Age,    Nationality,    Food        Acceptable?
            (2f,             "Barry",    "63",   "England",      "Greggs",   true),
            (12f,            "Anatoli Smorin",    "39",   "Italy",      "Spaghetti",   true),
            (25f,            "Senor Baguette",    "39",   "France",      "Spaghetti",   false),
            (45f,            "Marl Karx",         "99",   "Russia",     "Asbest-O's",   true),
            (52f,            "Biggus Dickus",    "56",   "Wome",      "Wump Theak",   true),
        };
    }

    // Space
    private void setupLevel4() {
        level4 = new List<(float, string, string, string, string, bool)>() {
            // Spawn time,  Name,       Age,    Nationality,    Food        Acceptable?
            (5f,            "Blorbus",    "63",   "Englund",      "Phlorpo",   true),
            (16f,            "Phlorpo",    "112",   "Dalekia",      "EXTERMINATION",   false),
            (20f,            "Space-Barry",    "63",   "Space-England",      "Space-Greggs",   true),
            (32f,            "Zark Muckerberg",    "17",   "Metaverse",      "Money",   false),
            (53f,            "Nyan Cat",    "nyanyanyanyanya",   "nyanyanyanyanya",      "nyanyanyanyanya",   false),
        };
    }

    // Cthulu
    private void setupLevel5() {
        level5 = new List<(float, string, string, string, string, bool)>() {
            // Spawn time,  Name,       Age,    Nationality,    Food        Acceptable?
            (5f,            "Blorbus",    "163",   "Galactica",      "Pinkish Goo",   true),
            (16f,            "Phlorpo",    "112",   "Dalekia",      "EXTERMINATION",   true),
            (20f,            "Space-Barry",    "64",   "Space-England",      "Space-Greggs",   false),
            (30f,            "Zeus",    "2789",   "Mt. Olympus",      "Ambrosia",   false),
            (50f,            "Hades",    "2789",   "Underworld",      "Death",   false),
        };
    }

    // Ur mum (heh)
    private void setupLevel6() {
        level6 = new List<(float, string, string, string, string, bool)>() {
            // Spawn time,  Name,       Age,    Nationality,    Food        Acceptable?
            (5f,             "Dicken Sider",    "69",   "Scunthorpe",      "Greggs",   true),
            (16f,            "Ben Dover",    "69",   "Boring",      "Spotted Dick",   false),
            (21f,            "Mike Cork",    "69",   "Fucking (Austria)",      "Sausage",   false),
            (37f,            "Phil McKraken",    "69",   "France",      "Toad in your hole",   false),
            (43f,            "Jenna Tolls",    "69",   "Bell End",      "Big Meatballs",   false),
            (55f,            "Gabe Itche",    "69",   "Penistone",      "Clams",   true),
        };
    }
}
