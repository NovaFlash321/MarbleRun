using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerController playerMovement; 
    [SerializeField, Tooltip("Determines how fast the input is set to the maximum value")] float inputRate;
    PlayerControls controls;
    PlayerControls.MovementActions groundMovement;

    Vector2 horzInput;
    Vector2 mouseInput;
    float crouchInput;
    float jumpInput;
    float sprintInput;
    float xtimePressed, ytimePressed;
    float xInput, yInput;
    float posXInput, negXInput, posYInput, negYInput;
    float posXTimePressed, negXTimePressed, posYTimePressed, negYTimePressed;


    void Awake()
    {
        GetControls();
        
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void FixedUpdate()
    {
        
    }

    void Update()
    {
        ReceivePlayerMovement();
        ReceiveMouseMovement();
        // TestJump();

    }

    public Vector2 SetPlayerMovement()
    {
        return CombineMovement();
    }

    private void ReceivePlayerMovement()
    {
        // playerMovement.ReceiveInput(CombineMovement());
        // playerMovement.ReceiveInput(horzInput);
        // playerMovement.ReceiveJumpInput(PlayerJump());
        // playerMovement.ReceiveSprintInput(sprintInput);
        // playerMovement.ReceiveCrouchInput(crouchInput);
    }

    public Vector2 SendMouseInput()
    {
        return mouseInput;
    }
    private void ReceiveMouseMovement()
    {
        // mouseLook.ReceiveInput(mouseInput);
    }

    public float PlayerJump()
    {
        PlayerControls _con = new PlayerControls();
        PlayerControls.MovementActions ground = controls.Movement;
        if(ground.Jump.WasPressedThisFrame()) return 1;
        else return 0;
        // return ground.Jump.WasPressedThisFrame();
        // return 1;
    }


    private void GetControls()
    {
        controls = new PlayerControls();
        groundMovement = controls.Movement;

        groundMovement.Movement.performed += ctx => horzInput = ctx.ReadValue<Vector2>();

        groundMovement.HorizontalLook.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.VerticalLook.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
        


        groundMovement.Jump.performed += ctx => jumpInput = ctx.ReadValue<float>();

        groundMovement.Sprint.performed += ctx => sprintInput = ctx.ReadValue<float>();

        groundMovement.Crouch.performed += ctx => crouchInput = ctx.ReadValue<float>();
    }

    private Vector2 CombineMovement()
    {
            
        Vector2 _horzMovement = new Vector2(BreakXAxis(),BreakYAxis());
        _horzMovement = Vector2.ClampMagnitude(_horzMovement, 1);
        
        
        return _horzMovement;

    }

    private float BreakXAxis()
    {
        if(horzInput.x == 0)
        {
            posXTimePressed = SubtractTime(posXTimePressed);
            posXInput = posXTimePressed;

            negXTimePressed = SubtractTime(negXTimePressed);
            negXInput = -negXTimePressed;
        }
        else if(horzInput.x > 0)
        {
            posXTimePressed = AddTime(posXTimePressed);
            posXInput = posXTimePressed;

            negXTimePressed = SubtractTime(negXTimePressed);
            negXInput = -negXTimePressed;
        }
        else if (horzInput.x < 0)
        {
            negXTimePressed = AddTime(negXTimePressed);
            negXInput = -negXTimePressed;

            posXTimePressed = SubtractTime(posXTimePressed);
            posXInput = posXTimePressed;
        }
        return (posXInput + negXInput);
    }

    private float BreakYAxis()
    {
        if (horzInput.y == 0)
        {
            posYTimePressed = SubtractTime(posYTimePressed);
            posYInput = posYTimePressed;

            negYTimePressed = SubtractTime(negYTimePressed);
            negYInput = -negYTimePressed;
        }
        else if (horzInput.y > 0)
        {
            posYTimePressed = AddTime(posYTimePressed);
            posYInput = posYTimePressed;
        
            negYTimePressed = SubtractTime(negYTimePressed);
            negYInput = -negYTimePressed;
        }
        else if (horzInput.y < 0)
        {
            negYTimePressed = AddTime(negYTimePressed);
            negYInput = -negYTimePressed;
            
            posYTimePressed = SubtractTime(posYTimePressed);
            posYInput = posYTimePressed;
        }
        return (posYInput + negYInput);
    }
    
    private float SubtractTime(float _timePressed)
    {   
        _timePressed = Mathf.Clamp(_timePressed, 0, 1) - (Time.deltaTime * inputRate);
        if (_timePressed <= 0) _timePressed = 0f;

        return _timePressed;
    }

    private float AddTime(float _timePressed)
    {
        _timePressed = Mathf.Clamp(_timePressed, 0, 1) + (Time.deltaTime * inputRate);
        if (_timePressed >= 1) _timePressed = 1f;

        return _timePressed;
    }


}
