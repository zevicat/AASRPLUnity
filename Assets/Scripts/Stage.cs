using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    private void Update()
    {
        // StageOne();
    }

    public void StageOne(){
        SceneManager.LoadScene(2);
    }
    public void StageTwo(){
        SceneManager.LoadScene(3);
    }
}
