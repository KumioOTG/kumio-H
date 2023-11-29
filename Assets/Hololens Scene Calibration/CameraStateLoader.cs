using UnityEngine;

public class CameraStateLoader : MonoBehaviour
{
    void Start()
    {
        LoadCameraState();
    }

    private void LoadCameraState()
    {
        var position = new Vector3(
            PlayerPrefs.GetFloat("CameraPosX", 0),
            PlayerPrefs.GetFloat("CameraPosY", 0),
            PlayerPrefs.GetFloat("CameraPosZ", 0));

        var rotation = Quaternion.Euler(
            PlayerPrefs.GetFloat("CameraRotX", 0),
            PlayerPrefs.GetFloat("CameraRotY", 0),
            PlayerPrefs.GetFloat("CameraRotZ", 0));

        Camera.main.transform.position = position;
        Camera.main.transform.rotation = rotation;
    }
}
