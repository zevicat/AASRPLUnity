using UnityEngine;

public class snapToGrid : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Round(newPosition.x);
        newPosition.y = Mathf.Round(newPosition.y);
        transform.position = newPosition;
    }
}
