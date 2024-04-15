using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject Parent;
    public Rigidbody2D rigid;
    bool isPlayerCheck;
    Vector2 destination = Vector2.zero;
    BombState state;

    // Start is called before the first frame update
    private void Awake()
    {
        Parent = transform.parent.gameObject;
        rigid = GetComponent<Rigidbody2D>();
        InitState();
    }
    void Start()
    {
        

    }

    private void OnEnable()
    {
        transform.position = Parent.transform.position;
        destination = GetComponentInParent<PlayerController>().bombThrowPos * 1.5f;
        transform.GetChild(0).gameObject.SetActive(false);
        state.ChangeState((BombStateName)GetComponentInParent<PlayerController>().bombIndex);
        gameObject.transform.SetParent(null);
        isPlayerCheck = false;
        rigid.AddForce(destination);
    }

    private void FixedUpdate()
    {
        state.FixedUpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        //rigid.AddForce(GameManger.Instance.wind * Time.deltaTime);
        state.UpdateState();
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlayerCheck)
        {
            return;
        }
        gameObject.transform.SetParent(Parent.transform);
        

        transform.GetChild(0).gameObject.SetActive(true);
        //this.gameObject.SetActive(false);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerCheck = true;
        }
    }

    private void OnDisable()
    {
        state.ChangeState(BombStateName.Normal);
        Parent.GetComponentInParent<PlayerController>().bombIndex = 0;
    }


    void InitState()
    {
        state = new BombState(BombStateName.Normal, new NormalBomb(this));
        state.AddState(BombStateName.Plus, new PlusBomb(this));
        state.AddState(BombStateName.Horming, new HormingBomb(this));
    }

}
