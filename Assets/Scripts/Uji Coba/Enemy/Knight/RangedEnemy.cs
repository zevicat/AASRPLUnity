using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;


    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;


    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    //References
    private Animator animasi;
    private Health playerHealth;
    // private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        animasi = GetComponent<Animator>();
        // enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        // attack only when player in sight?
        if(playerInSight()){
            if(cooldownTimer >= attackCooldown){
                cooldownTimer = 0;
                animasi.SetTrigger("rangedAttack");

            } 
        }
        // if(enemyPatrol != null){
        //     enemyPatrol.enabled = !playerInSight();
        //  }
    }
    private bool playerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
         0, Vector2.left, 0, playerLayer);
        // if(hit.collider != null){
        //     playerHealth = hit.transform.GetComponent<Health>();
        // }
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center+ transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void rangedAttack(){
        cooldownTimer = 0;
        fireballs[findFireballs()].transform.position = firePoint.position;
        // fireballs[findFireballs()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int findFireballs(){
        for(int i = 0; i < fireballs.Length; i++){
            if(!fireballs[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }
    // private void damagePlayer(){
    //     if(playerInSight()){
    //         playerHealth.takeDamage(damage);
    //     }
    // }
}
