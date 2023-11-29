using UnityEngine;

public class CameraStateButtonHandler : MonoBehaviour
{
    public void SaveCameraState()
    {
        var cameraTransform = Camera.main.transform;

        PlayerPrefs.SetFloat("CameraPosX", cameraTransform.position.x);
        PlayerPrefs.SetFloat("CameraPosY", cameraTransform.position.y);
        PlayerPrefs.SetFloat("CameraPosZ", cameraTransform.position.z);
        PlayerPrefs.SetFloat("CameraRotX", cameraTransform.eulerAngles.x);
        PlayerPrefs.SetFloat("CameraRotY", cameraTransform.eulerAngles.y);
        PlayerPrefs.SetFloat("CameraRotZ", cameraTransform.eulerAngles.z);

        PlayerPrefs.Save();
    }
}
