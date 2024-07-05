using System.Collections;
using UnityEngine;

public class SlimeHealth : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animasi;
    private bool dead;

    [Header("Iframe")]
    [SerializeField] private float iframesDuration;
    [SerializeField] private float numberOfSlashed;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Sounds")]
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        animasi = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void takeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            animasi.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }

                animasi.SetTrigger("die");

                //Player    
                if (GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;

                // Enemy Died
                if (GetComponentInParent<SlimeBiruPatrol>() != null)
                    GetComponentInParent<SlimeBiruPatrol>().enabled = false;

                if (GetComponent<SlimeBiruMelee>() != null)
                    GetComponent<SlimeBiruMelee>().enabled = false;

                dead = true;
            }
        }
    }

    public void GetHealth(float _healthValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + _healthValue, 0, startingHealth);
    }

    public void addHealth(float _healthAdd)
    {
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Clamp(currentHealth + _healthAdd, 0, startingHealth);
        }
    }

    public void RespawnHealth()
    {
        dead = false;
        GetHealth(startingHealth);
        animasi.ResetTrigger("died");
        animasi.Play("idle");
        StartCoroutine(Invunerability());
        Debug.Log("Respawn");

        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfSlashed; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframesDuration / (numberOfSlashed * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iframesDuration / (numberOfSlashed * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
