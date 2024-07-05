using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public class SecurityUnimma : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform rect;
    private int currentCPosition;
    private void Awake() {
        rect = GetComponent<RectTransform>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            ChangePosition(-1);
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            ChangePosition(1);
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter)){
            Interact(); 
        }
    }

    private void ChangePosition(int _change){
        currentCPosition += _change;
        if(_change != 0){
            SoundManager.instance.PlaySound(changeSound);
        }

        if(currentCPosition < 0){
            currentCPosition = options.Length - 1;
        }else if(currentCPosition > options.Length - 1){
            currentCPosition = 0;
        }
         
        rect.position = new Vector3(rect.position.x, options[currentCPosition].position.y, 0);
    }

    private void Interact(){
        SoundManager.instance.PlaySound(interactSound);

        options[currentCPosition].GetComponent<Button>().onClick.Invoke();
    }
}
