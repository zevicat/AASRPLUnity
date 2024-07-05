using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    //Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    //Follow Player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float aheadDistanceTop;
    [SerializeField] private float cameraSpeedTop;
    private float lookAheadTop;
    private float lookAhead;
    private  void Update()
    {
        // follow player in top
        if(player.position.y >= 3.5f){
            lookAheadTop = Mathf.Lerp(lookAheadTop, aheadDistanceTop * player.localScale.y, Time.deltaTime * cameraSpeedTop);
            transform.position = new Vector3(player.position.x, player.position.y - lookAheadTop , transform.position.z);
            // lookAhead = Mathf.Lerp(lookAhead, aheadDistance * player.localScale.y, Time.deltaTime * cameraSpeed);
            // transform.position = new Vector3(player.position.x, player.position.y - lookAhead, transform.position.z);
        }else{
        // Follow Player
            lookAheadTop = Mathf.Lerp(lookAheadTop, aheadDistanceTop * player.localScale.y, Time.deltaTime * cameraSpeedTop);
            lookAhead = Mathf.Lerp(lookAhead, aheadDistance * player.localScale.x , Time.deltaTime * cameraSpeed);
            transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        }
    }
    public void MoveToNewRoom(Transform _newRoom){
        currentPosX = _newRoom.position.x;
    }
}
