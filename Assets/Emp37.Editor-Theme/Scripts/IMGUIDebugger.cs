using UnityEditor;

public static class IMGUIDebugger
{
      [MenuItem("Window/IMGUI Debugger", priority = 64)]
      public static void OpenIMGUIDebuggerWindow() => EditorWindow.GetWindow(System.Type.GetType("UnityEditor.GUIViewDebuggerWindow,UnityEditor")).Show();
}