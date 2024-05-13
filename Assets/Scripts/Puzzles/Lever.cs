using UnityEngine;
 
public class Lever : MonoBehaviour
{
    private Animator anim;
 
    public bool triggered = false;

    public int leverID;
    public delegate void LeverTriggered(int leverID);
    public static event LeverTriggered OnLeverTriggered;

    public Puzzle1 puzzle1;

    public void TriggerLever()
    {
        OnLeverTriggered?.Invoke(leverID);
    }
    // Call this method when the lever is triggered
    public void Trigger()
    {
        triggered = true;
        puzzle1.AddId(leverID);
    }
 
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (triggered) {
            anim.SetBool("triggered_bool", true);
        } else {
            anim.SetBool("triggered_bool", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered) {
               Trigger();
            }
        }
    }
}