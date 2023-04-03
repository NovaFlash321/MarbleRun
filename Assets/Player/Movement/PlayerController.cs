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
    #endregion

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

        mRigidBody.AddForce(horzInput);
        // Debug.Log(horzInput);
        playerCamera.transform.LookAt(mTransform);
        Debug.DrawRay(mTransform.position, horzInput * 10f, Color.green);
        
    }

    #region Player Input

    public void ReceiveInput(Vector2 _input)
    {
        horzInput = _input;
    }



    #endregion



}
