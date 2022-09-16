using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float speed;
    float horizontalInput;
    float verticalInput;
    public float velocityLimit;
    public float explosionForce;
    public float explosionRadius;
    public GameObject waveHolder;
    public GameObject wave;
    public GameObject explosionColliderHolder;
    float counter = 0;
    bool produceForce = false;
    // Start is called before the first frame update
    void Start()
    {
        //rigidbody.AddExplosionForce()
    }

    // Update is called once per frame
    void Update()
    {
        //Get Players Move Input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Move Player according to Player Move Inputs
        rigidbody.AddForce(Vector3.forward * verticalInput * speed);
        rigidbody.AddForce(Vector3.right * horizontalInput * speed);

        //Limit speed of player
        if (rigidbody.velocity.magnitude > velocityLimit)
        {
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, velocityLimit);
        }
        if (rigidbody.velocity.magnitude < -velocityLimit)
        {
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, -velocityLimit);
        }

        //stop player if user dont want to move player
        if (verticalInput == 0 && horizontalInput == 0)
        {
            rigidbody.velocity = Vector3.zero;
        }

        //counter in seconds to allow next explosion 
        counter += Time.deltaTime;
        //Add explosion force on left mouse button preesed
        if (Input.GetMouseButton(0) && counter > 0.4)
        {
            GameObject waveH = Instantiate(waveHolder, explosionColliderHolder.transform.position, explosionColliderHolder.transform.rotation);
            waveH.transform.localScale = explosionColliderHolder.transform.lossyScale;
            Instantiate(wave, Vector3.zero, wave.transform.rotation).transform.SetParent(waveH.transform);
            StartCoroutine(waveH.GetComponent<WaveHolderScript>().DestroyThis());
            //waveO.GetComponent<Animator>().SetBool("WaveGo_b",true);

            counter = 0;
            produceForce = true;
        }
        else
        {
            produceForce = false;
        }
    }

    private void FixedUpdate()
    {
        if (produceForce)
        {
            //get colliders of our player, as our player is a sphere so we use OverlapSphere
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider hit in colliders)
            {
                // hit.gameObject.tag = "explosionCollider";
                Rigidbody rg = hit.GetComponent<Rigidbody>();
                if (rg != null)
                {
                    rg.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0, ForceMode.Impulse);
                }
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy current object on collision wiyh wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            GameManager.gameManager.gameOver = true;
            Destroy(this.gameObject);
        }
    }
}
