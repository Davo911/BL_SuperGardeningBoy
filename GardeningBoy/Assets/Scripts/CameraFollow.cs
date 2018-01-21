
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float xmin;
    [SerializeField]
    private float ymin;

    private Transform target;

     void Start()
    {
        target = GameObject.Find("GardeningBoy").transform;
    }


     void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xmin, xMax), Mathf.Clamp(target.position.y, ymin, yMax),transform.position.z);
    }

}
