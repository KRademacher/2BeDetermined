using System.Collections;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            door.SetActive(false);
        }
    }
}