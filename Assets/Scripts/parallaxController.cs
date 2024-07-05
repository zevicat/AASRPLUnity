using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class parallaxController : MonoBehaviour
{
    Transform cam;
    Vector3 prevCamPos;
    float distance;
    GameObject[] background;
    Material[] mat;
    float[] backSpeed;
    float farthestBack;
    [Range(0.01f, 0.5f)]
    public float parallaxSpeed;

    private void Start()
    {
        cam = Camera.main.transform;
        prevCamPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        background = new GameObject[backCount];

        for(int i = 0; i < backCount; i++){
            background[i] = transform.GetChild(i).gameObject;
            mat[i] = background[i].GetComponent<Renderer>().material;
        }
        backSpeedCalculate(backCount);
    }

    private void backSpeedCalculate(int backCount){
        for(int i = 0; i < backCount; i++){
            if((background[i].transform.position.z - cam.position.z) > farthestBack){
                farthestBack = background[i].transform.position.z - cam.position.z;
            }
        }
        for (int i = 0; i < backCount; i++){
            backSpeed[i] =1 - (background[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private  void LateUpdate()
    {
        distance = cam.position.x - prevCamPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);
        for(int i = 0; i < background.Length; i ++){
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
