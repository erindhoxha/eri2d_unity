using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    public float move;
    public float speed = 6.5f;
    public float jump = 12;

    bool grounded = false;

    bool facingRight = true;

    bool isAttacking = false;
    bool isMagicAttacking = false;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [SerializeField] private AudioClip fireballSoundClip;
    [SerializeField] private AudioClip swordSoundClip;
    [SerializeField] private AudioClip swordSoundClip2;
    [SerializeField] private AudioClip jumpingSound;
    [SerializeField] private bool canDoubleJump = true;
    public Animator animator;
    [SerializeField] private GameObject attackHitbox;



    void StartAttack() {
        attackHitbox.SetActive(true);
    }
    void EndAttack() {
        attackHitbox.SetActive(false);
    }
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void ResetAttack() {
        isAttacking = false;
        EndAttack();
        animator.SetBool("isAttacking", false);
    }
    void ResetMagicAttack() {
        isMagicAttacking = false;
        animator.SetBool("isMagicAttacking", false);
    }
    void ThrowFireball() {
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(facingRight ? 1 : -1);
    }
    void Update() {
        move = Input.GetAxisRaw("Horizontal");
        if (!isAttacking) {
            rb.velocity = new Vector2(move * speed, rb.velocity.y);
        }
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", Math.Abs(rb.velocity.y));
        if (move > 0 && !facingRight || move < 0 && facingRight) {
            Flip();
        }
        if (Input.GetButton("Fire1")) {
            if (!isAttacking) {
                StartAttack();
                if (UnityEngine.Random.Range(0, 2) == 0)
                   SoundManager.instance.PlaySound(swordSoundClip2);
                else
                    SoundManager.instance.PlaySound(swordSoundClip);
                isAttacking = true;
                animator.SetBool("isAttacking", true);
                animator.Play("KnightAttack");
                Invoke("ResetAttack", .2f);
            }
            float attackMove = facingRight ? 1 : -1;
            rb.velocity = new Vector2(attackMove * speed / 2, rb.velocity.y);
        }
        if (Input.GetButton("Fire2")) {
            if (!isMagicAttacking) {
                isMagicAttacking = true;
                animator.SetBool("isMagicAttacking", true);
                animator.Play("KnightMagicAttack");
                SoundManager.instance.PlaySound(fireballSoundClip);
                Invoke("ThrowFireball", 0.2f);
                Invoke("ResetMagicAttack", 0.5f);

            }
            float attackMove = facingRight ? 1 : -1;
            rb.velocity = new Vector2(attackMove * speed / 2, rb.velocity.y);
        }
        if (Input.GetButton("Jump") && grounded) {
            SoundManager.instance.PlaySound(jumpingSound);
            grounded = false;
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }
    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Wall")) {
            if (canDoubleJump) {
                grounded = true;
                canDoubleJump = false;
            }
        }
        else if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("EnemyPlatform")) {
            grounded = true;
            canDoubleJump = true;
        }
    }
   private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
	