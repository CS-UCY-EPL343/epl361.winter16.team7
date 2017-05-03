using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using PixelCrushers.DialogueSystem;
using System.Text;
using System;

/// <summary>
/// Register lua SHA256 to use them in Dialogue Manager Lua .
/// also it registers some functions for opening links.
/// </summary>
public class registerLuaSHA256 : MonoBehaviour {
	


	void OnEnable()
	{
		
		Lua.RegisterFunction("sha256_hash", this, typeof(registerLuaSHA256).GetMethod("sha256_hash"));
		Lua.RegisterFunction("openSiteLink", this, typeof(registerLuaSHA256).GetMethod("openSiteLink"));
		Lua.RegisterFunction("openVideoLink", this, typeof(registerLuaSHA256).GetMethod("openVideoLink"));
		Lua.RegisterFunction("openVideoLink2", this, typeof(registerLuaSHA256).GetMethod("openVideoLink2"));
	}

	void OnDisable()
	{

		Lua.UnregisterFunction("sha256_hash");
		Lua.UnregisterFunction("openSiteLink");
		Lua.UnregisterFunction("openVideoLink");
		Lua.UnregisterFunction("openVideoLink2");
	}

	/// <summary>
	/// Opens the site link.
	/// </summary>
	public static void openSiteLink(){
	
		System.Diagnostics.Process.Start("http://cybersafety.pi.ac.cy/");
	
	}
	/// <summary>
	/// Opens the video link.
	/// </summary>
	public static void openVideoLink(){

		System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=r3nqwbAt624");

	}
	/// <summary>
	/// Opens the video link2.
	/// </summary>
	public static void openVideoLink2(){

		System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=hLbOOzbAfuQ");

	}
	/// <summary>
	/// Sha256s the hash.
	/// </summary>
	/// <returns>The hash.</returns>
	/// <param name="value">string to encrypt</param>
	public static string sha256_hash(string value) {
		StringBuilder Sb = new StringBuilder();

	
		using (SHA256 hash = SHA256Managed.Create()) {
			Encoding enc = Encoding.UTF8;

			Byte[] result = hash.ComputeHash(enc.GetBytes(value));

			foreach (Byte b in result)
				Sb.Append(b.ToString("x2"));
		}

		return Sb.ToString();
	}


	void Update(){
		
	}

}
