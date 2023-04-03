using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("The Marble Object")] private GameObject marbleObject; //The transform of the Marble Object in the Game World

    #region Marble Components
        private Transform mTransform;
        private Rigidbody mRigidBody;
    #endregion

    #region  Player Components

    [SerializeField, Tooltip("Player's Main Camera")] private Camera playerCamera;
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
        // mRigidBody.AddForce(horzInput);
        // Debug.Log(horzInput);
        playerCamera.transform.LookAt(mTransform);
        MovePlayer();
        
    }

    #region Player Input

    private void MovePlayer()
    {

        SetPlayerMovementDirection();
        
    }

    private void SetPlayerMovementDirection()
    {
        ReceiveInput();
        // SetForceEndpoint(horzInput);

        // Debug.DrawLine(marbleObject.transform.position, SetForceEndpoint(horzInput));
        mRigidBody.AddForce(SetForceEndpoint(horzInput));
    }


    private Vector3 lerpedEndpoint;
    [SerializeField, Range(0,1f)] private float endpointSmoothSlider;
    private Vector3 SetForceEndpoint(Vector2 inputDirection)
    {
        Vector3 _3dDirection = new Vector3(inputDirection.x, 0, inputDirection.y);

        lerpedEndpoint = Vector3.Lerp(lerpedEndpoint, _3dDirection, endpointSmoothSlider);

        return lerpedEndpoint;
    }

    public void ReceiveInput()
    {
        horzInput = playerInput.SetPlayerMovement();
    }



    #endregion



}
