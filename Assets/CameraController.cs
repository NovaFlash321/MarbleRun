using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform marbleTransform;
    [SerializeField, Tooltip("The direction the camera is facing")] private Camera playerCamera;
    [SerializeField] InputManager playerInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        SetCameraPositionAndRotation();
    }
        

    private void SetCameraPositionAndRotation()
    {
        playerCamera.transform.position = GetMarblePosition();
        RotateAroundMarble();
    }   


    [Range(-5f,5f), SerializeField, Tooltip("Offset Floats for Camera Position")] private float offsetX, offsetY, offsetZ;
    private Vector3 GetMarblePosition()
    {
        this.transform.position = marbleTransform.transform.position;
        Vector3 newCameraPosition = this.transform.position;
        
        Vector3 positionOffset = new Vector3(offsetX, offsetY, offsetZ);
        return newCameraPosition + positionOffset;
    } 

    public float xRotation, zRotation;

    /// <summary>
    /// Rotates the camera around the marble object
    /// </summary>    
    private void RotateAroundMarble()
    {
        
        //mouse X and mouse Y nomenclature is inverted
        //mouse X is left/right
        //mouse Y is up/down
        Vector2 mouseInput = playerInput.SendMouseInput();
        float mouseX = mouseInput.y;
        float mouseY = mouseInput.x;

        // Vector3 newRotation = new Vector3(xRotation, this.transform.eulerAngles.y, zRotation);
        // newRotation.y += mouseY;
        // this.transform.eulerAngles = newRotation;
        // this.transform.rotation = Quaternion.Euler(xRotation, this.transform.rotation.y, zRotation);
        this.transform.RotateAround(this.transform.position, this.transform.up, mouseY);
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.RotateAround(this.transform.position, this.transform.forward, 1f);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.RotateAround(this.transform.position, this.transform.forward, -1f);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.RotateAround(this.transform.position, this.transform.right, -1f);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.RotateAround(this.transform.position, this.transform.right, 1f);
        }
        //Test rotation for planetary gravity
        // this.transform.Rotate(.5f,0,0);
    }

}
