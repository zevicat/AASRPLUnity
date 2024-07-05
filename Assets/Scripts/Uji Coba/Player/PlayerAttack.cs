using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] projectile;
    // [SerializeField] private AudioClip fireballSound;
    private Animator animasi;
    private playerMovent playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        animasi = GetComponent<Animator>();
        playerMovement = GetComponent<playerMovent>();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.I) && cooldownTimer > attackCooldown && playerMovement.canAttacks()){
            buttonAttack();
        }else if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttacks()){
            buttonAttack();
        }
        cooldownTimer += Time.deltaTime;
    }
    private void buttonAttack(){
        // SoundManager.instance.PlaySound(fireballSound);
        animasi.SetTrigger("attack");
        cooldownTimer = 0;

        projectile[findProjectile()].transform.position = firepoint.position;
        projectile[findProjectile()].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findProjectile(){

        for(int i = 0; i < projectile.Length; i ++ ){
            if(!projectile[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

}
