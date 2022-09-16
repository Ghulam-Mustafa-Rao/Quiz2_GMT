using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3[] spwaningPoints;
    public bool gameOver = false;
    public GameObject enemyPrefab;
    public static GameManager gameManager;
    public GameObject player;
    public GameObject enemyHolder;
    public bool explosionForceOccured = false;
    public int Score = 0;
    WaitForSeconds spwanWait = new WaitForSeconds(2f);

    private void Awake()
    {
        //make singleton of gameManager
        if (gameManager == null)
        {
            gameManager = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spwanEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if(explosionForceOccured)
        {

        }
    }

    IEnumerator spwanEnemies()
    {
        //spwan enemies till game is not over
        while (!gameOver)
        {
            //Instantiate enemy and assign player to him for player destination
            Instantiate(enemyPrefab, spwaningPoints[Random.Range(0, spwaningPoints.Length)], 
                enemyPrefab.transform.rotation).GetComponent<Enemy>().player=player;
            //wait for some time for next spwan
            yield return spwanWait;
        }
    }

    
}
