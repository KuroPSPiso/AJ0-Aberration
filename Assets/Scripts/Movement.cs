using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float MovementSpeed;
    public bool IsOnFloor; //custom floor detection for KZ
    public float AirStrafe;
    public bool InvertCameraY;

    private Rigidbody rigidBody;
    private Camera playerCamera;

    private float cameraLookY;
    private float cameraLookX;
    private Vector2 movementDir;
    private Vector2 lookDir;


    void Start()
    {
        this.rigidBody = this.gameObject.GetComponent<Rigidbody>();
        this.playerCamera = Camera.main;
        this.cameraLookY = 90; //center view
        this.cameraLookX = this.transform.rotation.eulerAngles.x;
    }

    void Update()
    {
        this.SetCameraLookPosition();
    }

    void FixedUpdate()
    {
        this.HandleInput();
    }

    //Legacy Input
    void HandleInput()
    {
        InputSettings inputSettings;

        if (!GameManager.TryGetInputSettings(out inputSettings)) return;

        float inX = Input.GetAxis("Horizontal");
        float inY = Input.GetAxis("Vertical");

        if (inX != 0 || inY != 0)
        {
            this.movementDir = new Vector2(inX, inY).normalized; //for this style of game to normalize?
        }

        float lookX = Input.GetAxis("Mouse X");
        float lookY = Input.GetAxis("Mouse Y");

        if (lookX != 0 || lookY != 0)
        {
            this.cameraLookX += lookX * inputSettings.InputXMultiplier;
            this.cameraLookY += lookY * inputSettings.InputYMultiplier * ((!this.InvertCameraY) ? -1f : 1f);

            this.cameraLookX %= 360;

            if (this.cameraLookY < 0) this.cameraLookY = 0;
            if (this.cameraLookY > 180) this.cameraLookY = 180;

            this.lookDir = new Vector2(this.cameraLookX, this.cameraLookY - 90);
        }

    }

    void SetCameraLookPosition()
    {
        this.playerCamera.transform.eulerAngles = new Vector3(
                this.lookDir.y,
                this.playerCamera.transform.eulerAngles.y,
                this.playerCamera.transform.eulerAngles.z
            );

        this.transform.eulerAngles = new Vector3(

                this.transform.transform.eulerAngles.x,
                this.lookDir.x,
                this.transform.transform.eulerAngles.z
            );
    }
}
