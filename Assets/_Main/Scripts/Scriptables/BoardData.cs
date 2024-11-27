using UnityEngine;

namespace PenClash.Data
{
      [CreateAssetMenu(order = 542)]
      public class BoardData : Data
      {
            [SerializeField] private Sprite[] pool;

            public override int Current { get => base.Current; set => base.Current = (int) Mathf.Repeat(value, pool.Length); }

            public Sprite FetchBoard(int index) => pool[index];
      }
}