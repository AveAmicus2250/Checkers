using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject redPiecePrefab, whitePiecePrefab;
    public Vector3 boardOffset = new Vector3(-4.0f, 0.0f, -5.0f);
    public Vector3 pieceOffset = new Vector3(.5f, 0, .5f);
    public Piece[,] pieces = new Piece[8, 8];

    // Converts array coordinates to world position
    Vector3 GetWorldPosition(Vector2Int cell)
    {
        return new Vector3(cell.x, 0, cell.y) + boardOffset + pieceOffset;
    }

    // Moves a Piece to another coordinate on a 2D Grid
    void MovePiece(Piece piece, Vector2Int newCell)
    {
        Vector2Int oldCell = piece.cell;
        // Update array
        pieces[oldCell.x, oldCell.y] = null;
        pieces[newCell.x, newCell.y] = piece;
        // Update data on piece
        piece.oldCell = oldCell;
        // Translate the piece to another location
        piece.transform.localPosition = GetWorldPosition(newCell);
    }

    // Generates a Checker Piece in specified coordinates
    void GeneratePiece(GameObject prefab, Vector2Int desiredCell)
    {
        // Generate Instance of prefab
        GameObject clone = Instantiate(prefab, transform);
        // Get the Piece component 
        Piece piece = clone.GetComponent<Piece>();
        // Set the cell data for the first time
        piece.oldCell = desiredCell;
        piece.cell = desiredCell;
        // Resposition clone
        MovePiece(piece, desiredCell);
    }

    void GenerateBoard()
    {
        Vector2Int desiredCell = Vector2Int.zero;
        // Generate White Team
        for (int y = 0; y < 3; y++)
        {
            bool oddRow = y % 2 == 0;
            // Loop through columns
            for (int x = 0; x < 8; x += 2)
            {
                desiredCell.x = oddRow ? x : x + 1;
                desiredCell.y = y;
                // Generate Piece
                GeneratePiece(whitePiecePrefab, desiredCell);
            }
        }
        // Generate Red Team
        for (int y = 5; y < 8; y++)
        {
            bool oddRow = y % 2 == 0;
            // Loop through colunms
            for (int x = 0; x < 8; x += 2)
            {
                desiredCell.x = oddRow ? x : x + 1;
                desiredCell.y = y;
                // Generate Piece
                GeneratePiece(redPiecePrefab, desiredCell);
            }
        }
    }

    // Use this for intialization
    void Start()
    {
        GenerateBoard();    
    }
}
