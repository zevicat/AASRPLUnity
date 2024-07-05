using System.Collections;
using UnityEngine;

public class damageFireTraps : MonoBehaviour
{
    [Header("Fire traps Timer")]
    [SerializeField] private float activasionTimerDelay;
    [SerializeField] private float activateTime;
    private Animator animasi;
    private SpriteRenderer spriteRend;

    private bool active;
    private bool triggered;

    [Header ("Damage Fire Traps")]
    [SerializeField] private float damageFire;


    private void Awake() {
        animasi = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

    }

     private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player"){
            if(!triggered){
                // trigger the firetrap
                StartCoroutine(activateFireTrap());
            }
            if(active){
                collision.GetComponent<Health>().takeDamage(damageFire);
            }
        }
    }

    private IEnumerator activateFireTrap(){
        //untuk sprite bewarna merah untuk memberikan notif ke player dan mentriger trap
        triggered = true;
        spriteRend.color = Color.red; 

        // Terjadi Delay, mengaktifkan Trap, mengaktifkan Animasi, dan mengembalikan warna ke normal
        yield return new WaitForSeconds(activasionTimerDelay);
        spriteRend.color = Color.white; // untuk kembali ke warna default
        active = true;
        animasi.SetBool("Activated", true);

        // menunggu sampai beberapa detik, menonaktifkan trap dan mereset semua variabel dan animator
        yield return new WaitForSeconds(activateTime);
        triggered = false;
        active = false;
        animasi.SetBool("Activated", false);
    }

}
