using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region spriteVariables
    public Sprite[] rightSprites;
    public Sprite[] leftSprites;
    public Sprite[] downSprites;
    public Sprite[] upSprites;
    public int spriteIndex;
    SpriteRenderer spriteRenderer;
    #endregion

    Transform boxToMove;
    
    void Awake()
    {
        spriteIndex = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 movementDirection = Vector3.zero;
        movementDirection = GetMovementInput();
        boxToMove = null;
        if(movementDirection != Vector3.zero && CanMove(movementDirection))
        {
            MoveTo(movementDirection);
        }
    }
        
    void MoveTo(Vector3 movementDirection)
    {
        transform.position += movementDirection;
        if(boxToMove!=null)
        {
            boxToMove.transform.position += movementDirection;
        }
        spriteIndex++;
        AssignSprite(spriteIndex,movementDirection);
        GameManager.SaveGameState();
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

    public void UndoSpriteIndex()
    {
        spriteIndex -= 1;
        if(spriteIndex<0)
        {
            spriteIndex = rightSprites.Length-1;
        }
    }

    void AssignSprite(int index, Vector3 direction)
    {
        if(spriteIndex == rightSprites.Length)
        {
            spriteIndex = 0;
        }
        Sprite spriteToAssign = null;
        if(direction == Vector3.left)
        {
            spriteToAssign = leftSprites[spriteIndex];
        }
        else if(direction == Vector3.right)
        {
            spriteToAssign = rightSprites[spriteIndex];
        }
        else if(direction == Vector3.down)
        {
            spriteToAssign = downSprites[spriteIndex];
        }
        else if(direction == Vector3.up)
        {
            spriteToAssign = upSprites[spriteIndex];
        }
        if(spriteToAssign!=null)
        {
            spriteRenderer.sprite = spriteToAssign;
        }
    }

    bool CanMove(Vector3 direction)
    {
        Vector3 positionToCheckFirst = transform.position + direction;
        Collider2D colliderFirst = GetColliderAt(positionToCheckFirst);
        Vector3 positionToCheckSecond = positionToCheckFirst + direction;
        Collider2D colliderSecond = GetColliderAt(positionToCheckSecond);
        if(colliderFirst == null)
        {
            return true;
        }
        else if(colliderFirst.tag == "Box")
        {
            if(colliderSecond == null)
            {
                boxToMove = colliderFirst.transform;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        return false;
    }

    Collider2D GetColliderAt(Vector3 position)
    {
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(position,.3f, Vector3.zero);
        if(hit)
        {
            Debug.Log(hit.collider);
        }
        return hit.collider;
    }

}
