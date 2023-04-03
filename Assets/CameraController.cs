using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform marbleTransform;
    [SerializeField, Tooltip("The direction the camera is facing")] private Camera playerCamera;
    [SerializeField] InputManager playerInput;
    [SerializeField] private Transform gravitationalDirection;
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
        this.transform.position = GetMarblePosition();
    }   


    [Range(-5f,5f), SerializeField, Tooltip("Offset Floats for Camera Position")] private float offsetX, offsetY, offsetZ;
    private Vector3 GetMarblePosition()
    {
        this.transform.position = marbleTransform.transform.position;
        Vector3 newCameraPosition = this.transform.position;
        
        Vector3 positionOffset = new Vector3(offsetX, offsetY, offsetZ);
        return newCameraPosition + positionOffset;
    } 

    // public float xRotation, zRotation; //TEST VARIABLES. DELETE WHEN NO LONGER NEEDED

    [SerializeField, Range(0,90f)] private float negativeXClamp;
    [SerializeField, Range(0,-90f)] private float positiveXClamp;

    private float xRotation = 0f;
    /// <summary>
    /// Rotates the camera around the marble object
    /// </summary> 
    private Vector3 RotateAroundMarble()
    {
        
        //mouse X and mouse Y nomenclature is inverted
        //mouse X is left/right
        //mouse Y is up/down
        Vector2 mouseInput = playerInput.SendMouseInput();
        float mouseX = mouseInput.y;
        float mouseY = mouseInput.x;
        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, positiveXClamp, negativeXClamp);
        Vector3 clampedRotation = this.transform.eulerAngles;

        clampedRotation.x = xRotation;

        this.transform.eulerAngles = clampedRotation; //Rotate up/down
        this.transform.RotateAround(marbleTransform.transform.position, gravitationalDirection.transform.up, mouseY); //Rotate left/right



        #region Manually Rotate around X and Z axes
        
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            gravitationalDirection.transform.RotateAround(gravitationalDirection.transform.position, gravitationalDirection.transform.forward, 1f);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            gravitationalDirection.transform.RotateAround(gravitationalDirection.transform.position, gravitationalDirection.transform.forward, -1f);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            gravitationalDirection.transform.RotateAround(gravitationalDirection.transform.position, gravitationalDirection.transform.right, -1f);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            gravitationalDirection.transform.RotateAround(gravitationalDirection.transform.position, gravitationalDirection.transform.right, 1f);
        }

        
        #endregion
        //Test rotation for planetary gravity
        // this.transform.Rotate(.5f,0,0);

        return this.transform.eulerAngles;
    }

}
