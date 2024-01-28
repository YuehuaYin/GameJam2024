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

    public Tilemap tm;

    private Dictionary<Vector2Int, Tile> tileCoords;

    //Add more of these to set up the initial state for each level
    private void level1Start() {
        //How the level starts
        tm.SetTile(new Vector3Int(0,0),unbuilt);
        tm.SetTile(new Vector3Int(0,-1),unbuilt);
        tm.SetTile(new Vector3Int(-1,0),unbuilt);
        tm.SetTile(new Vector3Int(-1,-1),unbuilt);

        //The things you can buy
        tileCoords.Add(new Vector2Int(0,0),corner);
        tileCoords.Add(new Vector2Int(0,-1),rightWall);
        tileCoords.Add(new Vector2Int(-1,0),leftWall);
        tileCoords.Add(new Vector2Int(-1,-1),empty);     
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
    
    public void buildTile(Vector2Int pos) {
        tm.SetTile(convVector(pos),tileCoords[pos]);
    }


    private Vector3Int convVector(Vector2Int vector) {
        return (new Vector3Int(vector.x,vector.y));
    }

    // Start is called before the first frame update
    void Start()
    {
        tileCoords = new Dictionary<Vector2Int,Tile> {};
        level1Start();
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
