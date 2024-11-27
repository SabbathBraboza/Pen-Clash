using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using UnityEditor;
using UnityEditor.Compilation;
using UnityEditorInternal;

namespace Emp37.ET
{
      using static EditorStyle;


      internal abstract class BaseTheme : ScriptableObject
      {
            public const string DIRECTORY = "Assets/Emp37.Editor-Theme/StyleSheets/Extensions";


            public EditorStyleGroup[] Rules;

            [Tooltip("Enables immediate preview of changes applied.\n\n<b>NOTE:</b> some changed may only take effect after the next domain reload.")]
            [SerializeField] private bool _quickApply;

            public abstract string FileName { get; }
            protected abstract bool IsSkinInvalid { get; }
            private string Content
            {
                  get
                  {
                        try // averts newly created theme asset from causing exceptions until a repaint request is made.
                        {
                              var output = new StringBuilder("/*Theme: " + name + "*/\n");
                              Array.ForEach(Rules, rule => Array.ForEach(rule.Styles, style => output.Append(value: SubContent(in style) + '\n')));
                              return output.ToString();
                        }
                        catch (NullReferenceException)
                        {
                              return string.Empty;
                        }
                  }
            }

            private string SubContent(in EditorStyle style)
            {
                  var output = new StringBuilder();

                  #region C L A S S   T Y P E S 
                  foreach (var type in style.Types.Where(type => !string.IsNullOrEmpty(type)))
                  {
                        foreach (var context in style.States)
                        {
                              var pseudoChain = context is 0 ? string.Empty : ':' + string.Join(':', from state in Enum.GetValues(typeof(PseudoState)).Cast<PseudoState>() where context.HasFlag(state) select state.ToString().ToLower());

                              output.Append($"\n.{type + pseudoChain},");
                        }
                  }
                  #endregion

                  #region S C O P E
                  var scope = new Queue<string>();
                  foreach (var option in Properties)
                  {
                        if ((style.Flags & option.Key) == 0) continue;
                        var expression = option.Key switch
                        {
                              1 << 0 => $"{(style.background_image == null ? "none" : $"resource(\"{AssetDatabase.GetAssetPath(style.background_image)}\")")}",
                              1 << 1 => ToUSS(style.background_color),
                              1 << 2 => ToUSS(style.border_color),
                              1 << 3 => ToUSS(style.border_top_color),
                              1 << 4 => ToUSS(style.border_right_color),
                              1 << 5 => ToUSS(style.border_bottom_color),
                              1 << 6 => ToUSS(style.border_left_color),
                              1 << 7 => $"{style.border_radius.top:0}px {style.border_radius.right:0}px {style.border_radius.bottom:0}px {style.border_radius.left:0}px",
                              1 << 8 => $"{style.border_width.top:0}px {style.border_width.right:0}px {style.border_width.bottom:0}px {style.border_width.left:0}px",
                              1 << 9 => ToUSS(style.color),
                              _ => null,
                        };
                        scope.Enqueue($"\t{option.Value.Replace('_', '-')}: {expression};");
                  }

                  static string ToUSS(Color32 color) => $"rgba({color.r:000}, {color.g:000}, {color.b:000}, {color.a / 255F})";
                  #endregion

                  return output.Append(string.Concat(" {\n", string.Join("\n", scope), "\n}")).ToString();
            }
            public void Refresh()
            {
                  if (IsSkinInvalid)
                  {
                        InternalEditorUtility.SwitchSkinAndRepaintAllViews();
                        return;
                  }
                  InternalEditorUtility.RepaintAllViews();
                  if (!_quickApply) CompilationPipeline.RequestScriptCompilation();
            }

            /// <summary>
            /// Generates a finalised USS code representation for this theme.
            /// </summary>
            public sealed override string ToString() => Content;
      }
}