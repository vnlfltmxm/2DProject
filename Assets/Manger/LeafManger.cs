using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafManger : Singleton<LeafManger>
{
    [SerializeField] GameObject leafPrefab;
    [SerializeField] GameObject bg;
    [SerializeField] int leafCount;

    Queue<GameObject> leafQueue = new Queue<GameObject>();
    BoxCollider2D bgCollider;

    private void Awake()
    {
        //leafPrefab = GetComponent<GameObject>();
        bgCollider = bg.GetComponent<BoxCollider2D>();

        for (int i = 0; i < leafCount; i++)
        {
            GameObject leaf = Instantiate(leafPrefab);
            leaf.SetActive(false);
            leafQueue.Enqueue(leaf);

        }

        StartCoroutine(LeafSpawn());

    }

    IEnumerator LeafSpawn()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(2);

            for(int i =0; i < 5; i++)
            {
                GameObject leaf = leafQueue.Dequeue();
                leafQueue.Enqueue(leaf);
                leaf.SetActive(false);
                leaf.SetActive(true);

                float rd = Random.Range(-bgCollider.size.x * 3 - 5, bgCollider.size.x * 3 + 5);
                float rdY = Random.Range(bgCollider.size.y * 3 - 5, bgCollider.size.y * 3 + 5);

                leaf.transform.position = new Vector3(rd, rdY, 0);
            }



        }
    }

}
