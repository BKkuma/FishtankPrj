using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 acceleration;
    [SerializeField] private float mass;
    [SerializeField] private float arrived_dist;
    [SerializeField] private Transform target;
    [SerializeField] private Mover[] agents;

    private float avoidanceDist = 5.0f;
    private float avoiddanceForce = 6.0f;
    private float cohesionForce = 5.0f;
    private float maxSpeed = 5;
    private void Start()
    {
        velocity = new Vector3(0.0f, 0.0f, 0.0f);
        acceleration = new Vector3(0.0f, 0.0f, 0.0f);
        mass = 1.0f;
        

        agents = gameObject.transform.parent.GetComponentsInChildren<Mover>();


    }
    public void AddForce(Vector3 force)
    {
        acceleration += force / mass;
    } 
    public void SeekTo (  )
    {
            Vector3 d = target.position - transform.position;
            d.Normalize();
            d *= maxSpeed;
            Vector3 f = d - velocity;

            float distance = (target.position - transform.position).magnitude;

            if (distance < avoidanceDist)
            {
                float p = distance / avoidanceDist;
                float energy = f.magnitude * p;
                f.Normalize();
                f *= energy;
                AddForce(f);
            }
            else
            {
                velocity *= 0.5f;
            }
    }

    

    private void Avoiddance()
    {

        Vector3 f = Vector3.zero;
        for (int i = 0; i < agents.Length; i++)
        {
            Vector3 d = transform.position - agents[i].transform.position;
            float dist = d.magnitude;
            if (dist < avoidanceDist)
            {
                f += d;
            }
        }
        f = f.normalized;
        f *= avoiddanceForce;

        AddForce(f);
    }

    private void Cohesion()
    {
        Vector3 f = Vector3.zero;
        for (int i = 0; i < agents.Length; i++)
        {
            Vector3 d = agents[i].transform.position - transform.position;
            float dist = d.magnitude;
            if (dist < 10)
            {
                f += d;
            }
        }
        f = f.normalized;
        f *= cohesionForce;

        AddForce(f);
    }

    private void Alignment()
    {
        Vector3 f = Vector3.zero;


        for (int i = 0; i < agents.Length; i++)
        {
            Vector3 d = transform.position - agents[i].transform.position;
            float dist = d.magnitude;
            if (dist < 7)
            {
                f += agents[i].velocity;
            }
        }
        f = f.normalized;
        f *= 1;

        AddForce(f);
    }
    private void FixedUpdate()
    {
        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
        acceleration *= 0;

    }

    private void Update()
    {

        SeekTo();
        Avoiddance();
        Cohesion();
        Alignment();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, avoidanceDist);
    }

}
