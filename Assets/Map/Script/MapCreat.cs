using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCreat : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] int adjustment = -10;
    [SerializeField] float seed;
    [SerializeField] float smooth;
    [SerializeField] TileBase topTile;
    [SerializeField] TileBase soilTile;
    [SerializeField] Tilemap tilemap;



    int[,] map;

    private void Start()
    {
        Genration();
    }

    void Genration()
    {
        tilemap.ClearAllTiles();
        map = GenerateArray(width, height, true);
        map = TerrainGeneration(map);
        RenderMap(map, tilemap, topTile, soilTile);
    }

    //map을 전부 0으로 채운다
    public int[,] GenerateArray(int width, int height, bool empty)//짜피 밑에서 바꾸는데 굳이 트루와 삼항연산자가 필요할까?
    {
        int[,] map = new int[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < height; k++)
            {
                map[i, k] = empty ? 0 : 1;
            }
        }
        return map;
    }

    //특정값을 1로 바꾼다
    public int[,] TerrainGeneration(int[,] map)
    {
        int perlinHeight;
        int f = 0;
        for (int i = 0; i < width; i++)//가로
        {
            perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(i / smooth, seed) * height / 2);//노이즈 생성 (물결이나 파동 같은거)
            f = perlinHeight;//시작점 백업용
            perlinHeight += height / 2;//노이즈의 높이설정

            for (int k = f; k < perlinHeight/2; k++)//세로
            {
                map[i, k] = 1;
            }//여기끝나면 세로로 한줄 값이 들어간다


            for (int k = f + perlinHeight / 2; k < perlinHeight; k++) 
            {
                map[i, k] = 1;
            }
        }
        


        return map;
    }

    //맵을 그린다
    public void RenderMap(int[,] map, Tilemap tilemap, TileBase topTile, TileBase soilTile)
    {
        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < height; k++)
            {
                if (k + 1 < height && map[i, k] == 1 && map[i, k + 1] == 0)
                {
                    tilemap.SetTile(new Vector3Int(i + adjustment, k + adjustment, 0), topTile);
                }
                else if (map[i, k] == 1 )
                {
                    tilemap.SetTile(new Vector3Int(i + adjustment, k + adjustment, 0), soilTile);
                }
            }
        }
    }


}
