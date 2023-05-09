using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using static HexGridController;


public class HexGridGeneratorWindow : EditorWindow
{
    int gridSize = 10;
    float initialElevation = 0f;
    bool useRandomElevation = false;
    float minRandomElevation = 0f;
    float maxRandomElevation = 10f;


    NoiseType noiseType;
    bool useNoise = false;
    float noiseScale = 1f;
    float noiseAmplitude = 1f;

    [MenuItem("Window/Hex Grid Generator")]
    public static void ShowWindow()
    {
        GetWindow<HexGridGeneratorWindow>("Hex Grid Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Hex Grid Settings", EditorStyles.boldLabel);
        gridSize = EditorGUILayout.IntField("Grid Size", gridSize);
        initialElevation = EditorGUILayout.FloatField("Initial Elevation", initialElevation);


        useRandomElevation = EditorGUILayout.Toggle("Use Random Elevation", useRandomElevation);
        if (useRandomElevation)
        {
            minRandomElevation = EditorGUILayout.FloatField("Minimum Elevation", minRandomElevation);
            maxRandomElevation = EditorGUILayout.FloatField("Maximum Elevation", maxRandomElevation);
        }


        useNoise = EditorGUILayout.Toggle("Use Noise", useNoise);
        if (useNoise)
        {
            noiseType = (NoiseType)EditorGUILayout.EnumPopup("Noise Type", noiseType);
            noiseScale = EditorGUILayout.FloatField("Noise Scale", noiseScale);
            noiseAmplitude = EditorGUILayout.FloatField("Noise Amplitude", noiseAmplitude);
        }

        if (GUILayout.Button("Generate Hex Grid"))
        {
            GenerateHexGrid();
        }

        if (GUILayout.Button("Clear Hex Grid"))
        {
            ClearHexGrid();
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

        if (useRandomElevation)
        {
            hexGridController.GenerateGrid(gridSize, initialElevation, minRandomElevation, maxRandomElevation);
        }
        else if (useNoise)
        {
            hexGridController.GenerateGrid(gridSize, initialElevation, noiseType, noiseScale, noiseAmplitude);
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
