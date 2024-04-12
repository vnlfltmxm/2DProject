using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : Singleton<GameManger>
{
    public Vector2 wind;
    int windSpeed;
    int windDir;

    private void Awake()
    {
       CreatWind();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatWind()
    {
        windDir = Random.Range(0, 2);
        windSpeed = Random.Range(0, 11) * 50;
        if(windDir == 0)
        {
            wind = new Vector2(windSpeed, 0);
        }
        else
        {
            wind = new Vector2(-windSpeed, 0);
        }
    }
}
