using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Rendering.Universal.Internal;

public class RaycastTorch : MonoBehaviour
{
     
    NavMeshAgent agent;
    GameObject lastHit;
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;

    bool issupressed = false;



    void Update()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 15f, Color.green);
        if (Physics.Raycast(ray, out hit, 15))
        {
            lastHit = hit.transform.gameObject;
            collision = hit.point;
        }

        if (lastHit.tag == "Enemy")
        {
            if (hit.collider.gameObject.GetComponent<NavMeshAgent>() != null)
            {
                agent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
                agent.speed = 0f;
                issupressed = true;
            }
       
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            agent.speed = 20f;
        }
    }
}
