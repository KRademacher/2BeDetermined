using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmMachine : FSM
{
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        Attack,
    }

    //Current state that the NPC is reaching
    public FSMState curState;

    //Speed of the tank
    public float curSpeed;

    public float maxChaseDist;
    public float chaseSpeed;


    //Tank Rotation Speed
    private float curRotSpeed;


    //Whether the NPC is destroyed or not
    private bool bDead;
    private int health;

    public GameObject[] patrolPoints;

    private Vector3 startLocation;
    private int currentPos;


    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize()
    {
        curState = FSMState.Patrol;
        curSpeed = 150.0f;
        curRotSpeed = 2.0f;

        //Get the target enemy(Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        startLocation = patrolPoints[0].transform.position;
        currentPos = 0;

    }

    //Update each frame
    protected override void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
            case FSMState.Attack: UpdateAttackState(); break;
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 200f, Color.red);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 200f))
        {
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Player")
            {
                curState = FSMState.Chase;
            }
        }

        //Update the time
        elapsedTime += Time.deltaTime;
    }

    /// <summary>
    /// Patrol state
    /// </summary>
    protected void UpdatePatrolState()
    {

        this.transform.position = Vector3.MoveTowards(
            this.transform.position,
            patrolPoints[currentPos].transform.position,
            0.8f);

        Quaternion targetRotation = Quaternion.LookRotation(patrolPoints[currentPos].transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        if (this.transform.position == patrolPoints[currentPos].transform.position)
        {
            if (currentPos >= patrolPoints.Length - 1)
            {
                currentPos = 0;
            }
            else
            {
                currentPos++;
            }
        }
    }

    /// <summary>
    /// Chase state
    /// </summary>
    protected void UpdateChaseState()
    {

        //Set the target position as the player position
        Vector3 playerPos = playerTransform.position;

        Quaternion rotation = Quaternion.LookRotation(playerPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * curRotSpeed);

        transform.position += transform.forward * chaseSpeed * Time.deltaTime;
        Debug.Log(Vector3.Distance(transform.position, playerPos));

        if (Vector3.Distance(transform.position, playerPos) >= maxChaseDist)
        {
            curState = FSMState.Patrol;
        }
    }

    /// <summary>
    /// Attack state
    /// </summary>
    protected void UpdateAttackState()
    {

    }


    /// <summary>
    /// Check the collision with the bullet
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
            playerTransform.GetComponent<PointAndClick>().Explode();
    }
   

    /// <summary>
    /// Check whether the next random position is the same as current tank position
    /// </summary>
    /// <param name="pos">position to check</param>
    protected bool IsInCurrentRange(Vector3 pos)
    {
        float xPos = Mathf.Abs(pos.x - transform.position.x);
        float zPos = Mathf.Abs(pos.z - transform.position.z);

        if (xPos <= 50 && zPos <= 50)
            return true;

        return false;
    }
}
