using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace BayatGames.SaveGamePro.Editor
{

    /// <summary>
    /// Save Game Pro General Settings Window.
    /// </summary>
    public class SaveGameGeneralSettings : EditorWindow
    {

        public const string SaveGameProFolder = "Assets/BayatGames/SaveGamePro/";
        public const string PlayFabIntegration = SaveGameProFolder + "Integration/PlayFab/SaveGamePro.unitypackage";
        public const string PlayFabPlayMakerIntegration = SaveGameProFolder + "Integration/PlayFab/PlayMaker.unitypackage";
        public const string FirebaseIntegration = SaveGameProFolder + "Integration/Firebase/SaveGamePro.unitypackage";
        public const string FirebasePlayMakerIntegration = SaveGameProFolder + "Integration/Firebase/PlayMaker.unitypackage";
        public const string PlaymakerIntegration = SaveGameProFolder + "Integration/PlayMaker/SaveGamePro.unitypackage";
        public static readonly string[] SaveGameProFiles = new string[] {
            SaveGameProFolder
        };
        public static readonly string[] PlayFabFiles = new string[] {
            SaveGameProFolder + "Scripts/Networking/SaveGamePlayFab.cs",
            SaveGameProFolder + "Examples/Cloud Saving/Scenes/Cloud Save - PlayFab.unity",
            SaveGameProFolder + "Examples/Cloud Saving/Scripts/PlayFabCloudSave.cs"
        };
        public static readonly string[] PlayFabPlayMakerFiles = new string[] {
            SaveGameProFolder + "PlayMaker/PlayFab"
        };
        public static readonly string[] FirebaseFiles = new string[] {
            SaveGameProFolder + "Scripts/Networking/SaveGameFirebase.cs",
            SaveGameProFolder + "Examples/Cloud Saving/Scenes/Cloud Save - Firebase.unity",
            SaveGameProFolder + "Examples/Cloud Saving/Scripts/FirebaseCloudSave.cs"
        };
        public static readonly string[] FirebasePlayMakerFiles = new string[] {
            SaveGameProFolder + "PlayMaker/Firebase"
        };
        public static readonly string[] PlayMakerFiles = new string[] {
            SaveGameProFolder + "PlayMaker"
        };

        private static GUIStyle m_CenteredText;

        public static GUIStyle CenteredText
        {
            get
            {
                if (m_CenteredText == null)
                {
                    m_CenteredText = new GUIStyle
                    {
                        alignment = TextAnchor.MiddleCenter
                    };
                }
                return m_CenteredText;
            }
        }

        protected string m_Identifier;
        protected string m_FromIdentifer;
        protected string m_ToIdentifier;
        protected Vector2 m_ScrollPosition;

        [MenuItem("Window/Save Game Pro/General Settings")]
        public static void Init()
        {
            SaveGameGeneralSettings window = EditorWindow.GetWindow<SaveGameGeneralSettings>();
            window.minSize = new Vector2(400f, 100f);
            window.Show();
        }

        protected virtual void OnEnable()
        {
            titleContent = new GUIContent(" Settings", Resources.Load<Texture>("savegamepro-icon"));
        }

        protected virtual void OnGUI()
        {
            bool guiEnabled = GUI.enabled;
            m_ScrollPosition = EditorGUILayout.BeginScrollView(
                m_ScrollPosition,
                false,
                true,
                GUIStyle.none,
                GUI.skin.verticalScrollbar,
                GUIStyle.none);
            EditorGUILayout.Separator();
            GUILayout.BeginVertical(
                GUILayout.MinWidth(400f),
                GUILayout.MaxWidth(Screen.width),
                GUILayout.ExpandWidth(true));
            GUILayout.Label("Save Game Pro", EditorStyles.boldLabel);
            GUILayout.Label(
                "Here you can manage general Save Game Pro stuff, such as clearing and browsing save game files and applying simple operations.",
                EditorStyles.wordWrappedLabel);
            GUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.Label("Single Operations", EditorStyles.boldLabel);
            GUILayout.Label("Apply any operation on the specified identifier.");
            GUILayout.BeginHorizontal(
                GUILayout.MinWidth(400f),
                GUILayout.MaxWidth(Screen.width),
                GUILayout.ExpandWidth(true));
            GUILayout.BeginVertical();
            GUILayout.Label("Identifier", CenteredText);
            m_Identifier = EditorGUILayout.TextField(m_Identifier);
            GUILayout.EndVertical();
            GUILayout.BeginVertical(GUILayout.Width(100f));
            GUILayout.Label("Action", CenteredText);
            if (GUILayout.Button("Delete"))
            {
                SaveGame.Delete(m_Identifier);
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            GUILayout.Label("From To Operations", EditorStyles.boldLabel);
            GUILayout.Label("Copy or Move identifier from specified identifier to desired identifier.");
            GUILayout.BeginHorizontal(
                GUILayout.MinWidth(400f),
                GUILayout.MaxWidth(Screen.width),
                GUILayout.ExpandWidth(true));
            GUILayout.BeginVertical();
            GUILayout.Label("From", CenteredText);
            m_FromIdentifer = EditorGUILayout.TextField(m_FromIdentifer);
            GUILayout.EndVertical();
            GUILayout.BeginVertical(GUILayout.Width(100f));
            GUILayout.Label("Action", CenteredText);
            if (GUILayout.Button("Copy"))
            {
                SaveGame.Copy(m_FromIdentifer, m_ToIdentifier);
            }
            if (GUILayout.Button("Move"))
            {
                SaveGame.Move(m_FromIdentifer, m_ToIdentifier);
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Label("To", CenteredText);
            m_ToIdentifier = EditorGUILayout.TextField(m_ToIdentifier);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            GUILayout.Label("General Operations", EditorStyles.boldLabel);
            GUILayout.Label("Clear all, browse saved files and apply general operations.");
            GUILayout.BeginVertical(
                GUILayout.MinWidth(400f),
                GUILayout.MaxWidth(Screen.width),
                GUILayout.ExpandWidth(true),
                GUILayout.Width(200f));
            if (GUILayout.Button("Clear All Saved Data"))
            {
                if (EditorUtility.DisplayDialog(
                         "Clear All Saved Data",
                         "Are you sure you want to clear all saved data?",
                         "Yes, Clear",
                         "No, Cancel"))
                {
                    SaveGame.Clear();
                    EditorUtility.DisplayDialog(
                        "All Saved Data Cleard",
                        "All Saved Data have been cleared.",
                        "Done");
                }
            }
            if (GUILayout.Button("Show Save Path in File Browser"))
            {
                EditorUtility.RevealInFinder(Application.persistentDataPath);
            }
            if (GUILayout.Button("Save Dummy Data"))
            {
                SaveGame.Save("dummy.data", Vector3.zero);
                EditorUtility.DisplayDialog(
                    "Dummy Data Saved",
                    "Dummy Data Saved at " + Application.persistentDataPath + "/dummy.data",
                    "Done");
            }
            if (GUILayout.Button("Uninstall Save Game Pro"))
            {
                UninstallSaveGamePro();
            }
            GUILayout.Label(Application.persistentDataPath);
            GUILayout.EndVertical();

            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(
                GUILayout.MinWidth(400f),
                GUILayout.MaxWidth(Screen.width),
                GUILayout.ExpandWidth(true),
                GUILayout.Width(200f));
            GUILayout.Label("Support & News", EditorStyles.boldLabel);
            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL("https://github.com/BayatGames/SaveGamePro#documentation");
            }
            if (GUILayout.Button("Unity Asset Store"))
            {
                Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/89198");
            }
            if (GUILayout.Button("Itch.io"))
            {
                Application.OpenURL("https://bayat.itch.io/save-game-pro-save-everything");
            }
            if (GUILayout.Button("GitHub"))
            {
                Application.OpenURL("https://github.com/BayatGames/SaveGamePro");
            }
            if (GUILayout.Button("Issue Tracker"))
            {
                Application.OpenURL("https://github.com/BayatGames/SaveGamePro/issues");
            }
            if (GUILayout.Button("Support"))
            {
                Application.OpenURL("https://github.com/BayatGames/Support");
            }
            if (GUILayout.Button("News"))
            {
                Application.OpenURL("https://github.com/BayatGames/Support");
            }
            if (GUILayout.Button("Email"))
            {
                Application.OpenURL("mailto:hasanbayat1393@gmail.com");
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical(
                GUILayout.MinWidth(400f),
                GUILayout.MaxWidth(Screen.width),
                GUILayout.ExpandWidth(true),
                GUILayout.Width(200f));
            GUILayout.Label("Install Integrations", EditorStyles.boldLabel);
            guiEnabled = GUI.enabled;
            GUI.enabled = GUI.enabled && !Installed(PlayFabFiles);
            if (GUILayout.Button("Install PlayFab"))
            {
                Install(PlayFabIntegration, "PlayFab");
            }
            GUI.enabled = guiEnabled;
            guiEnabled = GUI.enabled;
            GUI.enabled = GUI.enabled && !Installed(PlayFabPlayMakerFiles);
            if (GUILayout.Button("Install PlayFab PlayMaker"))
            {
                Install(PlayFabPlayMakerIntegration, "PlayFab PlayMaker");
            }
            GUI.enabled = guiEnabled;
            guiEnabled = GUI.enabled;
            GUI.enabled = GUI.enabled && !Installed(FirebaseFiles);
            if (GUILayout.Button("Install Firebase"))
            {
                Install(FirebaseIntegration, "Firebase");
            }
            GUI.enabled = guiEnabled;
            guiEnabled = GUI.enabled;
            GUI.enabled = GUI.enabled && !Installed(FirebasePlayMakerFiles);
            if (GUILayout.Button("Install Firebase PlayMaker"))
            {
                Install(FirebasePlayMakerIntegration, "Firebase PlayMaker");
            }
            GUI.enabled = guiEnabled;
            guiEnabled = GUI.enabled;
            GUI.enabled = GUI.enabled && !Installed(PlayMakerFiles);
            if (GUILayout.Button("Install PlayMaker"))
            {
                Install(PlaymakerIntegration, "PlayMaker");
            }
            GUI.enabled = guiEnabled;
            guiEnabled = GUI.enabled;
            GUILayout.EndVertical();
            GUILayout.BeginVertical(
                GUILayout.MinWidth(400f),
                GUILayout.MaxWidth(Screen.width),
                GUILayout.ExpandWidth(true),
                GUILayout.Width(200f));
            GUILayout.Label("Uninstall Integrations", EditorStyles.boldLabel);
            GUI.enabled = GUI.enabled && Installed(PlayFabFiles);
            if (GUILayout.Button("Uninstall PlayFab"))
            {
                Uninstall(PlayFabFiles, "PlayFab");
            }
            GUI.enabled = guiEnabled;
            guiEnabled = GUI.enabled;
            GUI.enabled = GUI.enabled && Installed(PlayFabPlayMakerFiles);
            if (GUILayout.Button("Uninstall PlayFab PlayMaker"))
            {
                Uninstall(PlayFabPlayMakerFiles, "PlayFab PlayMaker");
            }
            GUI.enabled = guiEnabled;
            guiEnabled = GUI.enabled;
            GUI.enabled = GUI.enabled && Installed(FirebaseFiles);
            if (GUILayout.Button("Uninstall Firebase"))
            {
                Uninstall(FirebaseFiles, "Firebase");
            }
            GUI.enabled = guiEnabled;
            guiEnabled = GUI.enabled;
            GUI.enabled = GUI.enabled && Installed(FirebasePlayMakerFiles);
            if (GUILayout.Button("Uninstall Firebase PlayMaker"))
            {
                Uninstall(FirebasePlayMakerFiles, "Firebase PlayMaker");
            }
            GUI.enabled = guiEnabled;
            guiEnabled = GUI.enabled;
            GUI.enabled = GUI.enabled && Installed(PlayMakerFiles);
            if (GUILayout.Button("Uninstall PlayMaker"))
            {
                Uninstall(PlayMakerFiles, "PlayMaker");
            }
            GUI.enabled = guiEnabled;
            guiEnabled = GUI.enabled;
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Label("Made with ❤️ by Bayat Games", CenteredText);
            EditorGUILayout.Separator();
            EditorGUILayout.EndScrollView();
        }

        public static void Install(string packagePath, string integration)
        {
            AssetDatabase.ImportPackageCallback callback = null;
            callback = delegate (string packageName)
            {
                EditorUtility.DisplayDialog(
                    integration + " Installed",
                    integration + " Integration Successfully Installed",
                    "Done");
                AssetDatabase.importPackageCompleted -= callback;
            };
            AssetDatabase.importPackageCompleted += callback;
            AssetDatabase.ImportPackage(packagePath, false);
            AssetDatabase.Refresh();
        }

        public static void Uninstall(string[] files, string integration)
        {
            if (EditorUtility.DisplayDialog(
                     "Uninstall " + integration,
                     "Are you sure you want to uninstall the " + integration + " Integration?",
                     "Yes, Uninstall",
                     "No, Cancel"))
            {
                Debug.Log("Uninstalling...");
                if (files == null || files.Length == 0)
                {
                    return;
                }
                bool allDone = true;
                for (int i = 0; i < files.Length; i++)
                {
                    Debug.Log("Deleting " + files[i]);
                    bool result = AssetDatabase.DeleteAsset(files[i]);
                    if (result)
                    {
                        Debug.Log(files[i] + " Deleted Successfully");
                    }
                    else
                    {
                        Debug.LogWarning(files[i] + " Delete Failed");
                    }
                    allDone &= result;
                }
                if (allDone)
                {
                    Debug.Log(integration + " Integration Uninstalled Successfully");
                }
                else
                {
                    Debug.Log(integration + " Integration Partially Uninstalled");
                }
                AssetDatabase.Refresh();
            }
        }

        public static void UninstallSaveGamePro()
        {
            if (EditorUtility.DisplayDialog(
                     "Uninstall Save Game Pro",
                     "Are you sure you want to uninstall the Save Game Pro?",
                     "Yes, Uninstall",
                     "No, Cancel"))
            {
                Debug.Log("Uninstalling...");
                if (SaveGameProFiles == null || SaveGameProFiles.Length == 0)
                {
                    return;
                }
                bool allDone = true;
                for (int i = 0; i < SaveGameProFiles.Length; i++)
                {
                    Debug.Log("Deleting " + SaveGameProFiles[i]);
                    bool result = AssetDatabase.DeleteAsset(SaveGameProFiles[i]);
                    if (result)
                    {
                        Debug.Log(SaveGameProFiles[i] + " Deleted Successfully");
                    }
                    else
                    {
                        Debug.LogWarning(SaveGameProFiles[i] + " Delete Failed");
                    }
                    allDone &= result;
                }
                if (allDone)
                {
                    Debug.Log("Save Game Pro Uninstalled Successfully");
                }
                else
                {
                    Debug.Log("Save Game Pro Partially Uninstalled");
                }
                AssetDatabase.Refresh();
                EditorUtility.DisplayDialog(
                    "Save Game Pro Uninstalled",
                    "Save Game Pro Uninstalled Successfully.",
                    "Done");
            }
        }

        public static bool Installed(string[] files)
        {
            bool installed = true;
            for (int i = 0; i < files.Length; i++)
            {
                installed &= Exists(files[i]);
            }
            return installed;
        }

        public static bool Exists(string path)
        {
            return File.Exists(path) || Directory.Exists(path);
        }

    }

}