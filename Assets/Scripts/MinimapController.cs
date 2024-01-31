using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }
}
