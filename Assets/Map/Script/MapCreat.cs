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

    //map�� ���� 0���� ä���
    public int[,] GenerateArray(int width, int height, bool empty)//¥�� �ؿ��� �ٲٴµ� ���� Ʈ��� ���׿����ڰ� �ʿ��ұ�?
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

    //Ư������ 1�� �ٲ۴�
    public int[,] TerrainGeneration(int[,] map)
    {
        int perlinHeight;
        int f = 0;
        for (int i = 0; i < width; i++)//����
        {
            perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(i / smooth, seed) * height / 2);//������ ���� (�����̳� �ĵ� ������)
            f = perlinHeight;//������ �����
            perlinHeight += height / 2;//�������� ���̼���

            for (int k = f; k < perlinHeight/2; k++)//����
            {
                map[i, k] = 1;
            }//���ⳡ���� ���η� ���� ���� ����


            for (int k = f + perlinHeight / 2; k < perlinHeight; k++) 
            {
                map[i, k] = 1;
            }
        }
        


        return map;
    }

    //���� �׸���
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
