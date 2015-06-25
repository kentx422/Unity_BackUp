using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// Ovrvision Custom Editor
/// </summary>
[CustomEditor( typeof(Ovrvision) )]
public class OvrvisionEditor : Editor { 

	private const int OV_SET_AUTOMODE = (-1);

	public override void OnInspectorGUI() {
		OvrvisionProperty resultProp = new OvrvisionProperty();
		Ovrvision obj = target as Ovrvision;

		EditorGUILayout.LabelField( "Ovrvision Status" );
		if(obj.camStatus)
			EditorGUILayout.HelpBox( "Opened", MessageType.Info, true );
		else
			EditorGUILayout.HelpBox( "Closed", MessageType.Error, true );

		EditorGUILayout.Space();

		EditorGUILayout.LabelField( "Ovrvision Setting" );
		if (EditorGUILayout.Toggle ("Exposure Automatic", resultProp.exposure < 0) == false)
			resultProp.exposure = EditorGUILayout.IntSlider ("Exposure level", resultProp.exposure, 0, 5);
		else
			resultProp.exposure = OV_SET_AUTOMODE;	//Automode

		if (EditorGUILayout.Toggle ("Color temp Automatic", resultProp.whitebalance < 0) == false)
			resultProp.whitebalance = EditorGUILayout.IntSlider( "Color temperature", resultProp.whitebalance, 2800, 6500 );
		else
			resultProp.whitebalance = OV_SET_AUTOMODE;	//Automode

		resultProp.contrast = EditorGUILayout.IntSlider( "Contrast", resultProp.contrast, 0, 127 );
		resultProp.saturation = EditorGUILayout.IntSlider( "Saturation", resultProp.saturation, 0, 127 );
		resultProp.brightness = EditorGUILayout.IntSlider( "Brightness", resultProp.brightness, 0, 255 );
		resultProp.sharpness = EditorGUILayout.IntSlider( "Sharpness", resultProp.sharpness, 0, 15 );
		resultProp.gamma = EditorGUILayout.IntSlider( "Gamma", resultProp.gamma, 0, 10 );

		EditorUtility.SetDirty( target );	//editor set
		//changed param
		if (GUI.changed) {
			obj.UpdateOvrvisionSetting(resultProp);	//apply
			EditorUtility.SetDirty( target );	//editor set
		}
	}
}