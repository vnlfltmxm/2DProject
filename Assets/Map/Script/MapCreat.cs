using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCreat : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] int adjustmentX = -10;
    [SerializeField] int adjustmentY = -10;
    [SerializeField] float seed;
    [SerializeField] float smooth;
    [SerializeField] TileBase topTile;
    [SerializeField] TileBase soilTile;
    [SerializeField] TileBase leftTile;
    [SerializeField] TileBase rightTile;
    [SerializeField] TileBase soilLeftTile;
    [SerializeField] TileBase soilRightTile;
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
        RenderMap(map, tilemap, topTile, soilTile, leftTile, rightTile);
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
        for (int i = 0; i < width; i++)//����
        {
            perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(i / smooth, seed) * height / 3);//������ ���� (�����̳� �ĵ� ������)
            //f = perlinHeight;//������ �����
            //perlinHeight += height / 2;//�������� ���̼���

            //for (int k = f; k < perlinHeight / 2 + 3; k++)//����
            //{
            //    map[i, k] = 1;
            //}//���ⳡ���� ���η� ���� ���� ����


            //for (int k = f + height / 2; k < height - f + 3; k++) 
            //{
            //    map[i, k] = 1;
            //}
            for (int k = perlinHeight; k < 10+perlinHeight; k++)//����
            {
                map[i, k] = 1;
            }//���ⳡ���� ���η� ���� ���� ����


            for (int k = perlinHeight + height/2; k < 10+ perlinHeight + height / 2; k++)
            {
                map[i, k] = 1;
            }
        }
        


        return map;
    }

    //���� �׸���
    public void RenderMap(int[,] map, Tilemap tilemap, TileBase topTile, TileBase soilTile,TileBase leftTile,TileBase rightTile)
    {
        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < height; k++)
            {
                if (k + 1 < height && map[i, k] == 1 && map[i, k + 1] == 0)
                {
                    if (i + 1 < width && map[i + 1, k] == 0) 
                    {
                        tilemap.SetTile(new Vector3Int(i + adjustmentX, k + adjustmentY, 0), leftTile);
                    }
                    else if(i - 1 > 0 && map[i - 1, k] == 0)
                    {
                        tilemap.SetTile(new Vector3Int(i + adjustmentX, k + adjustmentY, 0), rightTile);
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(i + adjustmentX, k + adjustmentY, 0), topTile);

                    }
                }
                else if (map[i, k] == 1 )
                {
                    tilemap.SetTile(new Vector3Int(i + adjustmentX, k + adjustmentY, 0), soilTile);
                }
            }
        }
    }

}
