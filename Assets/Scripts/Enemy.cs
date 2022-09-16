using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    public Rigidbody rigidbody;

    private void Awake()
    {
        //set objects parent to keep hirarchy simple
        transform.SetParent(GameManager.gameManager.enemyHolder.transform);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        // Set Destination for agent to move towards
        if (player != null && agent.enabled)
        {
            agent.SetDestination(player.transform.position);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Repel enemy
        if (other.gameObject.CompareTag("explosionCollider"))
        {
            agent.enabled = false;
            StartCoroutine(enableAgentCo());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy current object on collision wiyh wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            //add 25 score on each kill
            GameManager.gameManager.Score += 25;
            Destroy(this.gameObject);
        }

       
    }

    

    IEnumerator enableAgentCo()
    {
        yield return new WaitForSeconds(1f);
        agent.enabled = true;
    }

    
}
