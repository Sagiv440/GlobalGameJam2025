#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using ScriptableObjects;

[CustomEditor(typeof(SoundEffectSO))]
public class SoundEffectsCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SoundEffectSO so = (SoundEffectSO)target;
        if (GUILayout.Button("Preview"))
        {
            so.PlayPreview();
        }
        if (GUILayout.Button("Stop"))
        {
            so.StopPreview();
        }
    }
}
#endif
