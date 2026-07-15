using UnityEngine;

public class DialogueCameraFocus : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform focusTarget;
    [SerializeField] private float rotateSpeed = 5f;

    private bool _focusing = false;

    public void StartFocus()
    {
        _focusing = true;
    }

    public void StopFocus()
    {
        _focusing = false;
    }

    private void Update()
    {
        if (!_focusing) return;

        Vector3 dir = focusTarget.position - playerBody.position;
        Vector3 flatDir = new Vector3(dir.x, 0f, dir.z);

        if (flatDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(flatDir);
            playerBody.rotation = Quaternion.Slerp(
                playerBody.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }
    }
}
