using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionColliderHolder : MonoBehaviour
{
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionToLookAt = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        positionToLookAt.y = 0;
        transform.LookAt(positionToLookAt);
         
    }
}
