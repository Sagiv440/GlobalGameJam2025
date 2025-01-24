
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour , Damageable<DamageLog>
{
    public enum playerControlls
    { 
        PLAYER_ONE,
        PLAYER_TWO,
    }
    public playerControlls controlls = playerControlls.PLAYER_ONE;

    [Header("Air System")]
    [SerializeField] private PlayerController otherPlayer;
    [SerializeField] public float airAmout = 100f;
    [SerializeField] private float minSize = 2.5f;
    [SerializeField] private float maxSize = 0.5f;
    [SerializeField] private float SpeedDifference = 0.5f;


    [Header("Movement")]
    [SerializeField] private float speed = 25;

    [Header("OnDeath")]
    [SerializeField] private UnityEvent OnDeath;

    private PlayerInputSystem InputSystem;
    private Rigidbody2D rb;

    private SmartSwitch inflateSwitch;
    private Vector2 moveInput;

    // Start is called before the first frame update

    void Awake()
    {
        rb = rb == null ? GetComponent<Rigidbody2D>() : rb;
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
    [ContextMenu("Update Player Size")]
    public void UpdateSize()
    {
        InflateBubble(0f);
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

    void ReletiveVelocity()
    {
        Vector2 velocty;
        float factor = ((airAmout - otherPlayer.airAmout) / 100f);
        velocty = rb.velocity;
        if(Mathf.Abs(factor) > 0.15f)
        {
            velocty.y = factor * SpeedDifference;
        }
        rb.velocity = velocty;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.get.state == GameManager.gameState.GAME)
        {
            UpdateMoveInput();
            rb.AddForce(moveInput * speed);

            if (inflateSwitch.OnHold())
            {
                TransfareAir(50f);
            }

            ReletiveVelocity();
        }

    }

    public void OnDamage(DamageLog log)
    {
        if(log.source.tag == "Hazerd")
        {
            OnDeath.Invoke();
            GameManager.get.GameOver();
        }
    }

    public bool IsDead()
    {
        return true;
    }
}
