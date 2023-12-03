using System.Collections.Generic;
using Match3.Grid.GridData;
using Match3.Services.AssetManagement;
using UnityEditor;
using UnityEngine;

namespace Match3.LevelEditor
{
    public class LevelEditorGridSettingsDrawer
    {
        private int _levelObjectiveCount;
        
        public void DrawGridSettings(LevelData levelData, ref int levelSizeX, ref int levelSizeY)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical();
                {
                    DrawLevelSizeSettings(levelData, ref levelSizeX, ref levelSizeY);
                    DrawLevelObjectives(levelData);
                } 
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawLevelSizeSettings(LevelData levelData, ref int levelSizeX, ref int levelSizeY)
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                
                GUILayout.Label("Size X", GUILayout.Width(50));
                EditorGUI.BeginChangeCheck();
                levelSizeX = EditorGUILayout.IntSlider(levelSizeX, 3, 12, GUILayout.MaxWidth(150));
                if (EditorGUI.EndChangeCheck())
                {
                    AssetManager.LevelEditor.ResetLevelDataWithNewSize(levelData, levelSizeX, levelSizeY);
                }
                
                GUILayout.FlexibleSpace();
                
                
                EditorGUI.BeginChangeCheck();

                GUILayout.Label("Size Y", GUILayout.Width(50));
                levelSizeY = EditorGUILayout.IntSlider(levelSizeY, 3, 12, GUILayout.MaxWidth(150));
                if (EditorGUI.EndChangeCheck())
                {
                    AssetManager.LevelEditor.ResetLevelDataWithNewSize(levelData, levelSizeX, levelSizeY);

                }
                
                GUILayout.FlexibleSpace();

            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawLevelObjectives(LevelData levelData)
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            {
                levelData.levelMoveCount = EditorGUILayout.IntField("Move Count", levelData.levelMoveCount);
                if (levelData.levelMoveCount < 0) levelData.levelMoveCount = 1;
                GUILayout.FlexibleSpace();

            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label("Level Objectives");
                EditorGUILayout.Space();
                
                EditorGUI.BeginChangeCheck();
                if (GUILayout.Button("Add Objective",GUILayout.MaxWidth(100)))
                {
                    if (levelData.ObjectiveDataList.Count < levelData.GetMaxObjectiveCount())
                    {
                        levelData.ObjectiveDataList.Add(new ObjectiveData());
                    }
                }
                
                EditorGUILayout.Space();
                
                GUILayout.BeginVertical();
                {
                    for (int i = 0; i < levelData.ObjectiveDataList.Count; i++)
                    {
                        GUILayout.BeginVertical();
                        {
                            EditorGUI.BeginChangeCheck();
                            levelData.ObjectiveDataList[i].ObjectiveType = (BoardElementType)EditorGUILayout.EnumPopup("Type", (BoardElementType)levelData.ObjectiveDataList[i].ObjectiveType,
                                    GUILayout.MaxWidth(350));

                            EditorGUI.BeginChangeCheck();
                            levelData.ObjectiveDataList[i].Count = EditorGUILayout.IntField("Objective Count",
                                levelData.ObjectiveDataList[i].Count, GUILayout.MaxWidth(200));
                        }
                        GUILayout.EndVertical();
                    
                        GUILayout.BeginHorizontal();
                        {
                            if (GUILayout.Button("-", GUILayout.MaxWidth(30)))
                            {
                                levelData.ObjectiveDataList.RemoveAt(i);
                            }
                        }
                        GUILayout.EndHorizontal();
                        
                        EditorGUILayout.Space();
                    }
                }
                GUILayout.EndVertical();
                
                GUILayout.FlexibleSpace();
            } 
            EditorGUILayout.EndHorizontal();
            
        }


    }
}