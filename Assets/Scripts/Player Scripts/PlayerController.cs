using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("The Marble Object")] private GameObject marbleObject; //The transform of the Marble Object in the Game World
    [SerializeField,Tooltip("Direction of the force the player will input")] private Transform forceDirection;

    #region Marble Components
        private Transform mTransform;
        private Rigidbody mRigidBody;
    #endregion

    #region  Player Components


    [SerializeField] private InputManager playerInput;
    
    #endregion


    [Header("Sliders")]
    [SerializeField, Range(1,10f), Tooltip("Multiplies the player input by a given value")] private float forceMultiplier;

    // Start is called before the first frame update
    
    private Vector2 horzInput;
    

    private void GetMarbleComponents()
    {
        mTransform = marbleObject.GetComponent<Transform>();
        mRigidBody = marbleObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        GetMarbleComponents();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        

        
    }

    #region Player Input

    private void MovePlayer()
    {

        SetPlayerMovementDirection();
        Jump();
        
    }

    private void SetPlayerMovementDirection()
    {
        ReceiveInput();
        Vector3 direction = SetForceEndpoint(horzInput) * forceMultiplier;
        
        mRigidBody.AddForce(direction);
        SetAngularVelocity();
    }

    [SerializeField, Range(1f,5f)] private float jumpForce;
    private bool isInAir = false;
    private void Jump()
    {
        if (playerInput.PlayerJump() == 1 && !isInAir)
        {
            mRigidBody.velocity += (forceDirection.transform.up * jumpForce);
            isInAir = true;
        }  
        
        if(isInAir)
        {
            RaycastHit groundCheck;
            if(Physics.Raycast(mTransform.transform.position, -forceDirection.transform.up, out groundCheck, Mathf.Infinity))
            {
                if(Vector3.Distance(groundCheck.point, mTransform.transform.position) <= 0.5f) isInAir = false;
                else isInAir =  true;
                
            }
        }
    }
    private Vector3 SetForceDirection(Vector3 lerpedPoint)
    {
        Vector3 fDirection = (forceDirection.transform.forward * lerpedPoint.z) + (forceDirection.transform.right * lerpedPoint.x);
        
        return fDirection;
    }


    private Vector3 lerpedEndpoint;
    [SerializeField, Range(0,1f)] private float endpointSmoothSlider;
    private Vector3 SetForceEndpoint(Vector2 inputDirection)
    {
        Vector3 _3dDirection = new Vector3(inputDirection.x, 0, inputDirection.y);

        lerpedEndpoint = Vector3.Lerp(lerpedEndpoint, _3dDirection, endpointSmoothSlider);

        Vector3 fPoint = SetForceDirection(lerpedEndpoint);
        // Debug.Log(_3dDirection);
        // Debug.Log(SetForceDirection(lerpedEndpoint));

        return fPoint;
    }

    [SerializeField] private float angularVelocityMultiplier;
    [SerializeField, Range(0, 1f)] private float angVelocitySmoothness;  
    private Vector3 lerpedDirection;
    /// <summary>
    /// Provides an apperance that the marble is drifting
    /// </summary>
    private void SetAngularVelocity()
    {
        // Vector3 vDirection = new Vector3(_direction.z, _direction.y, _direction.x);
        // mRigidBody.angularVelocity = vDirection * angularVelocityMultiplier;
        
        Vector3 vDirection = ((horzInput.y * forceDirection.transform.right) + (-horzInput.x * forceDirection.transform.forward));

        lerpedDirection = Vector3.Lerp(lerpedDirection, vDirection, angVelocitySmoothness);
        Debug.DrawRay(mTransform.transform.position, lerpedDirection, Color.green);

        mRigidBody.angularVelocity = lerpedDirection  * angularVelocityMultiplier;
    }
    

    public void ReceiveInput()
    {
        horzInput = playerInput.SetPlayerMovement();
    }



    #endregion



}
