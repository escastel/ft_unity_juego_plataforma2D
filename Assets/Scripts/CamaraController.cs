using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -1);
    }
}