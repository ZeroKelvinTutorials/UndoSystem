using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class GameState
{
    #region dataToSave
    public Vector3 playerPos;
    public Sprite playerSprite;
    public List<Vector3> boxPositions;
    #endregion

    public static GameState GetCurrentState()
    {
        GameState gameStateToSave = new GameState();
        SavedElement[] elementsToSaveOnScene = GameObject.FindObjectsOfType<SavedElement>();
        gameStateToSave.boxPositions = new List<Vector3>();
        foreach(SavedElement element in elementsToSaveOnScene)
        {
            if(element.type == SavedElement.Type.Player)
            {
                gameStateToSave.playerPos = element.transform.position;
                gameStateToSave.playerSprite = element.transform.GetComponent<SpriteRenderer>().sprite;
            }
            else if(element.type == SavedElement.Type.Box)
            {
                gameStateToSave.boxPositions.Add(element.transform.position);
            }
        }
        return gameStateToSave;
    }
    public void LoadGameState()
    {
        SavedElement[] elementsToLoadOnscene = GameObject.FindObjectsOfType<SavedElement>();
        List<Vector3> remainingBoxPosition =  new List<Vector3>(boxPositions);
        foreach(SavedElement elementToLoad in elementsToLoadOnscene)
        {
            if(elementToLoad.type == SavedElement.Type.Player)
            {
                elementToLoad.transform.position = playerPos;
                elementToLoad.GetComponent<SpriteRenderer>().sprite = playerSprite;
                elementToLoad.GetComponent<Player>().UndoSpriteIndex();
            }
            else if(elementToLoad.type == SavedElement.Type.Box)
            {
                elementToLoad.transform.position = remainingBoxPosition[0];
                remainingBoxPosition.RemoveAt(0);
            }
        }
    }
}
