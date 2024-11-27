using UnityEngine;

namespace PenClash.Data
{
      [CreateAssetMenu(order = 541)]
      public class PlayerData : Data
      {
            [SerializeField] private Sprite[] pens;

            public override int Current { get => base.Current; set => base.Current = (int) Mathf.Repeat(value, pens.Length); }

            public Sprite FetchPen(int index) => pens[index];
      }
}