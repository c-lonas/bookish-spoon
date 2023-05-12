using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class HexTileHighlighter : EditorWindow
{
    private List<GameObject> highlightedObjects = new List<GameObject>();

    [MenuItem("Window/Hex Tile Highlighter")]
    public static void ShowWindow()
    {
        GetWindow<HexTileHighlighter>("Hex Tile Highlighter");
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnScene;
        Debug.Log("Tile Highlighter Enabled");
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnScene;
    }

    private void OnScene(SceneView scene)
    {
        // Perform a raycast from the mouse position
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(worldRay, out hit))
        {
            // Check if the hit object has a HexTile component
            hex_tile tile = hit.transform.GetComponent<hex_tile>();
            Debug.Log("Raycast hit: " + hit.transform.name);

            if (tile != null)
            {
                // Highlight this object
                HighlightObject(hit.transform.gameObject);
                Debug.Log("Raycast hit: " + hit.transform.name);
            }
            else
            {
                // If we're not over a HexTile, unhighlight everything
                UnhighlightAll();
            }
        }
        else
        {
            // If the raycast didn't hit anything, unhighlight everything
            UnhighlightAll();
        }
    }

    private void HighlightObject(GameObject obj)
    {
        if (!highlightedObjects.Contains(obj))
        {
            // Add the object to the list of highlighted objects
            highlightedObjects.Add(obj);

            // Change the object's color to highlight it
            Renderer renderer = obj.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.material.color = new Color(0, 2, 1); // Use whatever color you want for the highlight
            }
        }
    }

    private void UnhighlightAll()
    {
        foreach (GameObject obj in highlightedObjects)
        {
            // Change the object's color back to its original color
            Renderer renderer = obj.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.material.color = Color.white; // Use the object's original color here
            }
        }

        // Clear the list of highlighted objects
        highlightedObjects.Clear();
    }
}
