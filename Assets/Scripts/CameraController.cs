using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using InputSystem;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera camera;

    public static CameraController main { get; private set; }

    private Transform mainTarget;

    bool wideFocusing = false;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (main != null && main != this)
        {
            Destroy(this);
        }
        else
        {
            main = this;
        }
        camera = GetComponent<CinemachineVirtualCamera>();
        
    }

    public void SetFocus(Transform target)
    {
        mainTarget = target;
        if (wideFocusing) return;
        camera.Follow = target;
    }

    private void OnEnable()
    {
        GlobalGame.Instance.inputSystem.Camera += WideFocus;
        if(!GlobalGame.Instance.inputSystem) Debug.Log("not");
    }

    public void WideFocus()
    {
        wideFocusing = !wideFocusing;
        if (wideFocusing)
        {
            camera.Follow = null;
            transform.position = new Vector3(0, 8.69f, -16);
            return;
        }

        SetFocus(mainTarget);
        
    }

    public void QuickWideFocus()
    {
        camera.Follow = null;
        transform.position = new Vector3(0, 8.69f, -16);

    }

    public void QuickFocus(Transform target, float time)
    {
        camera.Follow = target;
       // Time.timeScale = .5f;
        StartCoroutine(QuickFocusRoutine(time));
    }

    public void QuickFocus(Transform target)
    {
        camera.Follow = target;
    }

    public void ResetFocus()
    {
        camera.Follow = mainTarget;
    }

    IEnumerator QuickFocusRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        if (wideFocusing)
        {
            camera.Follow = null;
            transform.position = new Vector3(0, 8.69f, -16);
        }
        else ResetFocus();
       // Time.timeScale = 1;

    }
}
