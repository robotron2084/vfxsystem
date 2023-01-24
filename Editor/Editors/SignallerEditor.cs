using GameJamStarterKit.FXSystem;
using UnityEditor;

namespace GameJamStarterKit.Fx
{
  [CustomEditor(typeof(Signaller), true)]
  public class SignallerEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      var signaller = serializedObject.targetObject as Signaller;

      EditorGUILayout.TextArea(signaller.DebugInfo());
      base.OnInspectorGUI();
      
    }
    

  }
}