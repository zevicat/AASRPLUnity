using UnityEngine;
using UnityEngine.SceneManagement;


public class BackLogic : MonoBehaviour
{
    [SerializeField] private AudioClip clickTable;

    private void Update()
    {
        backToMainMenuEscape();
    }
    public void backToMainMenuEscape() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }
        // SoundManager.instance.PlaySound(clickTable);
    }

    public void backToMainMenuClick(){
        SceneManager.LoadScene(0);
    }
}
