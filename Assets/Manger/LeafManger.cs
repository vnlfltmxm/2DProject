using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafManger : Singleton<LeafManger>
{
    [SerializeField] GameObject leafPrefab;
    [SerializeField] GameObject bg;
    [SerializeField] Camera mainCamera;
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
            yield return new WaitForSeconds(1.5f);

            for(int i =0; i < 5; i++)
            {
                GameObject leaf = leafQueue.Dequeue();
                leafQueue.Enqueue(leaf);
                leaf.SetActive(false);
                leaf.SetActive(true);

                float cameraY = mainCamera.transform.position.y + mainCamera.orthographicSize;
                float minX = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
                float maxX = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
                //float rd = Random.Range(-bgCollider.size.x * 3 - 5, bgCollider.size.x * 3 + 5);
                //float rd = Random.Range(minX - 15, maxX + 15);
                float leftRd = Random.Range(minX - 15, maxX);
                float rightRd = Random.Range(minX , maxX + 15);
                //float rdY = Random.Range(bgCollider.size.y * 3 + 5, bgCollider.size.y * 3 + 10);
                float rdY = Random.Range(cameraY + 1, cameraY + 3);

                if (GameManger.Instance.wind.x < 0)
                {
                    leaf.transform.position = new Vector3(rightRd, rdY, 0);

                }
                else
                {
                    leaf.transform.position = new Vector3(leftRd, rdY, 0);
                }

            }



        }
    }

}
