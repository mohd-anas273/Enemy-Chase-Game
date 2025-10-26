using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class ParallaxDataHolder
{
    public string name;
    public GameObject gameObject;
    public float speed;
    public float rightMostPosition;
    public float leftMostPosition;
    [HideInInspector] public GameObject[] activeObjects;
}
public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private ParallaxDataHolder[] parallaxDatas;
    [SerializeField] private int maxTiles=2;

    private Coroutine parallax_Coroutine;

    public void SpawnParallax()
    {
        foreach (ParallaxDataHolder parallaxData in parallaxDatas)
        {
            parallaxData.activeObjects = new GameObject[maxTiles];
            for (int i = 0; i < maxTiles; i++)
            {
                GameObject obj = Instantiate(parallaxData.gameObject, transform);
                parallaxData.activeObjects[i] = obj;
            }
        }
    }

    public void SetPosition()
    {
        foreach(ParallaxDataHolder parallaxData in parallaxDatas)
        {
            for(int i = 0; i< parallaxData.activeObjects.Length; i++)
            {
                Transform t = parallaxData.activeObjects[i].transform;
                Vector3 spawnPosition = -i * parallaxData.rightMostPosition * Vector3.right;
                spawnPosition.y = t.position.y;
                t.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            }
        }
    }
    public void StartParallax()
    {
        if (parallax_Coroutine != null)
        {
            StopCoroutine(parallax_Coroutine);
        }
        parallax_Coroutine = StartCoroutine(UpdateParallax());
    }

    public void StopParallax()
    {
        if(parallax_Coroutine != null)
        {
            StopCoroutine(parallax_Coroutine);
        }
        parallax_Coroutine = null;
    }

    IEnumerator UpdateParallax()
    {
        while (true)
        {
            for (int i = 0; i < parallaxDatas.Length; i++)
            {
                ParallaxDataHolder parallaxData = parallaxDatas[i];
                float movement = parallaxData.speed * Time.deltaTime;
                for (int j = 0; j < parallaxData.activeObjects.Length; j++)
                {
                    Transform obj = parallaxData.activeObjects[j].transform;
                    Vector3 position = obj.position;
                    position.x -= movement;
                    if (position.x <= parallaxData.leftMostPosition)
                    {
                        position.x = parallaxData.rightMostPosition;
                    }
                    obj.position = position;
                }
            }
            yield return null;
        }
    }
}
