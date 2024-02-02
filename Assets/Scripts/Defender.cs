using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [Header("Vision")]
    [SerializeField] Transform vision;
    [SerializeField] float visionRadius = 10f;
    [SerializeField] LayerMask targetMask;

    [Header("Shader")]
    [SerializeField] Shader outlineShader;
    [SerializeField] MeshRenderer meshRenderer;

    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 0.2f;
    private float fireTimer = 0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(vision.position, visionRadius);
    }

    private void FixedUpdate()
    {
        var encounterPlayer = Physics.CheckSphere(vision.position, visionRadius, targetMask);

        if(encounterPlayer)
        {
            //Debug.Log($"[{gameObject.name}] Encountered player");

            //Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            //Vector3 direction = (target.position - bullet.transform.position).normalized;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log($"[{gameObject.name}] Encountered {other.name}");

    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log($"[{gameObject.name}] On trigger stay.");

        if (other.CompareTag("Player"))
        {
            fireTimer += Time.deltaTime;

            if(fireTimer > fireRate)
            {
                Debug.Log($"[{gameObject.name}] FIRE!!");

                var bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity).GetComponent<Projectile>();
                Vector3 direction = (other.transform.position - firePoint.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);
                //bullet.transform.rotation = rotation;
                bullet.Fire(direction);

                fireTimer = 0;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        fireTimer = 0;
    }
}
