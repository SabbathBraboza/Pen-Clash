    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    using MilkShake;
using PenClash.Data;

public class FlickPlayer : MonoBehaviour
    {
        [Header("-----------Parameters---------")]
        [SerializeField] private int playerID;
        [SerializeField] public float power = 10f;
        [SerializeField] public float penaltyTime = 5f;
        [SerializeField] public Vector2 minPower, maxPower;
        private Vector3 startPoint, endPoint; // Variables for tracking drag start and end points
        [SerializeField] private float drag;

        [Header("----------References:-------")]
        private bool isDragging = false;
        private GameObject touchedObject;
        private float turnTimer;
        private Camera cam;
        private Collider2D borderCollider;
       [SerializeField] private trailline tl;

        [Header("------------Gameobjects:--------")]
        public Rigidbody2D rb;
        [SerializeField] private GameObject highlight;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Slider powerMeter;

        public Shaker MyShaker;
        public ShakePreset CameraShake;

      [SerializeField] private PlayerData playerdata;
     [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            highlight = GetComponentInChildren<Highlight>(true).gameObject;
        }
        private void Start()
        {
            cam = Camera.main;
            tl = GetComponent<trailline>();
            borderCollider = GameObject.Find("Border").GetComponent<Collider2D>();
            turnTimer = PlayerTurnManager.Instance.GetTurnTimeLimit(playerID);
            tl.ClearLine();
            highlight.SetActive(false);

        UpdateSprite();
        }

        void Update()
        {
            if (PlayerTurnManager.Instance.currentPlayer == playerID)  // Check if it's player 1's turn
            {
                UpdateTimerDisplay();
                turnTimer -= Time.deltaTime;
                highlight.SetActive(true);

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
                                powerMeter.gameObject.SetActive(true);
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
                                PlayerTurnManager.Instance.SwitchPlayerTurn(turnTimer <= 0f, playerID);
                                powerMeter.gameObject.SetActive(false);
                                ResetPlayer();
                            }
                        break;
                    }
                }
                if (turnTimer <= 0f) // Check if turn timer has expired
                {
                    PlayerTurnManager.Instance.SwitchPlayerTurn(true, playerID);
                    ResetPlayer();
                }
            }
            else
               highlight.SetActive(false);
            if (rb.velocity != Vector2.zero)
            {
                Vector2 newVelocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, drag * Time.deltaTime);
                rb.velocity = newVelocity;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out FlickPlayer player))
                if (player != null)
                    MyShaker.Shake(CameraShake);
        }

        void UpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(turnTimer / 60);
            int seconds = Mathf.FloorToInt(turnTimer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        // Convert screen touch position to world position
        Vector3 GetWorldTouchPosition(Vector2 touchPosition) => cam.ScreenToWorldPoint(touchPosition) - cam.transform.position.z * Vector3.forward;

        // Calculate flicking force
        Vector2 CalculateForce(Vector3 start, Vector3 end) => Vector2.ClampMagnitude(start - end, maxPower.magnitude) * power * powerMeter.value;

        GameObject GetTouchedObject(Vector2 touchPosition)   // Get game object touched by the screen position
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(touchPosition), Vector2.zero);
            return hit.collider != null ? hit.collider.gameObject : null;
        }
        void ResetPlayer()  // Reset player state after flick
        {
            isDragging = false;
            tl.ClearLine();
            turnTimer = PlayerTurnManager.Instance.GetTurnTimeLimit(playerID);
        }
    private void UpdateSprite()
    {
        if(spriteRenderer !=null && playerdata != null)
        {
            spriteRenderer.sprite = playerdata.FetchPen(playerdata.Current);
        }
    }
    }
