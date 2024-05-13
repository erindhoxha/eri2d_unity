using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start() {
        if (totalHealthBar) {
            totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
        }
    }

    private void Update() {
        if (currentHealthBar) {
            currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
        }
    }
}
