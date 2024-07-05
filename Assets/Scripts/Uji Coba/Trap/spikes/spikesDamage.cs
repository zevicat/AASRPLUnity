using UnityEngine;

public class spikesDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            collision.GetComponent<Health>().takeDamage(damage);
        }        
    }
}