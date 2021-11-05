using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenShot : MonoBehaviour
{

    public static void screen_Shot(Action<byte[]> cb, int scaleWidth, int scaleHeight)
    {
        CoroutineHub.GetInstance().StartCoroutine(screen_Shot_(cb, scaleWidth, scaleHeight));
    }

    // 儲存紀錄，在此執行螢幕擷取 
    static IEnumerator screen_Shot_(Action<byte[]> cb, int scaleWidth, int scaleHeight)
    {
        // 在當前禎的結尾進行截圖
        yield return new WaitForEndOfFrame();

        // 螢幕截圖的實作，並且指派到 tex 變數 (Texture2D)
        var tex = new Texture2D(Screen.width, Screen.height);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        // 縮放尺寸
        tex = ScaleTexture(tex, scaleWidth, scaleHeight);
        tex.Apply();

        cb?.Invoke(tex.EncodeToPNG());
    }

    // 代入圖像並且指定縮放的尺寸
    static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        // 依據代入的尺寸實例化一個 Texture2D 物件
        var result = new Texture2D(targetWidth, targetHeight, source.format, false);

        // 圖像尺寸縮放
        for (int i = 0; i < result.height; ++i)
        {
            for (int j = 0; j < result.width; ++j)
            {
                var col = source.GetPixelBilinear(
                    (float)j / result.width,
                    (float)i / result.height
                );
                result.SetPixel(j, i, col);
            }
        }
        result.Apply();

        return result;
    }


    // 將儲存的 byte[] 轉換為圖像
    public static Texture2D GetTextureWithBytes(byte[] screenshotData, int targetWidth, int targetHeight)
    {
        var tex = new Texture2D(targetWidth, targetHeight);
        tex.LoadImage(screenshotData);
        return tex;
    }
}
