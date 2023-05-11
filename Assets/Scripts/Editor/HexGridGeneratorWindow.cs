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


    // Step 1 (Grid generation) parameters
    int gridSize = 10;
    float initialElevation = 0f;

    // Step 2 (Parceling) parameters
    private int numberOfCities = 1;
    private int oceanRingRadius = 2;
    private float averageTerrainSectionSize = 10f;
    private float terrainSectionVariation = 2f;


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

        // Step 1 (GUI)
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
                Debug.Log("Generating Hex Grid...");
                GenerateHexGrid();
            }

            if (GUILayout.Button("Clear Hex Grid"))
            {
                Debug.Log("Clearing Hex Grid...");
                ClearHexGrid();
            }

        }


        // Step 2 (GUI)
        GUILayout.Space(10);
        foldoutStep2 = EditorGUILayout.Foldout(foldoutStep2, "Step 2: Parcel Grid Into Sections", foldoutStyle);

        if (foldoutStep2)
        {
            GUILayout.Space(5);
            numberOfCities = EditorGUILayout.IntField("Number of Cities", numberOfCities);
            oceanRingRadius = EditorGUILayout.IntField("Ocean Ring Radius", oceanRingRadius);
            averageTerrainSectionSize = EditorGUILayout.FloatField("Average Terrain Section Size", averageTerrainSectionSize);
            terrainSectionVariation = EditorGUILayout.FloatField("Terrain Section Variation", terrainSectionVariation);


            if (GUILayout.Button("Parcel Grid")) 
            {
                // Implement this function...


                // First we need to supply some parameters about how the parceling
                // should take place. Ie, how many cities, what the ocean ring radius 
                // should be, what size the evolving terrain sections should be 
                // and the variation range, etc.

                // Before these sections are 'baked', we should be able to modify them,
                // so we should be able to select a section and make changes to it,
                // such as dragging the borders, or changing the section type, etc.
                // This step will likely require a new editor tool to 
                // let the user interact with the grid more effectively.

                // We're going to need to have a way to store the sections as 
                // game objects that are labeled and organized in the hierarchy

                // Once the sections look good, we should be able to 'bake' them

                // Sections will need to be assigned components at this time to 
                // allow further modifications based on the section type
                // eg, cities will need separate logic, evolving terrain sections, 
                // oceans, etc.
                Debug.Log("Parceling Grid...");
                ParcelGrid(numberOfCities, oceanRingRadius, averageTerrainSectionSize, terrainSectionVariation);
            }
            if (GUILayout.Button("Bake Sections"))
            {
                Debug.Log("<NOT IMPLEMENTED YET>Baking Sections...");
                // Implement this function...
            }

            if (GUILayout.Button("Clear Sections"))
            {
                Debug.Log("Clearing Sections...");
                ClearSections();
            }
        }

        // Step 3 (GUI)
        GUILayout.Space(10);
        foldoutStep3 = EditorGUILayout.Foldout(foldoutStep3, "Step 3: Shape & Blend Terrain", foldoutStyle);
        if (foldoutStep3)
        {
            if (GUILayout.Button("Shape Terrain")) 
            {
                // Implement this function...

                // This section will need to act on the sections that were
                // created in the previous step. Based on the section type,
                // evolving terrain for example will then be procedurally shaped
                // by taking in parameters that are determined in this section.
                // For example, sliders to set the likelihood of a mountain,
                // or a forest, or desert, etc.

                // We also will need subfolders to set parameters for things like 
                // how likely we want dense/sparse environments (per biome, as in
                // how likely do we want dense forests in a forest biome)

                // This section also needs to include the blending, so sections
                // will need to query their neighbors to determine how to blend.
                // we will also want user parameters to determine how much blending,
                // or how likely the blending is to be smooth or abrupt.

                // This blending logic needs to be replicable at runtime, so realtime
                // queries will need to be possible.

                // Relatedly, this is probably when we will attach the realtime evolution
                // logic to the evolving terrain sections via components.

                // The real-time evolution logic should also be the logic that manages the 
                // material stuff probably to avoid the errors we were getting with the
                // material stuff being applied in the editor script
            }
        }


        // Step 4 (GUI)
        GUILayout.Space(10);
        foldoutStep4 = EditorGUILayout.Foldout(foldoutStep4, "Step 4: Spawn Rivers", foldoutStyle);
        if (foldoutStep4)
        {
            if (GUILayout.Button("Spawn Rivers")) 
            {
                // Implement this function...
            }
        }


        // Step 5 (GUI)
        GUILayout.Space(10);
        foldoutStep5 = EditorGUILayout.Foldout(foldoutStep5, "Step 5: Spawn Major Objects", foldoutStyle);
        if (foldoutStep5)
        {
            if (GUILayout.Button("Spawn major Objects")) 
            {
                // Implement this function...
            }
        }
        

        // Step 6 (GUI)
        GUILayout.Space(10);
        foldoutStep6 = EditorGUILayout.Foldout(foldoutStep6, "Step 6: Spawn Minor Objects", foldoutStyle);
        if (foldoutStep6)
        {
            if (GUILayout.Button("Spawn Minor Objects")) 
            {
                // Implement this function...
            }
        }

        // Step 7 (GUI)
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

    ///////////////////////////////////////////////////////////////////////////////////////////

        // Step 1 methods
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
                hexGridController.gridSize = gridSize;
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
    


    // Step 2 methods
    private void ParcelGrid(int numberOfCities, int oceanRingRadius, float averageTerrainSectionSize, float terrainSectionVariation)
    {
        SectionController sectionController = FindObjectOfType<SectionController>();
        if (sectionController == null)
        {
            Debug.LogError("No SectionController found in the scene. Please add one to a GameObject.");
            return;
        }
        else 
        {
            sectionController.ParcelGridIntoSections(numberOfCities, oceanRingRadius, averageTerrainSectionSize, terrainSectionVariation);
        }

    }

    private void ClearSections()
    {
        SectionController sectionController = FindObjectOfType<SectionController>();
        if (sectionController == null)
        {
            Debug.LogError("No SectionController found in the scene. Please add one to a GameObject.");
            return;
        }
        else 
        {
            sectionController.ClearSections();
        }
    }



}