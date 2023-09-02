using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

    PlayerInputs controls;
    PlayerInputs.PlayerInputActions playerInput;
    PlayerInputs.UIInputActions UIInput;

    // Inputs
    private Vector2 movementInput;
    private Vector2 mouseInput;

    // Raycasting
    private Ray ray;
    private RaycastHit hit;

    // Booleans
    public bool isMovable;
    public bool isLeftMouseDown;
    public bool isRightMouseDown;

    #region Delegates and Events
    #region Mouse Click delegate and events
    public delegate void MouseClickHandler();
    // Left Mouse Button
    public event MouseClickHandler OnLeftMouseDown;
    public event MouseClickHandler OnLeftMouseHolding;
    public event MouseClickHandler OnLeftMouseUp;
    // Right Mouse Button
    public event MouseClickHandler OnRightMouseDown;
    public event MouseClickHandler OnRightMouseHolding;
    public event MouseClickHandler OnRightMouseUp;
    #endregion

    #region Mouse Over delegate and events
    public delegate void MouseOverHandler(RaycastHit hit);
    public event MouseOverHandler OnLeftMouseDownOver;
    public event MouseOverHandler OnLeftMouseUpOver;
    #endregion

    #region Movement delegate and events
    public delegate void MovementHandler(Vector2 dir);
    public event MovementHandler OnMove;

    // Delegate for actions like jump, use, dash, etc.
    public delegate void ActionHandler();
    public event ActionHandler OnJumpButtonPressed;
    public event ActionHandler OnJumpButtonReleased;
    public event ActionHandler OnUsing;
    #endregion
    #endregion

    // Public Properties
    public Vector2 MouseInput { get => mouseInput; }
    public Vector2 MovementInput { get => movementInput; }

    // Singleton
    private static InputManager _instance;
    public static InputManager Instance { get { return _instance; } }

    private void Awake ()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        isMovable = true;
        isLeftMouseDown = false;
        isRightMouseDown = false;

        controls = new PlayerInputs();
        playerInput = controls.PlayerInput;
        UIInput = controls.UIInput;

        // Horizontal player movement
        playerInput.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        playerInput.Movement.canceled += ctx => movementInput = Vector2.zero;

        // Updated after every mouse movement
        playerInput.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        playerInput.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        // Usage of started and canceled (KeyDown and KeyUp)
        playerInput.LeftClick.started += _ => LeftMouseButtonDown();
        playerInput.LeftClick.canceled += _ => LeftMouseButtonUp();

        playerInput.RightClick.started += _ => RightMouseButtonDown();
        playerInput.RightClick.canceled += _ => RightMouseButtonUp();

        // Usage of performed (KeyDown - same as started)
        playerInput.LeftClick.performed += _ => LeftClickPerformed();
        playerInput.RightClick.performed += _ => RightClickPerformed();

        // Has 0.4 seconds hold interaction in it
        playerInput.ShiftHold.performed += _ => isMovable = !isMovable;

        // Jump
        playerInput.Jump.started += _ => JumpStarted();
        playerInput.Jump.canceled += _ => JumpEnded();

        // Use
        playerInput.Use.performed += _ => OnUsing?.Invoke();
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Update ()
    {
        if (isMovable)
        {
            OnMove?.Invoke(movementInput);
        }
        else
        {
            // Do Something
        }

        // Mouse button holding triggers
        if (isLeftMouseDown)
        {
            OnLeftMouseHolding?.Invoke();
        }

        if(isRightMouseDown)
        {
            OnRightMouseHolding?.Invoke(); 
        }
    }

    private void JumpStarted()
    {
        if(isMovable)
        {
            OnJumpButtonPressed?.Invoke();
        }
    }
    
    private void JumpEnded()
    {
        if(isMovable)
        {
            OnJumpButtonReleased?.Invoke();
        }
    }

    private void LeftMouseButtonDown()
    {
        OnLeftMouseDown?.Invoke();
        isLeftMouseDown = true;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 50000.0f))
        {
            OnLeftMouseDownOver?.Invoke(hit);
        }
    }

    private void LeftMouseButtonUp()
    {
        OnLeftMouseUp?.Invoke();
        isLeftMouseDown = false;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 50000.0f))
        {
            OnLeftMouseUpOver?.Invoke(hit);
        }
    }

    private void RightMouseButtonDown()
    {
        OnRightMouseDown?.Invoke();
    }

    private void RightMouseButtonUp()
    {
        OnRightMouseUp?.Invoke();
    }

    private void RightClickPerformed()
    {

    }

    private void LeftClickPerformed()
    {

    }

    private void OnEnable ()
    {
        controls.Enable();
    }

    private void OnDestroy ()
    {
        controls.Disable();
    }
}