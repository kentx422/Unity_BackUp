using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; 

/// <summary>
/// This class provides main interface to the Ovrvision Ex
/// </summary>
public class OvrvisionEx
{
	//OVRVISION Ex Dll import
	//ovrvision_ex_csharp.cpp
	//Main system
	[DllImport("ovrvision_ex", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern void ovExSetImage(System.IntPtr pImgSrc, int w, int h);
	[DllImport("ovrvision_ex", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern void ovExRender();
	[DllImport("ovrvision_ex", CharSet=CharSet.Ansi, CallingConvention=CallingConvention.Cdecl)]
	static extern int ovExGetData(System.IntPtr mdata, int datasize);
	
	// ------ Function ------
	
	//Renderer
	public int Render(System.IntPtr pImgSrc)
	{
		float[] markerGet = new float[10];
		GCHandle marker = GCHandle.Alloc(markerGet, GCHandleType.Pinned);

		ovExSetImage (pImgSrc, 640, 480);
		ovExRender ();
		int ri = ovExGetData(marker.AddrOfPinnedObject(), 10);
		Transform gb = GameObject.Find ("OvrvisionView").transform;
		gb.localPosition = new Vector3 (markerGet[1],markerGet[2],markerGet[3]);
		gb.localRotation = new Quaternion (markerGet[4],markerGet[5],markerGet[6],markerGet[7]);
		marker.Free ();

		return ri;
	}
}
