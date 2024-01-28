using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;

public class TileBuyer : MonoBehaviour
{
    public Tile stage;
    public Tile entrance;
    public Tile bar;
    public Tile toilets;
    public Tile empty;
    public Tile corner;
    public Tile leftWall;
    public Tile rightWall;
    public Tile unbuilt;
    public AudioSource audio;

    [SerializeField] TextMeshProUGUI buyNextText;


    //public int money = GameManager.money;

    public Tilemap tm;


    private Dictionary<Vector2Int, Tile> tileCoords;

    private Dictionary<Vector2Int, bool> isOccupied = new Dictionary<Vector2Int, bool>();

    private int getLevelCost() {
        return Mathf.RoundToInt((Mathf.Pow(1.5f,GameManager.level) * 100));
    }

    public void loadNextLevel() {
        if (GameManager.money > getLevelCost()) {
            GameManager.money -= getLevelCost();
            GameManager.newStage();
            
            SceneManager.LoadScene(("Level " + (GameManager.level)));
        }
    }

    //Add more of these to set up the initial state for each level
    private void level1Start() {
        //How the level starts
        // tm.SetTile(new Vector3Int(0,0),unbuilt);
        // tm.SetTile(new Vector3Int(0,-1),unbuilt);
        // tm.SetTile(new Vector3Int(-1,0),unbuilt);
        // tm.SetTile(new Vector3Int(-1,-1),unbuilt);
        // //tm.SetTile(new Vector3Int(-1,-2),unbuilt);

        // //The things you can buy
        // tileCoords.Add(new Vector2Int(0,0),corner);
        // tileCoords.Add(new Vector2Int(0,-1),toilets);
        // tileCoords.Add(new Vector2Int(-1,0),leftWall);
        // tileCoords.Add(new Vector2Int(-1,-1),empty);     
        buildXY(2,2,1);
        //tileCoords.Add(new Vector2Int(-1,-2),empty);  

        //Building stuff to start with

        
    }
    public void level2Start(){
        buildXY(3,3,2);

    }
    public void level3Start(){
        buildXY(4,4,3);

    }
    public void level4Start(){
        buildXY(5,5,4);

    }
    public void level5Start(){
        buildXY(6,6,5);

    }
    public void level6Start(){
        buildXY(7,7,6);

    }

    public void buildXY(int x, int y, int currentStage){
        
        
        //Set up initial unbuilt
        for (int i = 0; i < x ; i++){
            for (int j = 0; j < y ; j++){
                tm.SetTile(new Vector3Int(-i,-j),unbuilt);
            }
        }

        //Fill tileCoords with empty tiles (and walls)
        for (int i = 0; i < x ; i++){
            for (int j = 0; j < y ; j++){
                if (i == 0 && j ==0) {
                    tileCoords.Add(new Vector2Int(-i,-j),stage);
                }else if (j == 0 & i == (y-1)) {
                    tileCoords.Add(new Vector2Int(-i,-j),toilets);
                
                }else if (currentStage > 1 && i == (x- 1) && j == (y - 1)) {
                    tileCoords.Add(new Vector2Int(-i,-j),bar);
                
                }else if (currentStage > 2 && j == (x - 1) && i == 0 ){
                    tileCoords.Add(new Vector2Int(-i,-j),entrance);
                    Debug.Log("ha");

                }else if (i == 0){
                    tileCoords.Add(new Vector2Int(-i,-j),rightWall);
                }else if (j == 0){
                    tileCoords.Add(new Vector2Int(-i,-j),leftWall);
                } else{
                    tileCoords.Add(new Vector2Int(-i,-j),empty);
                } 
                isOccupied.Add(new Vector2Int(-i,-j),false);
            }
            
        }
        buildInitialTile(new Vector2Int(0,0));
        buildInitialTile(new Vector2Int(0,-1));
        isOccupied[(new Vector2Int(0,0))] = true;
        isOccupied[(new Vector2Int(0,-1))] = true;        

    }

    //Replace the values to alter the prices of tiles for each level
    private static int[] prices = {20,30,40,50,60,70};

    public static  int getPrice() {
        return prices[GameManager.level-1];
    }

    //Redundant
    public void buyTile(Vector2Int pos) {
        if(GameManager.money>=getPrice()) {
            GameManager.money = GameManager.money - getPrice();
        }
    }
    
    public void buildInitialTile(Vector2Int pos) {
        tm.SetTile(convVector(pos),tileCoords[pos]);
    }
    public void buildTile(Vector2Int pos) {
        Vector3Int v = new Vector3Int(pos.x,pos.y);
        if (isOccupied.TryGetValue(pos, out bool isItOccupied)){

        

        //change to getPrice when level system set up
        if(GameManager.money >= getPrice() && !isItOccupied){
            GameManager.money = GameManager.money - getPrice();
            
            tm.SetTile(convVector(pos),tileCoords[pos]);
                if (tileCoords[pos] == bar)
                {
                    GameManager.barUnlocked = true;
                }
                if (tileCoords[pos] == toilets)
                {
                    GameManager.toiletUnlocked = true;
                }
                if (tileCoords[pos] == entrance)
                {
                    GameManager.bouncerUnlocked = true;
                }
                isOccupied[(pos)] = true;
            GameManager.tilesOwned = GameManager.tilesOwned + 1;
            audio.Play();
        }
        }
    }


    private Vector3Int convVector(Vector2Int vector) {
        return (new Vector3Int(vector.x,vector.y));
    }

    // Start is called before the first frame update
    void Start()
    {
        tileCoords = new Dictionary<Vector2Int,Tile> {};
        int i = GameManager.level;
        
        switch (i){
        case 1:
        level1Start();
        break;
        case 2:
        level2Start();
        break;
        case 3:
        level3Start();
        break;
        case 4:
        level4Start();
        break;
        case 5:
        level5Start();
        break;
        case 6:
        level6Start();
        break;

        GameManager.tilesOwned = 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        buyNextText.text = ("Buy next level\nCost: " + getLevelCost());
    }

    void OnMouseDown() {
        Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int gridPos = new Vector2Int(tm.WorldToCell(clickPos).x,tm.WorldToCell(clickPos).y);
        
        //Once we have money set up, change this to: buyTile(gridPos) 
        buildTile(gridPos);
    }
}
