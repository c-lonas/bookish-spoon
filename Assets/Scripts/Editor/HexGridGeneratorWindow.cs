using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using static HexGridController;


public class HexGridGeneratorWindow : EditorWindow
{

    // Editor window parameters
    private bool foldoutStep1 = true;
    private bool foldoutStep2 = false;
    private bool foldoutStep3 = false;
    private bool foldoutStep4 = false;
    private bool foldoutStep5 = false;
    private bool foldoutStep6 = false;
    private bool foldoutStep7 = false;

    // Grid generation parameters
    int gridSize = 10;
    float initialElevation = 0f;


    [MenuItem("Window/Procedural Generator")]
    public static void ShowWindow()
    {
        GetWindow<HexGridGeneratorWindow>("Procedural Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Procedural Generator Settings", EditorStyles.boldLabel);

        // create a GUIStyle that mimics boldLabel as a workaround for the foldout label not being bold
        GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldout);
        foldoutStyle.fontStyle = FontStyle.Bold;

        // Step 1
        GUILayout.Space(10);
        foldoutStep1 = EditorGUILayout.Foldout(foldoutStep1, "Step 1: Hex Grid Creation", foldoutStyle);

        if (foldoutStep1)
        {
            GUILayout.Space(5);
            gridSize = EditorGUILayout.IntField("Grid Size", gridSize);
            initialElevation = EditorGUILayout.FloatField("Initial Elevation", initialElevation);

            GUILayout.Space(5);
            if (GUILayout.Button("Generate Hex Grid"))
            {
                GenerateHexGrid();
            }

            if (GUILayout.Button("Clear Hex Grid"))
            {
                ClearHexGrid();
            }

        }


        // Step 2
        GUILayout.Space(10);
        foldoutStep2 = EditorGUILayout.Foldout(foldoutStep2, "Step 2: Parcel out the grid into sections", foldoutStyle);
        if (foldoutStep2)
        {
            if (GUILayout.Button("Parcel Grid")) 
            {
                // Implement this function...
            }
        }

        // Step 3
        GUILayout.Space(10);
        foldoutStep3 = EditorGUILayout.Foldout(foldoutStep3, "Step 3: Shape & Blend Terrain", foldoutStyle);
        if (foldoutStep3)
        {
            if (GUILayout.Button("Shape Terrain")) 
            {
                // Implement this function...
            }
        }


        // Step 4
        GUILayout.Space(10);
        foldoutStep4 = EditorGUILayout.Foldout(foldoutStep4, "Step 4: Spawn Rivers", foldoutStyle);
        if (foldoutStep4)
        {
            if (GUILayout.Button("Spawn Rivers")) 
            {
                // Implement this function...
            }
        }


        // Step 5
        GUILayout.Space(10);
        foldoutStep5 = EditorGUILayout.Foldout(foldoutStep5, "Step 5: Spawn Major Objects", foldoutStyle);
        if (foldoutStep5)
        {
            if (GUILayout.Button("Spawn major Objects")) 
            {
                // Implement this function...
            }
        }
        

        // Step 6
        GUILayout.Space(10);
        foldoutStep6 = EditorGUILayout.Foldout(foldoutStep6, "Step 6: Spawn Minor Objects", foldoutStyle);
        if (foldoutStep6)
        {
            if (GUILayout.Button("Spawn Minor Objects")) 
            {
                // Implement this function...
            }
        }

        // Step 7
        GUILayout.Space(10);
        foldoutStep7 = EditorGUILayout.Foldout(foldoutStep7, "Step 7: Spawn NPCs", foldoutStyle);
        if (foldoutStep7)
        {
            if (GUILayout.Button("Spawn NPCs")) 
            {
                // Implement this function...
            }
        }


    }



    private void GenerateHexGrid()
    {
        HexGridController hexGridController = FindObjectOfType<HexGridController>();

        if (hexGridController == null)
        {
            Debug.LogError("No HexGridController found in the scene. Please add one to a GameObject.");
            return;
        }
        else 
        {
            hexGridController.GenerateGrid(gridSize, initialElevation);
        }
    }

    private void ClearHexGrid()
    {
        HexGridController hexGridController = FindObjectOfType<HexGridController>();

        if (hexGridController == null)
        {
            Debug.LogError("No HexGridController found in the scene. Please add one to a GameObject.");
            return;
        }

        hexGridController.ClearGrid();
    }
}