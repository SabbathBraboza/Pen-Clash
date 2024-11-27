using UnityEngine;

using UnityEditor;
using UnityEditorInternal;

namespace Emp37.Utility.Editor
{
      internal static class ExtractComponent //~Warped Imagination
      {
            [MenuItem("CONTEXT/Component/Extract To Child", priority = 525)] private static void ExtractToChildMenuOption(MenuCommand command) => Extract(command, 0);
            [MenuItem("CONTEXT/Component/Extract", priority = 524)] private static void ExtractMenuOption(MenuCommand command) => Extract(command, 1);

            private static void Extract(MenuCommand command, int level)
            {
                  var source = command.context as Component;
                  var typeName = source.GetType().Name;

                  int undoGroupID = Undo.GetCurrentGroup();
                  Undo.IncrementCurrentGroup();

                  var child = new GameObject(typeName);
                  child.transform.parent = level switch { 0 => source.transform, _ => null };
                  Undo.RegisterCreatedObjectUndo(child, "Extracted object.");

                  if (!ComponentUtility.CopyComponent(source) || !ComponentUtility.PasteComponentAsNew(child))
                  {
                        Debug.LogError($"Unable to extract component '{typeName}' from object '{source.name}'.", source.gameObject);
                        Undo.CollapseUndoOperations(undoGroupID);
                        Undo.PerformUndo();
                        return;
                  }
                  Undo.DestroyObjectImmediate(source);
                  Undo.CollapseUndoOperations(undoGroupID);
            }
      }
}