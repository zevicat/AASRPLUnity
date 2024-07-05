using Unity.VisualScripting;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private int deadthCount = 0;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindAnyObjectByType<UIManager>();
    }


    private void CheckRespawn()
    {
        deadthCount += 1; 
        // Check if check point available
        if (currentCheckpoint == null)
        {
            // show game over screen
            uiManager.GameOver();
            Debug.Log("IS null");
            gameObject.SetActive(false);
            return; //don't execute the rest of this function
        }else{
            if(deadthCount >= 2){
                uiManager.GameOver();
            }else{
                Debug.Log("Not Null");
                playerHealth.RespawnHealth();
                transform.position = currentCheckpoint.position;
                Debug.Log("succes");
                // Move Camera to checkpoints room(for this to work the checkpoint objects has to placed as a child of the room object)
                Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
            }
        }
        //validasi    
    }
    //Activate checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint"){
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
        
    }

}
