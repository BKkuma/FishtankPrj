using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPfood : MonoBehaviour
{
    [SerializeField]private float hpFood = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            hpFood -= 1.0f;
            if (hpFood <= 0.0f)
            {
                Destroy(gameObject);
            }
           
        }
        

    }
}
