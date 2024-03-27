using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Backing fields
    private int playerIndex;

    // Public properties
    public int BoardXPos { get; private set; }
    public int BoardYPos { get; private set; }
    public struct TileReference
    {
        public Vector2 tilePosition;
        public string tileName;
    }

    // References to the renderer and materials for the tile
    private MeshRenderer rendererReference;

    // Default Materials 
    private Material BlueMaterial;
    private Material RedMaterial;

    // Initialization in Start method
    // Assumes that the tile materials are located within a Resources folder
    void Start()
    {
        rendererReference = GetComponent<MeshRenderer>();
        // Materials are loaded with the generic typecast
        BlueMaterial = Resources.Load<Material>("Materials/PlayerTileColor");
        RedMaterial = Resources.Load<Material>("Materials/EnemyTileColor");
        this.playerIndex = -1;
    }

    public int GetPlayer() 
    {
        return playerIndex;
    }

    public void SetPlayer(int index)
    {
        playerIndex = index;
    }
    public void SetMaterial(Material material) {
        rendererReference.material = material;
    }
    public Vector2 GetTilePosition()
    {
        return new Vector2(BoardXPos, BoardYPos);
    }
}
