#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ScreenMaker : MonoBehaviour
{
    [SerializeField] RenderTexture rt;
    [SerializeField] string fileName;
    [SerializeField] private List<GameObject> listOfObjects;
    [SerializeField] private MeshFilter currentMesh;
    [SerializeField] private bool toList;

    IEnumerator Start()
    {
        if (toList)
        {
            for (int index = 0; index < listOfObjects.Count;)
            {
                listOfObjects[index].SetActive(true);


                for (int i = 0; i < listOfObjects[index].transform.childCount; i++)
                {
                    Transform current = listOfObjects[index].transform.GetChild(i);
                    current.gameObject.SetActive(false);
                }

                listOfObjects[index].transform.GetChild(0).gameObject.SetActive(true);

                for (int i = 1; i < listOfObjects[index].transform.childCount; i++)
                {
                    if (i >= 2)
                    {
                        Transform current2 = listOfObjects[index].transform.GetChild(i - 1);
                        current2.gameObject.SetActive(false);
                    }

                    Transform current = listOfObjects[index].transform.GetChild(i);
                    current.gameObject.SetActive(true);
                    fileName = current.GetComponent<MeshFilter>().name;
                    SavePNG();
                    yield return new WaitForSeconds(0.5f);
                }

                for (int i = 0; i < listOfObjects[index].transform.childCount; i++)
                {
                    Transform current = listOfObjects[index].transform.GetChild(i);
                    current.gameObject.SetActive(false);
                }

                index++;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            fileName = currentMesh.mesh.name;
            SavePNG();
        }
    }


    public void SavePNG()
    {
        Texture2D tex = new Texture2D(rt.width, rt.height);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        tex.Apply();

        string path = "Assets/Textures/Rendered textures/Eyebrow/" + fileName + ".png";
        File.WriteAllBytes(path, tex.EncodeToPNG());
    }
}
#endif