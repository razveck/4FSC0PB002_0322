using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool press;
    public bool jumped;
    private static InputManager _instance;
    public static InputManager Instance
    {
        get { return _instance; }
    }
    private PlayerControls playerControls;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
            _instance = this;

        playerControls = new PlayerControls();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.player.jump.performed += i=> jumped = true;
        playerControls.player.interact.performed += i => press = true;
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    public Vector2 GetPlayerMovement()
    {
        return playerControls.player.movement.ReadValue<Vector2>();
    }
    public Vector2 GetMouseDelta()
    {
        return playerControls.player.look.ReadValue<Vector2>();
    }
   public void HandleJumpInput()
    {
        if (jumped)
        {
            jumped = false;
        }
    }
    public void button_pushed()
    {
        if (press)
        {
            press = false;
        }
    }
}
