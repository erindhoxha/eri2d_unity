using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Serialize] public bool noWaitingDamageTime;

    public GameObject blood;
    public GameObject fire;

    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage, string type = "default")
    {
        if (invulnerable && !noWaitingDamageTime) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
            if (blood != null)
            {
                if (type == "fire") {
                    Instantiate(blood, transform.position, Quaternion.identity);
                } else {
                    Instantiate(blood, transform.position, Quaternion.identity);
                }
            }
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                SoundManager.instance.PlaySound(deathSound);
                anim.SetTrigger("dead");
                if (blood != null) {
                    if (type == "fire") {
                        Instantiate(blood, transform.position, Quaternion.identity);
                    } else {
                        Instantiate(blood, transform.position, Quaternion.identity);
                    }
                }
                foreach (Behaviour component in components)
                    component.enabled = false;
                dead = true;
            }
        }
    }
    public void Heal(float _heal) {
        currentHealth = Mathf.Clamp(currentHealth + _heal, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        SpriteRenderer[] childSpriteRends = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < numberOfFlashes; i++)
        {
            foreach (SpriteRenderer spriteRend in childSpriteRends)
            {
                spriteRend.color = new Color(1, 0, 0, 1f);
            }
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            foreach (SpriteRenderer spriteRend in childSpriteRends)
            {
                spriteRend.color = Color.white;
            }
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        invulnerable = false;
    }
}