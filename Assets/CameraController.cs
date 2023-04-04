using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform marbleTransform;
    [SerializeField, Tooltip("The direction the camera is facing")] private Camera playerCamera;
    [SerializeField] InputManager playerInput;
    [SerializeField] private Transform gravitationalDirection;
    [SerializeField, Tooltip("Rotates the force direction on the Y axis")] private Transform forceDirection;
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
        RotateAroundMarble();
        gravitationalDirection.transform.position = GetMarblePosition();
    }   


    [Range(-5f,5f), SerializeField, Tooltip("Offset Floats for Camera Position")] private float offsetX, offsetY, offsetZ;
    private Vector3 GetMarblePosition()
    {
        // this.transform.position = marbleTransform.transform.position;
        // Vector3 newCameraPosition = this.transform.position;
        
        // Vector3 positionOffset = new Vector3(offsetX, offsetY, offsetZ);
        // return newCameraPosition + positionOffset;
        Vector3 marblePos = marbleTransform.transform.position;
        return marblePos;
    } 

    // public float xRotation, zRotation; //TEST VARIABLES. DELETE WHEN NO LONGER NEEDED

    [SerializeField, Range(0,90f)] private float negativeXClamp;
    [SerializeField, Range(0,-90f)] private float positiveXClamp;

    private float xRotation = 0f;
    private float yRotation = 0f;
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
        xRotation -= mouseX;
        yRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, positiveXClamp, negativeXClamp);
        Vector3 clampedRotation = this.transform.localRotation.eulerAngles;

        clampedRotation.x = xRotation;
        clampedRotation.y = yRotation;
        this.transform.localRotation = Quaternion.Euler(clampedRotation); //Rotate up/down
        // this.transform.RotateAround(gravitationalDirection.transform.position, gravitationalDirection.transform.up, mouseY); //Rotate left/right

        Vector3 forceRotation = new Vector3(0,clampedRotation.y,0);
        forceDirection.transform.localRotation=  Quaternion.Euler(forceRotation);



        
        //Test rotation for planetary gravity
        // this.transform.Rotate(.5f,0,0);

        // return this.transform.eulerAngles;
        // Debug.Log(this.transform.rotation);
    }

    public float GetForceYDirection()
    {
        return forceDirection.transform.localRotation.y;
    }

}
