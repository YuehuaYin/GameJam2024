using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    public int money = 5;

    public Tilemap tm;

    private Dictionary<Vector2Int, Tile> tileCoords;

    private Dictionary<Vector2Int, bool> isOccupied = new Dictionary<Vector2Int, bool>();

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
        buildXY(2,2);
        //tileCoords.Add(new Vector2Int(-1,-2),empty);  

        //Building stuff to start with

        buildInitialTile(new Vector2Int(0,0));
        buildInitialTile(new Vector2Int(0,-1));
    }
    public void level2Start(){
        buildXY(3,3);

    }
    public void level3Start(){
        buildXY(4,4);

    }
    public void level4Start(){
        buildXY(5,5);

    }
    public void level5Start(){
        buildXY(6,6);

    }
    public void level6Start(){
        buildXY(7,7);

    }

    public void buildXY(int x, int y){
        
        
        
        for (int i = 0; i < x ; i++){
            for (int j = 0; j < y ; j++){
                tm.SetTile(new Vector3Int(-i,-j),unbuilt);
            }
        }
        for (int i = 0; i < x ; i++){
            for (int j = 0; j < y ; j++){
                if (i == 0 && j ==0) {
                    tileCoords.Add(new Vector2Int(-i,-j),corner);
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

    }

    //Replace the values to alter the prices of tiles for each level
    private int[] prices = {1,2,3,4,5,6};

    private int getPrice() {
        return prices[GameManager.level-1];
    }

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
        if (isOccupied.TryGetValue(pos, out bool isItOccupoed)){

        


        if(money > 0 && !isItOccupoed){
            money = money -1;
            
            tm.SetTile(convVector(pos),tileCoords[pos]);
            isOccupied[(pos)] = true;
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
        i = 1;
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


        
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int gridPos = new Vector2Int(tm.WorldToCell(clickPos).x,tm.WorldToCell(clickPos).y);
        
        //Once we have money set up, change this to: buyTile(gridPos) 
        buildTile(gridPos);
    }
}
