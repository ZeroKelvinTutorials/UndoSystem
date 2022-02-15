using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPieces : MonoBehaviour
{
    SavedElement[] elements;// = new SavedElement[4];
    Vector3[] previousPositions;
    void Start()
    {
        elements = GameObject.FindObjectsOfType<SavedElement>();
        previousPositions = GetPositions(elements);
        foreach(SavedElement element in elements)
        {
            element.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    Vector3[] GetPositions(SavedElement[] elementList)
    {
        Vector3[] newArray = new Vector3[elementList.Length];
        for(int i = 0; i<elementList.Length; i++)
        {
            newArray[i] = elementList[i].transform.position;
        }
        return newArray;
    }
    void LateUpdate()
    {
        if(GetMovementInput()!=Vector3.zero || Input.GetKeyDown(KeyCode.Z))
        {
            ColorMovedPieces();
            Vector3[] newPositions = GetPositions(elements);
            for(int i = 0; i < newPositions.Length; i++)
            {
                if(previousPositions[i]==newPositions[i])
                {
                    elements[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
                else
                {
                    elements[i].GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
            previousPositions = newPositions;
        }
    }
    Vector3 GetMovementInput()
    {
        Vector3 inputValue = Vector3.zero;
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inputValue = Vector3.left;
        }
        else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputValue = Vector3.up;
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            inputValue = Vector3.right;
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputValue = Vector3.down;
        }
        return inputValue;
    }
    void ColorMovedPieces()
    {

    }

    
}
