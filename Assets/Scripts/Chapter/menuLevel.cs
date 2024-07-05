// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuLevel : MonoBehaviour
{
    [SerializeField] private AudioClip clickTable;

    public void ListLevel(){
        SoundManager.instance.PlaySound(clickTable);

    }

}
