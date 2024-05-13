using UnityEngine;
using System.Collections;
 
public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;
 
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    [SerializeField] private AudioClip fireTrapSound;
    private Animator anim;
    private SpriteRenderer spriteRend;
 
    public bool triggered;
    public bool active;
 
    private Health player;

    private bool canTakeDamage;
 
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (canTakeDamage && active) {
            if (player) {
                player.TakeDamage(damage);
            }
        }
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canTakeDamage = true;
        if (collision.tag == "Player")
        {
            Debug.Log("Player entered the trap!");
            if (!triggered)
                StartCoroutine(ActivateFiretrap());
 
            player = collision.GetComponent<Health>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canTakeDamage = false;
        player = null;
    }
    
 
    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to notify the player and trigger the trap
        triggered = true;
        anim.SetBool("triggered", true);
 
        //Wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(fireTrapSound);
        spriteRend.color = Color.white; //turn the sprite back to its initial color
        active = true;
        anim.SetBool("triggered", false);
        anim.SetBool("activated", true);
 
        //Wait until X seconds, deactivate trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}