using UnityEngine;
using Cinemachine;

public class LockCameraPosition : MonoBehaviour
{
    public float minYLimit = 0f; // Set your minimum Y limit here

    private void LateUpdate()
    {
        // Assuming your virtual camera is a child of this GameObject
        CinemachineVirtualCamera virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();

        if (virtualCamera != null)
        {
            // Get the current position of the virtual camera
            Vector3 currentPosition = virtualCamera.transform.position;

            // Limit the Y position to minYLimit
            currentPosition.y = Mathf.Max(currentPosition.y, minYLimit);

            // Set the updated position back to the virtual camera
            virtualCamera.transform.position = currentPosition;
        }
    }
}
