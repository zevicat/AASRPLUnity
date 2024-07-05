using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SlimeBiruPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
   [SerializeField] private Transform leftEdge;
   [SerializeField] private Transform rightEdge;

   [Header ("Enemy")]
   [SerializeField] private Transform enemy;

   [Header ("Movement parameters")]
   [SerializeField] private float speed;
   private Vector3 initScale;
   private bool movingLeft;

    [Header ("Idle Behaviour")]
   [SerializeField] private float IdleDuration;
   private float IdleTimer;

    [Header ("Enemy Animator")]
   [SerializeField] private Animator anim;

   private void Awake()
   {
    initScale = enemy.localScale;
   }
   

   private void OnDisable()
   {
      anim.SetBool("moving", false);
   }

   private void Update()
   {
    if(movingLeft)
    {
        if(enemy.position.x >= leftEdge.position.x)
            MoveInDirection(-1);
        else
            DirectionChange();
        
    }
    else
    {
        if(enemy.position.x <= rightEdge.position.x)
        MoveInDirection(1);
        else
            DirectionChange();
        
    }
    
   }

   private void DirectionChange()
   {
    anim.SetBool("moving", false);

    IdleTimer += Time.deltaTime;

    if(IdleTimer > IdleDuration)

    movingLeft = !movingLeft;
   }

   private void MoveInDirection(int _direction)
   {
    IdleTimer = 0;
    anim.SetBool("moving", true);

    //make enemy face direction
    enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

    //Move in direction
    enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
   }
}
