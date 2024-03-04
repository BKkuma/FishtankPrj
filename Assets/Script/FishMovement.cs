using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private Transform target;
    private float rotationSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            
            Vector3 direction = target.position - transform.position;
            Vector3 forwardDirection = transform.forward;           
            float angle = Vector3.Angle(forwardDirection, direction);           
            Vector3 cross = Vector3.Cross(forwardDirection, direction);
            
            if (cross.y < 0)
                transform.Rotate(Vector3.up, -angle * Time.deltaTime * rotationSpeed);
            
            else
                transform.Rotate(Vector3.up, angle * Time.deltaTime * rotationSpeed);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        target = collision.transform;
    }
}
