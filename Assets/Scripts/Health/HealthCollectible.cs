using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
   [SerializeField] private float healthValue = 1;

   [SerializeField] private AudioClip collectSound;

   private void OnTriggerEnter2D(Collider2D collision) {
       if (collision.tag == "Player") {
            collision.GetComponent<Health>().Heal(healthValue);
            gameObject.SetActive(false);
            SoundManager.instance.PlaySound(collectSound);
       }
   }
}
