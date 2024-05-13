using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    [SerializeField] public Lever[] levers;

    private int[] order = { 3, 2, 4, 1, 5 }; // The order in which the levers should be triggered
    private List<int> sequence = new List<int>();

    private void Reset() {
        foreach (Lever lever in levers) {
            lever.triggered = false;
        }
    }

    public void AddId(int id) {
        sequence.Add(id);
            Debug.Log(sequence.Count);
            Debug.Log(order.Length);
        if (IsSequenceCorrect())
        {
            StartCoroutine(OpenDoor());
        } else if(sequence.Count == order.Length) {
            sequence = new List<int>();
            Invoke("Reset", 1f);
        }
    }
    private bool IsSequenceCorrect()
    {
        if (sequence.Count != order.Length)
        {
            return false;
        }

        for (int i = 0; i < order.Length; i++)
        {
            if (sequence[i] != order[i])
            {
                return false;
            }
        }
        return true;
    }
    private IEnumerator OpenDoor() {
        float duration = 1f; // Adjust this value to control the speed of the door opening
        float elapsed = 0f;
        float doorHeight = transform.localScale.y;
        Vector3 startingPosition = transform.position;
        Vector3 targetPosition = startingPosition + new Vector3(0, -doorHeight * 2, 0);

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}