using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KeysManager : MonoBehaviour
{
    public Key[] Keys;

    private GameObject[] keyObjects;

    void Awake()
    {
        Camera camera = Camera.main;

        keyObjects = new GameObject[Keys.Length];
        AddKeys();
    }

    void Update()
    {
        for (int i = 0; i < Keys.Length; i++)
        {
            if (Keys[i].Owned == true)
            {
                ActivateKey(i);
                GameObject.Find(Keys[i].Name).transform.Rotate(0, 6.0f * 10.0f * Time.deltaTime, 0);
            }
        }
    }

    void ActivateKey(int keyIndex)
    {
        string keyName = Keys[keyIndex].Name;
        keyObjects[keyIndex].SetActive(true);
    }

    void AddKeys()
    {
        for (int i = 0; i < Keys.Length; i++)
        {
            Key key = Keys[i];

            GameObject currentObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            currentObject.name = key.Name ?? i.ToString();
            var v3Pos = new Vector3(0.0f, 1.0f, 7.25f);
            Camera cam = GameObject.Find("ItemsCamera").GetComponent<Camera>();
            Vector3 pos = new Vector3(-3 + (i * 2), 0, 5);
            currentObject.transform.position = pos;
            currentObject.transform.localScale = new Vector3(1f, 3f, 1f);
            currentObject.GetComponent<MeshRenderer>().material.color = key.KeyColor;
            currentObject.SetActive(key.Owned);
            currentObject.transform.SetParent(GameObject.Find("ItemsCamera").transform);

            keyObjects[i] = currentObject;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "red" || other.tag == "green" || other.tag == "blue" || other.tag == "purple" || other.tag == "white")
        {
            int Index = Array.FindIndex(Keys, p => p.Name == other.tag);
            Keys[Index].Owned = true;
        }

    }
}

[Serializable]
public struct Key
{
    public string Name;
    public Color KeyColor;
    public bool Owned;
}


