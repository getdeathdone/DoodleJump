using UnityEngine;

public class CameraUp : MonoBehaviour
{
    [SerializeField] private Transform doodleTransform;
    private void Update()
    {
        if(doodleTransform)
        {
            if (doodleTransform.position.y > transform.position.y)
            {
                transform.position =
                    new Vector3(transform.position.x, doodleTransform.position.y, transform.position.z);
            }
        }
    }
}
