
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class HealthCollect : MonoBehaviour
{
    [SerializeField] private float healthValue;
    private Health playerHealth;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerHealth = collision.GetComponent<Health>();
        if (collision.tag == "Player" && playerHealth != null && playerHealth.currentHealth < playerHealth.startingHealth)
        {
            // SoundManager.instance.PlaySound(collectHe althSound);
            collision.GetComponent<Health>().addHealth(healthValue);
            gameObject.SetActive(false);
        } else{
            return;
        }
    }

}
