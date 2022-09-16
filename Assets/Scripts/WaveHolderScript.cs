using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHolderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
}
