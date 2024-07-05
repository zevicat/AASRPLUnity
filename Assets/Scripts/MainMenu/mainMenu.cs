using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip clickTable;
    // Start is called before the first frame update
    public void StartGame(){
        SoundManager.instance.PlaySound(clickTable);
        SceneManager.LoadScene(1);
    }
   public void QuitGame(){
        Application.Quit();
   }

   public void ClickTable(){
        SoundManager.instance.PlaySound(clickTable);
   }
}
