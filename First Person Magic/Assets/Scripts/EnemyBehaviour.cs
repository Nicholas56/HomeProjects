using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public enum state { Attacking, Hiding, Searching}
    public state currentState = state.Searching;

    public NavMeshAgent nav;
    public float moveSpeed;

    public float searchRadius;
    public float searchFrequency;
    float searchTimer=-1f;
    public float proximity;

    Transform attackTarget;

    Queue<Transform> searchPoints = new Queue<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case state.Attacking:

                break;
            case state.Hiding:

                break;

            case state.Searching:
                Search();
                break;
        }
    }

    void Search()
    {
        Debug.Log("Searching");
        if (Time.time > searchTimer)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius);
            foreach (Collider hit in hitColliders)
            {
                if (hit.tag == "Player")
                {
                    currentState = state.Attacking;
                    attackTarget = hit.transform;
                }
            }
            searchTimer = Time.time + searchFrequency;
        }

        Debug.Log(searchPoints.Count);
        if (searchPoints.Count == 0)
        {
            Collider[] areaPoints = Physics.OverlapSphere(transform.position, searchRadius + 2);
            foreach(Collider point in areaPoints)
            {
                if (point.tag == "Target")
                {
                    searchPoints.Enqueue(point.transform);
                    Debug.Log("Finding points");
                    MoveTowards(searchPoints.Peek().transform);
                }
            }
        }
        else
        {
            if(Vector3.Distance(searchPoints.Peek().position, transform.position) < proximity)
            {
                searchPoints.Dequeue();

                MoveTowards(searchPoints.Peek().transform);
            }
        }
    }

    void MoveTowards(Transform dest)
    {
        nav.SetDestination(dest.position);
    }
}
