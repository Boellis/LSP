#pragma warning disable 0219

using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MiniJSON;

public class NativeToolkit : MonoBehaviour {

	enum ImageType { IMAGE, SCREENSHOT };
	enum SaveStatus { NOTSAVED, SAVED, DENIED, TIMEOUT };

	public static event Action<Texture2D> OnScreenshotTaken;
	public static event Action<string> OnScreenshotSaved;
	public static event Action<string> OnImageSaved;
	public static event Action<Texture2D, string> OnImagePicked;
	public static event Action<bool> OnDialogComplete;
	public static event Action<string> OnRateComplete;
	public static event Action<Texture2D, string> OnCameraShotComplete;
	public static event Action<string, string, string> OnContactPicked;
	
	static NativeToolkit instance = null;
	static GameObject go; 
	
	#if UNITY_IOS
	
	[DllImport("__Internal")]
	private static extern int saveToGallery(string path);

	[DllImport("__Internal")]
	private static extern void pickImage();

	[DllImport("__Internal")]
	private static extern void openCamera();

	//[DllImport("__Internal")]
	//private static extern void pickContact();

	//[DllImport("__Internal")]
	//private static extern string getLocale();

	[DllImport("__Internal")]
	private static extern void sendEmail(string to, string cc, string bcc, string subject, string body, string imagePath);

	//[DllImport("__Internal")]
	//private static extern void scheduleLocalNotification(string id, string title, string message, int delayInMinutes, string sound);

	//[DllImport("__Internal")]
	//private static extern void clearLocalNotification(string id);

	//[DllImport("__Internal")]
	//private static extern void clearAllLocalNotifications();

	//[DllImport("__Internal")]
	//private static extern bool wasLaunchedFromNotification();

	//[DllImport("__Internal")]
	//private static extern void rateApp(string title, string message, string positiveBtnText, string neutralBtnText, string negativeBtnText, string appleId);

	//[DllImport("__Internal")]
	//private static extern void showConfirm(string title, string message, string positiveBtnText, string negativeBtnText);

	//[DllImport("__Internal")]
	//private static extern void showAlert(string title, string message, string confirmBtnText);

    //[DllImport("__Internal")]
    //private static extern void startLocation();

    //[DllImport("__Internal")]
    //private static extern double getLongitude();

    //[DllImport("__Internal")]
    //Private static extern double getLatitude();

#elif UNITY_ANDROID

	static AndroidJavaClass obj;

#endif


    //=============================================================================
    // Init singleton
    //=============================================================================

    public static NativeToolkit Instance 
	{
		get {
			if(instance == null)
			{
				go = new GameObject();
				go.name = "NativeToolkit";
				instance = go.AddComponent<NativeToolkit>();

				#if UNITY_ANDROID

				if(Application.platform == RuntimePlatform.Android)
					obj = new AndroidJavaClass("com.secondfury.nativetoolkit.Main");

				#endif
			}
			
			return instance; 
		}
	}

	void Awake() 
	{
		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
		}
	}


	//=============================================================================
	// Grab and save screenshot
	//=============================================================================

	public static void SaveScreenshot(string fileName, string albumName = "MyScreenshots", string fileType = "jpg", Rect screenArea = default(Rect))
	{
		Debug.Log("Save screenshot to gallery " + fileName);

		if(screenArea == default(Rect))
			screenArea = new Rect(0, 0, Screen.width, Screen.height);

		Instance.StartCoroutine(Instance.GrabScreenshot(fileName, albumName, fileType, screenArea));
	}
	
	IEnumerator GrabScreenshot(string fileName, string albumName, string fileType, Rect screenArea)
	{
		yield return new WaitForEndOfFrame();

		Texture2D texture = new Texture2D ((int)screenArea.width, (int)screenArea.height, TextureFormat.RGB24, false);
		texture.ReadPixels (screenArea, 0, 0);
		texture.Apply ();
		
		byte[] bytes;
		string fileExt;
		
		if(fileType == "png")
		{
			bytes = texture.EncodeToPNG();
			fileExt = ".png";
		}
		else
		{
			bytes = texture.EncodeToJPG();
			fileExt = ".jpg";
		}

		if (OnScreenshotTaken != null)
			OnScreenshotTaken (texture);
		else
			Destroy (texture);
		
		string date = System.DateTime.Now.ToString("hh-mm-ss_dd-MM-yy");
		string screenshotFilename = fileName + "_" + date + fileExt;
		string path = Application.persistentDataPath + "/" + screenshotFilename;

		#if UNITY_ANDROID

		if(Application.platform == RuntimePlatform.Android) 
		{
			string androidPath = Path.Combine(albumName, screenshotFilename);
			path = Path.Combine(Application.persistentDataPath, androidPath);
			string pathonly = Path.GetDirectoryName(path);
			Directory.CreateDirectory(pathonly);
		}

		#endif
		
		Instance.StartCoroutine(Instance.Save(bytes, fileName, path, ImageType.SCREENSHOT));
	}


	//=============================================================================
	// Save texture
	//=============================================================================

	public static void SaveImage(Texture2D texture, string fileName, string fileType = "jpg")
	{
		Debug.Log("Save image to gallery " + fileName);

		Instance.Awake();

		byte[] bytes;
		string fileExt;
		
		if(fileType == "png")
		{
			bytes = texture.EncodeToPNG();
			fileExt = ".png";
		}
		else
		{
			bytes = texture.EncodeToJPG();
			fileExt = ".jpg";
		}

		string path = Application.persistentDataPath + "/" + fileName + fileExt;

		Instance.StartCoroutine(Instance.Save(bytes, fileName, path, ImageType.IMAGE));
	}
	
	
	IEnumerator Save(byte[] bytes, string fileName, string path, ImageType imageType)
	{
		int count = 0;
		SaveStatus saved = SaveStatus.NOTSAVED;
		
		#if UNITY_IOS
		
		if(Application.platform == RuntimePlatform.IPhonePlayer) 
		{
			System.IO.File.WriteAllBytes(path, bytes);
			
			while(saved == SaveStatus.NOTSAVED)
			{
				count++;
				if(count > 30) 
					saved = SaveStatus.TIMEOUT;
				else
					saved = (SaveStatus)saveToGallery(path);
				
				yield return Instance.StartCoroutine(Instance.Wait(.5f));
			}
			
			UnityEngine.iOS.Device.SetNoBackupFlag(path);
		}
		
		
		#elif UNITY_ANDROID	
		
		if(Application.platform == RuntimePlatform.Android) 
		{
			System.IO.File.WriteAllBytes(path, bytes);
			
			while(saved == SaveStatus.NOTSAVED) 
			{
				count++;
				if(count > 30) 
					saved = SaveStatus.TIMEOUT;
				else
					saved = (SaveStatus)obj.CallStatic<int>("addImageToGallery", path);
				
				yield return Instance.StartCoroutine(Instance.Wait(.5f));
			}
		}
		
		#else
			
		Debug.Log("Native Toolkit: Save file only available in iOS/Android modes");
			
		saved = SaveStatus.SAVED;

		yield return null;
		
		#endif
		
		switch(saved)
		{
			case SaveStatus.DENIED:
				path = "DENIED";
				break;
				
			case SaveStatus.TIMEOUT:
				path = "TIMEOUT";
				break;
		}
		
		switch(imageType)
		{
			case ImageType.IMAGE:
				if(OnImageSaved != null) 
					OnImageSaved(path);
				break;
				
			case ImageType.SCREENSHOT:
				if(OnScreenshotSaved != null) 
					OnScreenshotSaved(path);
				break;
		}
	}


	//=============================================================================
	// Image Picker
	//=============================================================================

	public static void PickImage()
	{
		Instance.Awake ();

		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer)
			pickImage();

		#elif UNITY_ANDROID	

		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("pickImageFromGallery");

		#endif
	}
	
	public void OnPickImage(string path)
	{
        Texture2D texture = LoadImageFromFile(path);

        if(OnImagePicked != null)
            OnImagePicked(texture, path);
	}


	//=============================================================================
	// Camera
	//=============================================================================
	
	public static void TakeCameraShot()
	{
		Instance.Awake ();
		
		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer)
			openCamera();
		
		#elif UNITY_ANDROID	
		
		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("takeCameraShot");

		#endif
	}

	public void OnCameraFinished(string path)
	{
        Texture2D texture = LoadImageFromFile(path);

        if(OnCameraShotComplete != null)
            OnCameraShotComplete(texture, path);
	}

	

	//=============================================================================
	// Email with optional attachment
	//=============================================================================

	public static void SendEmail(string subject, string body, string pathToImageAttachment = "", string to = "", string cc = "", string bcc = "")
	{
		Instance.Awake ();
		
		#if UNITY_IOS

		if(Application.platform == RuntimePlatform.IPhonePlayer)
			sendEmail(to, cc, bcc, subject, body, pathToImageAttachment);

		#elif UNITY_ANDROID	
		
		if(Application.platform == RuntimePlatform.Android) 
			obj.CallStatic("sendEmail", new object[] { to, cc, bcc, subject, body, pathToImageAttachment } );
		
		#endif
	}

	//=============================================================================
	// General functions
	//=============================================================================

	public static Texture2D LoadImageFromFile(string path)
	{
		if(path == "Cancelled") return null;

		byte[] bytes;
		Texture2D texture = new Texture2D(128, 128, TextureFormat.RGB24, false);

		#if UNITY_WINRT

		bytes = UnityEngine.Windows.File.ReadAllBytes(path);
		texture.LoadImage(bytes);

		#else

		bytes = System.IO.File.ReadAllBytes(path);
		texture.LoadImage(bytes);

		#endif

		return texture;
	}
	
	
	IEnumerator Wait(float delay)
	{
		float pauseTarget = Time.realtimeSinceStartup + delay;
		
		while(Time.realtimeSinceStartup < pauseTarget)
		{
			yield return null;	
		}
	}
}