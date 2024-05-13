using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cameraController;

private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.tag == "Player") {
        if (collision.transform.position.x < transform.position.x) {
            cameraController.MoveToNewRoom(nextRoom);
            nextRoom.GetComponent<Rooms>().ActivateRoom(true);
            previousRoom.GetComponent<Rooms>().ActivateRoom(false);
        } else {
            cameraController.MoveToNewRoom(previousRoom);
            nextRoom.GetComponent<Rooms>().ActivateRoom(false);
            previousRoom.GetComponent<Rooms>().ActivateRoom(true);
        }
    }
}
}
