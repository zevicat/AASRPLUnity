using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //keep this object even when we go to new scene
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        // destroy duplicate object
        }else if(instance != null && instance != this){
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip _sound){
        audioSource.PlayOneShot(_sound);
    }
}