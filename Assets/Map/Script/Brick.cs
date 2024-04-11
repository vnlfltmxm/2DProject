using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Brick : MonoBehaviour
{
    Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RemoveTile(Vector3 pos)
    {
        Vector3Int cellPos = tilemap.WorldToCell(pos);
        tilemap.SetTile(cellPos, null);
    }
}
