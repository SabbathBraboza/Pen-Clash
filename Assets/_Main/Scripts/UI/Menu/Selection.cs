using System;

using UnityEngine;

using Emp37.Utility;

namespace PenClash.UI.Menu
{
      using Data;

      public class Selection : Base
      {
            [Serializable]
            private struct Base
            {
                  public Data Data;
                  public Selector Selector;
            }

            [SerializeField] private Base[] players;
            [SerializeField] private Base board;

            [Title("F O N T   A N I M A T I O N")]
            [SerializeField] private Material material;
            [SerializeField] private float a, b = 1F;
            [SerializeField, Readonly] private float value;
            [SerializeField] private float speed = 1F;

            private readonly int faceDilateHash = Shader.PropertyToID("_FaceDilate");


            private void Start()
            {
                  foreach (var player in players)
                  {
                        ChangeSelection<PlayerData>(player, 0);
                  }
                  ChangeSelection<BoardData>(board, 0);
            }
            protected override void OnEnable()
            {
                  base.OnEnable();
                  foreach (var player in players)
                  {
                        player.Selector.OnNext.AddListener(() => ChangeSelection<PlayerData>(player, +1));
                        player.Selector.OnPrevious.AddListener(() => ChangeSelection<PlayerData>(player, -1));
                  }
                  board.Selector.OnNext.AddListener(() => ChangeSelection<BoardData>(board, +1));
                  board.Selector.OnPrevious.AddListener(() => ChangeSelection<BoardData>(board, -1));
            }
            private void Update()
            {
                  material.SetFloat(faceDilateHash, Mathf.PingPong(Time.time * speed, b - a) + a);
            }

            private void ChangeSelection<T>(Base player, int direction) where T : Data
            {
                  player.Data.Current += direction;

                  Type type = typeof(T);
                  Sprite data = type switch
                  {
                        _ when type == typeof(PlayerData) => (player.Data as PlayerData).FetchPen(player.Data.Current),
                        _ when type == typeof(BoardData) => (player.Data as BoardData).FetchBoard(player.Data.Current),
                        _ => null
                  };
                  player.Selector.SetInfo(data, data.name);
            }
      }
}