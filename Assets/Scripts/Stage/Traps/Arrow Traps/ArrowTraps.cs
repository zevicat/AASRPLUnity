
using UnityEngine;

public class ArrowTraps : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;
    
    private void Attack(){
        cooldownTimer = 0;
        int fireballIndex = FindArrows();
        arrows[fireballIndex].transform.position = firePoint.position;
        arrows[fireballIndex].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrows() {
        for(int i = 0; i < arrows.Length; i ++) {
            if(!arrows[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(cooldownTimer>= attackCooldown){
            Attack();
        }
    }
}
