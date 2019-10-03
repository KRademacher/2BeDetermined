using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private CharacterController controller;
    private Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            direction = Camera.main.transform.TransformDirection(direction);
        }

        direction.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(direction * moveSpeed * Time.deltaTime);
    }
}