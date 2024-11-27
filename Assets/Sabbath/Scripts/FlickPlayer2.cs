using MilkShake;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FlickPlayer2 : MonoBehaviour
{
    [Header("Paramaters:")]
    public float power = 10f;
    public float penaltyTime = 5f;
    [SerializeField] private Vector2 minPower, maxPower;
    [SerializeField] private float drag;

    [Header("References")]
    private trailline tl;
    private Collider2D borderCollider;
    private Camera cam;
    private Vector3 startPoint, endPoint; // Variables for tracking drag start and end points
    private bool isDragging = false;
    private GameObject touchedObject;
    private float turnTimer;

    [Header("GameObjects:")]
    public Rigidbody2D rb;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Slider slider;

    [SerializeField] private string key;

    public Shaker MyShaker;
    public ShakePreset CameraShake;


    private void Start()
    {
        cam = Camera.main;
        tl = GetComponent<trailline>();
        borderCollider = GameObject.Find("Border").GetComponent<Collider2D>();
        turnTimer = PlayerTurnManager.Instance.GetTurnTimeLimit(2);
        tl.ClearLine(); // Clear the trail line at the beginning
    }

    private void Update()
    {
        UpdateTimerDisplay(); // Update timer display 

        if (PlayerTurnManager.Instance.currentPlayer == 2) // Check if it's player 2's turn
        {
            turnTimer -= Time.deltaTime; // Decrease turn timer

            if (Input.touchCount > 0) // Handle touch input
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPoint = GetWorldTouchPosition(touch.position);
                        touchedObject = GetTouchedObject(touch.position);
                        if (touchedObject == gameObject)
                        {
                            tl.RenderLine(startPoint, startPoint);
                            isDragging = true;
                        }
                        break;
                    case TouchPhase.Moved:
                        if (isDragging)
                        {
                            endPoint = GetWorldTouchPosition(touch.position);
                            tl.RenderLine(startPoint, endPoint);
                        }
                        break;
                    case TouchPhase.Ended:
                        if (isDragging)
                        {
                            endPoint = GetWorldTouchPosition(touch.position);
                            if (borderCollider.bounds.Contains(startPoint))
                                rb.AddForceAtPosition(CalculateForce(startPoint, endPoint), startPoint, ForceMode2D.Impulse);
                            tl.ClearLine();
                            isDragging = false;
                            PlayerTurnManager.Instance.SwitchPlayerTurn(turnTimer <= 0f, 2);
                            ResetPlayer();
                        }
                        break;
                }
            }
            if (turnTimer <= 0f) // Check if turn timer has expired
            {
                PlayerTurnManager.Instance.SwitchPlayerTurn(true, 2);
                ResetPlayer();
            }
        }
        if (rb.velocity != Vector2.zero)
        {
            Vector2 newVelocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, drag * Time.deltaTime);

            rb.velocity = newVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out FlickPlayer1 player1))
            if (player1!=null)
            MyShaker.Shake(CameraShake);
    }
    void UpdateTimerDisplay() // Update turn timer display
    {
        int minutes = Mathf.FloorToInt(turnTimer / 60);
        int seconds = Mathf.FloorToInt(turnTimer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Convert screen touch position to world position
    Vector3 GetWorldTouchPosition(Vector2 touchPosition) => cam.ScreenToWorldPoint(touchPosition) - cam.transform.position.z * Vector3.forward;

    // Calculate flicking force
    Vector2 CalculateForce(Vector3 start, Vector3 end) => Vector2.ClampMagnitude(start - end, maxPower.magnitude) * power * slider.value;

    GameObject GetTouchedObject(Vector2 touchPosition) // Get game object touched by the screen position
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(touchPosition), Vector2.zero);
        return hit.collider != null ? hit.collider.gameObject : null;
    }

    void ResetPlayer() // Reset player state after flick
    {
        isDragging = false;
        tl.ClearLine();
        turnTimer = PlayerTurnManager.Instance.GetTurnTimeLimit(2);
    }
}
