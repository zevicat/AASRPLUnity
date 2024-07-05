using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 10f;
    [SerializeField] private float jumpPower;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask wallLayer;
    private Rigidbody2D badan;
    private Animator animasi;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float moveHorizontal;

    [Header("Sound")]
    [SerializeField] private AudioClip jumpSound;

    
    private void Awake(){
        // Untuk mendapatkan preference pada Rigidbody2D, Animator, BoxCollider2D dari objek
        badan = GetComponent<Rigidbody2D>();
        animasi = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    private void Update(){
        // Logic untuk berjalan pada kekanan dan kekiri
        moveHorizontal = Input.GetAxis("Horizontal");
        badan.velocity = new Vector2(moveHorizontal * speed, badan.velocity.y);
       
        // Untuk berubah arah pada karakter.
        if(moveHorizontal > 0.01f) {
            transform.localScale = Vector3.one;
        }else if (moveHorizontal < -0.01f) {
            transform.localScale = new Vector3(-1,1,1);
        }

        // Animasi pada saat mau diam atau bergerak
        animasi.SetBool("run", moveHorizontal !=0);

        // Animasi pada saat lompat
        animasi.SetBool("grounded", isGrounded());

        if(wallJumpCooldown < 0.2f){
            // Logic Unutk  melompat
            if(onWall() && !isGrounded()){
                badan.gravityScale = 0;
                badan.velocity = Vector2.zero;
                Debug.Log($"this on wall, {onWall()}");
                Debug.Log($"this on ground, {isGrounded()}");
            }
            else{
                badan.gravityScale = 1;
                Debug.Log($"this on wall, {onWall()}");
                Debug.Log($"this on ground, {isGrounded()}");
            }
            if(Input.GetKey(KeyCode.Space)){
                JumpButton();
                if(Input.GetKeyDown(KeyCode.Space)&& isGrounded()){
                    SoundManager.instance.PlaySound(jumpSound);
                }
            }
        }else{
            wallJumpCooldown += Time.deltaTime;
        }
    }

    // untuk melakukan lompatan
    private void JumpButton(){
        if(isGrounded()){
            // SoundManager.instance.PlaySound(jumpSound);
            badan.velocity = new Vector2(badan.velocity.x, jumpPower);
            animasi.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded()) {
            if(moveHorizontal == 0){
                badan.velocity = new Vector2(-Mathf.Sign(transform.localScale.x), 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }else{
                badan.velocity = new Vector2(-Mathf.Sign(transform.localScale.x), 5);
            }
            wallJumpCooldown = 0;
            Debug.Log(wallJumpCooldown);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack(){
        return moveHorizontal == 0 && isGrounded() && !onWall();
    }
}

