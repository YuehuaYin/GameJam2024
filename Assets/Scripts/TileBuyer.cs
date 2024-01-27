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

    public Tilemap tm;

    private Dictionary<Vector2Int, Tile> tileCoords;

    private int[] prices = {1,2,3,4,5,6};

    private int getPrice() {
        return prices[GameManager.level];
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
        tileCoords = new Dictionary<Vector2Int, Tile>{};
        tileCoords.Add(new Vector2Int(0,0),corner);
        tileCoords.Add(new Vector2Int(-1,-1),empty);
        buildTile(new Vector2Int(0,0));
        buildTile(new Vector2Int(-1,-1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
