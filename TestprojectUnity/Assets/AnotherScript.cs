using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AnotherScript : MonoBehaviour
{
    Ray ray;
    public LayerMask lm;
    public Transform destination;
    public NavMeshAgent nma;
    float distance;
    Vector3 b;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private IEnumerator Enumerator ()
    {
        yield return new WaitForSeconds(0.1f);
        SearchforSelector();
    }
    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, destination.position);
        if (nma.enabled)
        {
            if (distance <= 0.1f)
            {
                nma.enabled = false;
            }
            nma.SetDestination(destination.position);
        }
        
        
        if (!nma.enabled)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                forward();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                left();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                right();

            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                back();
            }
        }
    }
    void forward()
    {
        b = transform.forward;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        SearchforSelector();
        StartCoroutine("Enumerator");
    }
    void right()
    {
        b = transform.right;
        transform.rotation = Quaternion.Euler(0, 90, 0);
        SearchforSelector();
        StartCoroutine("Enumerator");
    }
    void left()
    {
        b = -transform.right;
        transform.rotation = Quaternion.Euler(0, -90, 0);
        SearchforSelector();
        StartCoroutine("Enumerator");
    }
    void back()
    {
        b = -transform.forward;
        SearchforSelector();
        StartCoroutine("Enumerator");
    }
    void SearchforSelector()
    {
        ray = new Ray(transform.position, b);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, lm))
        {
            nma.enabled = true;
            destination = hit.collider.transform;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ray);
    }
}
