using System;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    [Header("Sound")]
    [SerializeField] private AudioClip soundMeleeAttack;


    //References
    private Animator animasi;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;
  

    private void Awake()
    {
        animasi = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        // attack only when player in sight?
        if(playerInSight()){
            if(cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0){
                cooldownTimer = 0;
                // attack player if he insight enemy
                animasi.SetTrigger("Attack");
                // sound melee attack
                SoundManager.instance.PlaySound(soundMeleeAttack);

            } 
        }
        if(enemyPatrol != null){
            enemyPatrol.enabled = !playerInSight();
         }
        
    }

    private bool playerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
         0, Vector2.left, 0, playerLayer);
        if(hit.collider != null){
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;

    }
     private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center+ transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void damagePlayer(){
        // if player still in range damage
        if(playerInSight()){
            playerHealth.takeDamage(damage);
        }

    }
}
