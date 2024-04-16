using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : Singleton<GameManger>
{
    [SerializeField] public GameObject player;
    [SerializeField] GameObject itemBoxPrefab;
    [SerializeField] GameObject backGround;
    public Vector2 wind;
    public bool playerTurn = true;
    public bool playerThrow = false;
    public Queue<GameObject> itemBoxPool = new Queue<GameObject>();

    PlayerController playerController;
    BoxCollider2D bgCollider;
    int windSpeed;
    int windDir;

    private void Awake()
    {
        playerTurn = false;
        bgCollider = backGround.GetComponent<BoxCollider2D>();
       playerController = player.GetComponent<PlayerController>();
        
       CreatWind();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject itemBox = Instantiate(itemBoxPrefab);
            itemBoxPool.Enqueue(itemBox);
            itemBox.SetActive(false);
        }
        CameraController.instanse.StartCamera();
        Invoke("StartGame", 2);
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerTurn && !playerThrow) 
        //{
        //    playerController.playerState.ChangeState(StateName.Move);
        //}

       
        SpawItem();

    }
    void StartGame()
    {
        playerTurn = true;
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

    void SpawItem()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            if (itemBoxPool.Count > 0)
            {
                GameObject itemBox = itemBoxPool.Dequeue();
                itemBox.SetActive(true);
                //itemBoxPool.Enqueue(itemBox);

                float rd = Random.Range(-bgCollider.size.x * 3, bgCollider.size.x * 3);

                itemBox.transform.position = new Vector3(rd, bgCollider.size.y * 3, 0);
            }
            else
            {
                playerTurn = true;
            }
            
                ChangeWind();

        }
    }

    public void ChangeWind()
    {
        CreatWind();
    }
}
