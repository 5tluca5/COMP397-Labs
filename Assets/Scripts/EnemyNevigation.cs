using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNevigation : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] List<Transform> points = new List<Transform>();
    [SerializeField] int index = 0, viewDistance = 10;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 destination;

    NavMeshAgent agent;
    EnemyEnums enemyState = EnemyEnums.Patrolling;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = points[index].position;
        agent.SetDestination(destination);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyState = EnemyEnums.Patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyEnums.Chasing)
        {
            destination = player.position;
        }
        else if (Vector3.Distance(points[index].position, agent.transform.position) < 1.0f)
        {
            index++;

            if(index >= points.Count) index = 0;

            destination = points[index].position;
        }

        agent.SetDestination(destination);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

            if (hit.transform.gameObject.name.Equals("Player"))
            {
                Debug.Log($"Change destination to {hit.transform.gameObject.name}");
                enemyState = EnemyEnums.Chasing;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * viewDistance, Color.red);

        }
    }
}
