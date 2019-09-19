using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
