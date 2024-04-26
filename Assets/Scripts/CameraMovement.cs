using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject target;

    void Update()
    {
        if (target == null && !WorldController.Instance.isLoading) {
            target = WorldController.Instance.GetPlayer();
        }
    }

    void LateUpdate() {
        if (target != null && target.transform.position != transform.position) {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
        }
    }
}
