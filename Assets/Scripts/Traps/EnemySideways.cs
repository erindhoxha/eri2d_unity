using Unity.VisualScripting;
using UnityEngine;

public class EnemySideways : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private float movementDistance = 3.5f;
    [SerializeField] private float speed = 3;

    [SerializeField] private float healthValue = 2;
     private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake() {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update() {
        if (movingLeft) {
            if (transform.position.x > leftEdge) {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                movingLeft = false;
            }
        } else {
            if (transform.position.x < rightEdge) {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                movingLeft = true;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fireball") {
            healthValue -= 1;
            if (healthValue <= 0) {
                Destroy(gameObject);
            }
        }
       if (collision.tag == "Player") {
            collision.GetComponent<Health>().TakeDamage(damage);
       }
       if (collision.tag == "Sword") {
         Debug.Log("Hitting something with sword!");
       }
    }
}
