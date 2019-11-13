using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3 : MonoBehaviour
{
    public float speed;
    public float sprintModifier;
    public float JumpForce;
    public Camera normalCam;

    public Transform weaponParent;
    private Rigidbody rig;

    public Vector3 targetBobPosition;
    private Vector3 weaponParentOrigin;
    public float movementCounter;
    public float idleCounter;
    private float basefOV;
    public float sprintFOVModifier = 1.5f;

    private void Start()
    {
        basefOV = normalCam.fieldOfView;
        Camera.main.enabled = false;
        rig = GetComponent<Rigidbody>();
        weaponParentOrigin = weaponParent.localPosition;
    }
    private void Update()
    {
        // if (!photonView.IsMine) return;
        {
            bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            bool jump = Input.GetKeyDown(KeyCode.Space);
        }
    }
    void FixedUpdate()
    {
        //  if (!photonView.IsMine) return;
        //Axis
        float t_hmove = Input.GetAxis("Horizontal");
        float t_vmove = Input.GetAxis("Vertical");
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);
        //States
        bool isJumping = jump;
        bool isSprinting = sprint && t_vmove > 0;
        //Jumping
        if (isJumping)
            rig.AddForce(Vector3.up * JumpForce);
        //Head bob
        if (t_hmove == 0 && t_vmove == 0)
        {
            Headbob(idleCounter, 0.01f, 0.01f); idleCounter += Time.deltaTime;
        }
        else
        {
            Headbob(movementCounter, 0.01f, 0.01f); movementCounter += Time.deltaTime * 0.01f;
        }
        weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetBobPosition, Time.deltaTime * 0.01f);
        //Movement
        Vector3 t_direction = new Vector3(t_hmove, 0, t_vmove);
        t_direction.Normalize();
        float t_adjustedSpeed = speed;
        if (isSprinting) t_adjustedSpeed *= sprintModifier;
        Vector3 t_targetVelocity = transform.TransformDirection(t_direction) * t_adjustedSpeed * Time.deltaTime;
        t_targetVelocity.y = rig.velocity.y;
        //Field of View
        if (isSprinting)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, basefOV * sprintFOVModifier, Time.deltaTime * 1f);
        }
        else
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, basefOV, Time.deltaTime);
        }
    }
    void Headbob(float p_z, float p_x_intensity, float p_y_intensity)
    {
        targetBobPosition = weaponParent.localPosition = weaponParentOrigin + new Vector3(Mathf.Cos(p_z * 2) * (p_x_intensity), Mathf.Sin(p_z * 2) * p_y_intensity, weaponParentOrigin.z);
    }
}

