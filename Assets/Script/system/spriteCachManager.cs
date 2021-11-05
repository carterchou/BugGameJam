using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
public class spriteCachManager : MonoBehaviour
{
    static List<spriteCache> caches;

    class spriteCache {
        public Sprite img;
        public string img_name;
        public string spriteAtlas_name;

        public spriteCache(Sprite img, string img_name, string spriteAtlas_name)
        {
            this.img = img;
            this.img_name = img_name;
            this.spriteAtlas_name = spriteAtlas_name;
        }
    }

    public static Sprite GetSprite(string spriteAtlas_name, string img_name)
    {
        if (caches == null) caches = new List<spriteCache>();
        spriteCache cacheData = caches.Find(o => o.spriteAtlas_name == spriteAtlas_name && o.img_name == img_name);
        bool isCache = cacheData != null;

        if (!isCache)
        {
            try
            {
                Sprite img = Resources.Load<SpriteAtlas>(string.Format("spriteAtlas/{0}", spriteAtlas_name)).GetSprite(img_name);
                if(img == null)
                {
                    Debug.Log(string.Format("[SpriteCache] Not found! :{0}/{1}", spriteAtlas_name, img_name));
                    return null;
                }
                else
                {
                    cacheData = new spriteCache(img, img_name, spriteAtlas_name);
                    caches.Add(cacheData);
                    Debug.Log(string.Format("[SpriteCache] Cache success! :{0}/{1}", spriteAtlas_name, img_name));
                }
            }
            catch(Exception e)
            {
                Debug.Log("[SpriteCache] Load Error! :" + e.ToString());
                return null;
            }

        }

        return cacheData.img;
    }
}
