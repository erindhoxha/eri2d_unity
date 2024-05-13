using UnityEngine;

public class Door2 : MonoBehaviour
        {
[SerializeField] private Firetrap[] triggers;
private bool opened = false;

    private void Update() {
        if (opened) {
            OpenDoor();
        }
        if (CheckTriggers()) {
            opened = true;
        }
    }
    private bool CheckTriggers() {
        foreach (Firetrap trigger in triggers) {
            if (!trigger.triggered) {
                return false;
            }
        }
        return true;
    }
    private void OpenDoor() {
        transform.Translate(0, -1 * Time.deltaTime * 2, 0);
    }
}
