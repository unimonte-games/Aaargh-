using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Select : MonoBehaviour
{
    public List<GameObject> models;
    private int selectionIndex = 0;
    
    void Start()
    {
        models = new List<GameObject>();
        foreach(Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(true);
        }
        models[selectionIndex].SetActive(false);
    }

    void Update()
    {
       if(Input.GetMouseButton(1))
        {
            SceneManager.LoadScene("Sample Scene");
        }
    }
    public void Selection(int index)
    {
        if (index == selectionIndex)
            return;
        if (index < 0 || index >= models.Count)
            return;
        models[selectionIndex].SetActive(false);
        selectionIndex = index;
        models[selectionIndex].SetActive(true);
        if (Input.GetMouseButtonDown(0))
        {
            
            selectionIndex++;
        }

    }
    
}
