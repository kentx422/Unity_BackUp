using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; 

/// <summary>
/// This class provides main interface to the Ovrvision
/// </summary>
public class Ovrvision : MonoBehaviour
{
	//OVRVISION Dll import
	//ovrvision_csharp.cpp
	//Main system
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovOpen(int locationID);
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovClose();
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern void ovGetCamImage(System.IntPtr img, int eye);
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern void ovGetCamImageForUnityColor32(System.IntPtr pImagePtr_Left, System.IntPtr pImagePtr_Right, System.IntPtr pImagePtr_LeftUndistort);
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetPixelSize();
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetBufferSize();
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetImageWidth();
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetImageHeight();
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetImageRate();

	//Set camera propartys
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern void ovSetExposure(int value);
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern void ovSetWhiteBalance(int value);
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern void ovSetContrast(int value);
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern void ovSetSaturation(int value);
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern void ovSetBrightness(int value);
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern void ovSetSharpness(int value);
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern void ovSetGamma(int value);
	//Get camera propartys
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetExposure();
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetWhiteBalance();
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetContrast();
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetSaturation();
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetBrightness();
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetSharpness();
	[DllImport("ovrvision", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovGetGamma();

	//camera select define
	private const int OV_CAMEYE_LEFT = 0;
	private const int OV_CAMEYE_RIGHT = 1;
	private const int OV_SET_AUTOMODE = (-1);

	//Camera GameObject
	private Camera go_cameraLeft;
	private Camera go_cameraRight;
	private GameObject go_cameraPlaneLeft;
	private GameObject go_cameraPlaneRight;
	//Camera texture
	private Texture2D go_CamTexLeft;
	private Texture2D go_CamTexRight;
	private Color32[] go_pixelsColorLeft;
	private Color32[] go_pixelsColorRight;
	private GCHandle go_pixelsHandleLeft;
	private GCHandle go_pixelsHandleRight;
	private System.IntPtr go_pixelsPointerLeft = System.IntPtr.Zero;
	private System.IntPtr go_pixelsPointerRight = System.IntPtr.Zero;
	
	//public setting var
	//Camera status
	public bool camStatus;

	// ------ Function ------

	// Use this for initialization
	void Awake() {
		//Open camera
		if (ovOpen (0) == 0) {
			camStatus = true;
		} else {
			camStatus = false;
			Debug.LogError ("Ovrvision open error!!");
		}

	}

	// Use this for initialization
	void Start()
	{
		// initialize camera plane object(Left)
		go_cameraLeft = transform.FindChild ("DeviceCameraLeft").camera;
		go_cameraPlaneLeft = transform.FindChild ("DeviceCameraLeft").FindChild("CameraPlane").gameObject;
		go_cameraPlaneLeft.renderer.material.shader = Shader.Find ("ovTexture");
		go_cameraPlaneLeft.transform.localPosition = new Vector3 (1.0f, 0.0f, 1.0f);	//Default
		go_cameraPlaneLeft.transform.localScale = new Vector3 (-1.0f, 1.0f, 0.75f);
		// initialize camera plane object(Right)
		go_cameraRight = transform.FindChild ("DeviceCameraRight").camera;
		go_cameraPlaneRight = transform.FindChild ("DeviceCameraRight").FindChild("CameraPlane").gameObject;
		go_cameraPlaneRight.renderer.material.shader = Shader.Find ("ovTexture");
		go_cameraPlaneRight.transform.localPosition = new Vector3 (-1.0f, 0.0f, 1.0f);
		go_cameraPlaneRight.transform.localScale = new Vector3 (-1.0f, 1.0f, 0.75f);

		//Setting cameras
		go_cameraLeft.transform.position = Vector3.zero;
		go_cameraLeft.transform.rotation = Quaternion.identity;
		go_cameraLeft.orthographicSize = 7.0f;
		go_cameraRight.transform.position = Vector3.zero;
		go_cameraRight.transform.rotation = Quaternion.identity;
		go_cameraRight.orthographicSize = 7.0f;

		//Create cam texture
		go_CamTexLeft = new Texture2D(ovGetImageWidth(), ovGetImageHeight(), TextureFormat.RGB24, false);
		go_CamTexRight = new Texture2D(ovGetImageWidth(), ovGetImageHeight(), TextureFormat.RGB24, false);
		//Cam setting
		go_CamTexLeft.wrapMode = TextureWrapMode.Clamp;
		go_CamTexRight.wrapMode = TextureWrapMode.Clamp;

		if (!camStatus)
			return;

		//Camera open only

		//Get texture pointer
		go_pixelsColorLeft = go_CamTexLeft.GetPixels32();
		go_pixelsColorRight = go_CamTexRight.GetPixels32();
		go_pixelsHandleLeft = GCHandle.Alloc(go_pixelsColorLeft, GCHandleType.Pinned);
		go_pixelsHandleRight = GCHandle.Alloc(go_pixelsColorRight, GCHandleType.Pinned);
		go_pixelsPointerLeft = go_pixelsHandleLeft.AddrOfPinnedObject();
		go_pixelsPointerRight = go_pixelsHandleRight.AddrOfPinnedObject();

		go_cameraPlaneLeft.renderer.material.mainTexture = go_CamTexLeft;
		go_cameraPlaneRight.renderer.material.mainTexture = go_CamTexRight;
	}

	// Update is called once per frame
	void Update ()
	{
		//camStatus
		if (!camStatus)
			return;

		if (go_CamTexLeft == null || go_CamTexRight == null)
			return;

		//Get the camera image.
		ovGetCamImageForUnityColor32 (go_pixelsPointerLeft, go_pixelsPointerRight, System.IntPtr.Zero);

		//Apply
		go_CamTexLeft.SetPixels32(go_pixelsColorLeft);
		go_CamTexLeft.Apply();
		go_CamTexRight.SetPixels32(go_pixelsColorRight);
		go_CamTexRight.Apply();

		//Key Input
		CameraViewKeySetting ();
	}

	//GUI view
	void OnGUI() {

		//Error
		if (!camStatus) {
			GUIStyle guiStyle = new GUIStyle();
			guiStyle.normal.textColor = Color.red;	//error color
			//ovrvision not found.
			GUI.Label (new Rect (20, 20, 300, 40), "[Error] Ovrvision not found.", guiStyle);
		}
	}
	
	//CameraViewKeySetting method
	void CameraViewKeySetting()
	{
		//Camera View Setting
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			go_cameraLeft.orthographicSize -= 0.1f;
			go_cameraRight.orthographicSize -= 0.1f;
		}
		
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			go_cameraLeft.orthographicSize += 0.1f;
			go_cameraRight.orthographicSize += 0.1f;
		}
		
		//Camera View Setting
		if (Input.GetKeyDown (KeyCode.Z)) {
			go_cameraPlaneRight.transform.localPosition += new Vector3(0.0f,0.05f,0.0f);
			go_cameraPlaneLeft.transform.localPosition += new Vector3(0.0f,-0.05f,0.0f);
		}
		
		if (Input.GetKeyDown (KeyCode.X)) {
			go_cameraPlaneRight.transform.localPosition += new Vector3(0.0f,-0.05f,0.0f);
			go_cameraPlaneLeft.transform.localPosition += new Vector3(0.0f,0.05f,0.0f);
		}
		
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			go_cameraPlaneRight.transform.localPosition += new Vector3(0.1f,0.0f,0.0f);
			go_cameraPlaneLeft.transform.localPosition += new Vector3(-0.1f,0.0f,0.0f);
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			go_cameraPlaneRight.transform.localPosition += new Vector3(-0.1f,0.0f,0.0f);
			go_cameraPlaneLeft.transform.localPosition += new Vector3(0.1f,0.0f,0.0f);
		}
	}

	// Quit
	void OnApplicationQuit()
	{
		if (!camStatus)
			return;

		//Close camera
		if(ovClose () != 0)
			Debug.LogError ("Ovrvision close error!!");

		//free
		go_pixelsHandleLeft.Free ();
		go_pixelsHandleRight.Free ();
	}

	//Public methods.
	//UpdateOvrvisionSetting method
	public void UpdateOvrvisionSetting(OvrvisionProperty prop)
	{
		if (!camStatus)
			return;

		ovSetExposure (prop.exposure);
		ovSetWhiteBalance (prop.whitebalance);
		ovSetContrast (prop.contrast);
		ovSetSaturation (prop.saturation);
		ovSetBrightness (prop.brightness);
		ovSetSharpness (prop.sharpness);
		ovSetGamma (prop.gamma);
	}
}
