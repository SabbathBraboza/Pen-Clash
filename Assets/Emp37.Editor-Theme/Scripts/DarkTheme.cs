namespace Emp37.ET
{
      [UnityEngine.CreateAssetMenu(menuName = "Editor-Theme/New Dark Theme", fileName = "New Dark Theme", order = 50)]
      internal class DarkTheme : BaseTheme
      {
            public override string FileName => "Dark.uss";
            protected override bool IsSkinInvalid => !UnityEditor.EditorGUIUtility.isProSkin;
      }
}