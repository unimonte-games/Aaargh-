using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraP3 : MonoBehaviourPun, IPunObservable
{
    public static bool cursorLocked = true;

    public Transform player;
    public Transform cam;

    public float xSensitivity = 10f;
    public float ySensitivity = 10f;
    public float maxAngle = 90f;

    private Quaternion camCenter;
    void Start()
    {
        camCenter = cam.localRotation;
    }
    void Update()
    {
        //if (photonView.IsMine)
        //{
        //    SetY();
        //    SetX();
        //    UpdateCursorLock();
        //}
        if(checked(photonView.IsMine))
        {
            SetY();
            SetX();
            UpdateCursorLock();
        }
    }
    void SetY()
    {
        float t_input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);
        Quaternion t_delta = cam.localRotation * t_adj;

        if (Quaternion.Angle(camCenter, t_delta) < maxAngle)
        {
            cam.localRotation = t_delta;
        }
    }
    void SetX()
    {
        float t_input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.down);
        Quaternion t_delta = player.localRotation * t_adj;
        player.localRotation = t_delta;
    }
    void UpdateCursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = true;
            }
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
    }
}
