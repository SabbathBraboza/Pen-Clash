using UnityEngine;
using UnityEngine.UI;

using Emp37.Utility;

namespace PenClash.UI
{
      public class Music : MonoBehaviour
      {
            [SerializeField] private Image image;
            [SerializeField] private AnimatedButton button;
            [SerializeField] private Sprite whenActive, whenInactive;
            [SerializeField] private Image icon, slash;

            [SerializeField, Readonly] private bool isActive = true;
            public bool IsActive
            {
                  get => isActive;
                  set
                  {
                        isActive = value;
                        image.sprite = isActive ? whenActive : whenInactive;
                        icon.color = isActive ? Color.white : Color.black;
                        slash.enabled = !isActive;
                  }
            }


            private void Reset()
            {
                  image = GetComponent<Image>();
                  button = GetComponent<AnimatedButton>();
            }
            private void OnEnable()
            {
                  button.OnClick.AddListener(Toggle);
            }
            private void OnDisable()
            {
                  button.OnClick.RemoveListener(Toggle);
            }

            [Button]
            private void Toggle() => IsActive = !IsActive;
      }
}