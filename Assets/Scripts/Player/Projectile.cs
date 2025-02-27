using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 12;
    private bool hit;
    private float direction;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Invisible") {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("explode");
            Debug.Log("Exploding!");
        }
        if (collision.tag == "Enemy") {
            collision.GetComponent<Health>().TakeDamage(2);
        }
    }
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
