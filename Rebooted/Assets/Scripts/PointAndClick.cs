using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointAndClick : MonoBehaviour
{
    // Start is called before the first frame update
    
    private bool moving;
    private bool gotHead;
    void Start()
    {
        moving = false;
        gotHead = false;
    }

    // Update is called once per frame

    public float speed;
    private Ray ray;
    private Vector3 target;
    private RaycastHit hit;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100)){
                target = new Vector3(hit.point.x , transform.position.y, hit.point.z);
                transform.position = Vector3.MoveTowards(transform.position,target,speed);
                moving = true;
            }
        }
        if(moving){
            transform.position = Vector3.MoveTowards(transform.position,target,speed);
            if(Vector3.Distance(transform.position,target)< 0.3f){
                moving = false;
            }
        }
    }
    public void Explode()
    {
        float rndX = Random.Range(10.0f, 30.0f);
        float rndZ = Random.Range(10.0f, 30.0f);
        for (int i = 0; i < 3; i++)
        {
            GetComponent<Rigidbody>().AddExplosionForce(10000.0f, transform.position - new Vector3(rndX, 10.0f, rndZ), 40.0f, 10.0f);
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(rndX, 20.0f, rndZ));
        }

        Destroy(gameObject, 1.5f);

        SceneManager.LoadScene(2);
    }

    void OnTriggerEnter(Collider other)
        {

            if(other.gameObject.CompareTag("robotHead"))
            {
                other.gameObject.SetActive(false);
                gotHead = true;
            }
            if(other.gameObject.CompareTag("door")){
                other.gameObject.SetActive(false);
            }
            
        }

        bool getGotHead(){
            return gotHead;
        }
}
