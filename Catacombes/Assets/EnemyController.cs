using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Transform target;

    public NavMeshAgent agent;

    public float rotationSpeed;

    public AudioClip chaseMusic;
    public AudioClip normalMusic;

    public bool chaseRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        Vector3 lookVector = target.position - transform.position;
        lookVector.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        agent.SetDestination(target.position);

        if (distance < 50f && chaseRange == false)
        {
            AudiManager.instance.ChangeSong(chaseMusic);
            chaseRange = true;
        }

        if (chaseRange == true && distance > 51)
        {
            AudiManager.instance.ChangeSong(normalMusic);
            chaseRange = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }
    }
}
