using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum playerControlls
    { 
        PLAYER_ONE,
        PLAYER_TWO,
    }
    public playerControlls controlls = playerControlls.PLAYER_ONE;

    [Header("Air System")]
    [SerializeField] private PlayerController otherPlayer;
    [SerializeField] private float airAmout = 100f;
    [SerializeField] private float minSize = 2.5f;
    [SerializeField] private float maxSize = 0.5f;


    [Header("Movement")]
    [SerializeField] private float speed = 25;

    private PlayerInputSystem InputSystem;
    private Rigidbody rb;

    private SmartSwitch inflateSwitch;
    private Vector2 moveInput;

    // Start is called before the first frame update

    void Awake()
    {
        rb = rb == null ? GetComponent<Rigidbody>() : rb;
        InputSystem = new PlayerInputSystem();
    }

    void OnEnable()
    {
        if(controlls == playerControlls.PLAYER_ONE)
        {
            InputSystem.Player1.Move.Enable();
            InputSystem.Player1.Fire.Enable();
        }
        else
        {
            InputSystem.Player2.Move.Enable();
            InputSystem.Player2.Fire.Enable();

        }
        InflateBubble(0);
    }

    void OnDisable()
    {
        if (controlls == playerControlls.PLAYER_ONE)
        {
            InputSystem.Player1.Move.Disable();
            InputSystem.Player1.Fire.Disable();
        }
        else
        {
            InputSystem.Player2.Move.Disable();
            InputSystem.Player2.Fire.Disable();
        }
    }

    public void InflateBubble(float amount)
    {
        airAmout += amount;
        float relSize = maxSize - minSize;

        this.transform.localScale = (Vector3.one * minSize) +  Vector3.one * relSize * (airAmout / 100f);
    }

    private void UpdateMoveInput()
    {
        if (controlls == playerControlls.PLAYER_ONE)
        {
            moveInput = InputSystem.Player1.Move.ReadValue<Vector2>();
            inflateSwitch.Update(InputSystem.Player1.Fire.IsPressed());
        }
        else
        {
            moveInput = InputSystem.Player2.Move.ReadValue<Vector2>();
            inflateSwitch.Update(InputSystem.Player2.Fire.IsPressed());
        }
        moveInput.y = 0; 
    }

    void TransfareAir(float rate)
    {
        float amount = rate * Time.deltaTime;
        if (airAmout - amount < 0)
        {
            amount = airAmout;
        }
        otherPlayer.InflateBubble(amount);
        this.InflateBubble(-amount);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateMoveInput();
        rb.AddForce(moveInput * speed);

        if(inflateSwitch.OnHold())
        {
            TransfareAir(50f);
        }


    }
}
