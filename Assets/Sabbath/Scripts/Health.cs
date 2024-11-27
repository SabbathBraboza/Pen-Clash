using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//using Emp37.Utility;

[SelectionBase, DisallowMultipleComponent, RequireComponent(typeof(Collider2D))]
internal sealed class Health : MonoBehaviour, IDamageable
{
    [field: SerializeField, Space(4F)]
    public int MaxHealth { get; private set; }

    [field: SerializeField/*, Readonly*/]
    public int Value { get; private set; }

    public UnityEvent<int> OnDamage;
    public UnityEvent OnZero;

    public Slider slider;

    [SerializeField] private int damage;
    [SerializeField] private int playerId;
    public PlayerTurnManager turnManager;
    [SerializeField] private TextMeshProUGUI textDisplay;

    private Health() => MaxHealth = 100;
   
    private void OnEnable()
    {
        Value = MaxHealth;
        slider.maxValue = Value;
        slider.value = Value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int multiplier = (int)collision.relativeVelocity.magnitude;
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable) && playerId == turnManager.currentPlayer)
        {
            slider.value -= Mathf.Clamp(damage * multiplier, 0, MaxHealth);
            Value = (int) slider.value;
        }
    }

    public void Intake(int amount)
    {
        Value = Mathf.Clamp(Value - amount, 0, MaxHealth);
        if (Value == 0)
            OnZero.Invoke();
        else
            OnDamage.Invoke(amount);  
    }
}