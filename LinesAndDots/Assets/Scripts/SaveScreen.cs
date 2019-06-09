using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class SaveScreen : MonoBehaviour
{
    public Texture2D screenTexture;

    public Camera mainCam;
    // Start is called before the first frame update

    public void SaveScreenTexture()
    {
        int sw = (int) Screen.width;
        int sh = (int) Screen.height;
        RenderTexture screenRenderTexture = new RenderTexture(sw, sh, 0);
        mainCam.targetTexture = screenRenderTexture;
        screenTexture = new Texture2D(sw, sh, TextureFormat.RGB24, false);
        mainCam.Render();
        RenderTexture.active = screenRenderTexture;
        screenTexture.ReadPixels(new Rect(0,0,sw,sh), 0, 0);
        mainCam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(screenRenderTexture);
        byte[] bytes = screenTexture.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + "/" + name + ".png", bytes);
    }
}
