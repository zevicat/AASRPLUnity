// using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // Start is called before the first frame update
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameter")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator animasi;


    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        animasi.SetBool("moving", false);
    }
    private void Update()
    {
        if(movingLeft){
            if(enemy.position.x >= leftEdge.position.x){
                moveInDirection(-1);
            }else{
                // change direction
                directionChange();
            }
        }else{
            if(enemy.position.x <= rightEdge.position.x){
            moveInDirection(1);
            }else{
                // change direction
                directionChange();
            }
        }
    }
    private void directionChange(){
        animasi.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration){
            movingLeft = !movingLeft;
        }
    }
    private void moveInDirection(int _direction){
        idleTimer = 0;
        animasi.SetBool("moving", true);
        // make enemy face
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        // move in that direction
        enemy.position = new UnityEngine.Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
        enemy.position.y, enemy.position.z);
    }
}
