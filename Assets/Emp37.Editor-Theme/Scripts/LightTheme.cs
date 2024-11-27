namespace Emp37.ET
{
      [UnityEngine.CreateAssetMenu(menuName = "Editor-Theme/New Light Theme", fileName = "New Light Theme", order = 49)]
      internal class LightTheme : BaseTheme
      {
            public override string FileName => "Light.uss";
            protected override bool IsSkinInvalid => UnityEditor.EditorGUIUtility.isProSkin;
      }
}