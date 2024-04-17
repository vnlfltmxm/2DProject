using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject effect;
    public GameObject Parent;
    public Rigidbody2D rigid;
    Vector2 destination = Vector2.zero;
    public StateMachin<Bomb> state;

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
        transform.GetChild(0).gameObject.SetActive(false);
        
        if (Parent.CompareTag("Player"))
        {
            GameManger.Instance.CancelInvoke("EnemyTurn");
            transform.position = Parent.transform.position + (Vector3)Parent.GetComponent<PlayerController>().bombPos * 3;
            destination = GetComponentInParent<PlayerController>().bombThrowPos * 1.5f;
            state.ChangeState((BombStateName)GetComponentInParent<PlayerController>().bombIndex);
        }
        else
        {
            transform.position = Parent.transform.position;
            destination = GetComponentInParent<EnemyController>().bombPos * 1.5f;
        }
        gameObject.transform.SetParent(null);
        CameraController.instanse.FollowCamera(this.gameObject);
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
        gameObject.transform.SetParent(Parent.transform);
        effect.SetActive(true);
        effect.transform.position = transform.position;
        transform.GetChild(0).gameObject.SetActive(true);
        //this.gameObject.SetActive(false);

    }


    private void OnDisable()
    {
        state.ChangeState(BombStateName.Normal);
        if (Parent.CompareTag("Player"))
        {
            Parent.GetComponentInParent<PlayerController>().bombIndex = 0;
        }
        else
        {
            return;
        }
    }


    void InitState()
    {
        state = new StateMachin<Bomb>(BombStateName.Normal, new NormalBomb(this));
        state.AddState(BombStateName.Plus, new PlusBomb(this));
        state.AddState(BombStateName.Horming, new HormingBomb(this));
    }

}
