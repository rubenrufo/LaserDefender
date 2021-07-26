using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsState : MonoBehaviour
{

    [SerializeField] bool inputKeyboard = false;

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)     // GetType() gets the type of this object
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    public bool GetInputMethod()
    {
        return inputKeyboard;
    }

    public void ChangeInputMethod(bool inputKeyboard)
    {
        inputKeyboard = !inputKeyboard;
        Debug.Log(inputKeyboard);
    }

}
