using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EnemyCreatorWindow : EditorWindow
{
    string enemyName = "";
    string path = "";
    string textToWrite;

    // Add a new state here
    public state[] states =
    {
        new state("MoveState", true),
        new state("IdleState", true),
        new state("PlayerDetectedState", true),
        new state("ChargeState", true),
        new state("MeleeAttackState", true),
        new state("LookForPlayerState", true),
        new state("StunState", true),
        new state("DeadState", true),
        new state("DodgeState", true),
        new state("RangedAttackState", true),
        new state("TeleportState", true)
    };

    [MenuItem("Window/Enemy Maker")]
    public static void ShowWindow()
    {
        GetWindow<EnemyCreatorWindow>("Enemy Maker");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create an enemy.", EditorStyles.boldLabel);

        enemyName = EditorGUILayout.TextField("Enemy Name", enemyName);

        // Create a folder for all enemy states.
        GUILayout.Label($"Path: {path}");
        path = $"Assets/Game/Scripts/Enemies/EnemySpecific/{enemyName}";

        // Display all states
        for (int i = 0; i < states.Length; i++)
        {
            states[i].value = EditorGUILayout.Toggle(states[i].name, states[i].value);
        }

        // Check for empty name, null path, or existing directory
        if (string.IsNullOrEmpty(enemyName))
        {
            GUILayout.Label("--- Type in an enemy name. ---", EditorStyles.whiteLargeLabel);
            return;
        }
        else if (Directory.Exists(path))
        {
            GUILayout.Label("--- Enemy already exists. Change the Enemy name. ---", EditorStyles.whiteLargeLabel);
            return;
        }

        // Display the create button if enemyName is not empty.
        else
        {
            if (GUILayout.Button("Create Enemy"))
            {
                //Create main and data folders.
                Directory.CreateDirectory(path);
                Directory.CreateDirectory($"{path}/Data");

                //Create Base Data
                textToWrite = File.ReadAllText("Assets/Game/Scripts/Editor/Enemy Maker/enemyBase.txt");
                CreateEnemyBase();
                
                //Create State files 
                textToWrite = File.ReadAllText("Assets/Game/Scripts/Editor/Enemy Maker/enemyStateData.txt");
                foreach (var state in states)
                {
                    // Create state scripts if true
                    if (state.value)
                    {
                        CreateEnemyStates(state);
                    }                     
                }

                //Create Data files
                CreateEnemyData(); 

                AssetDatabase.Refresh();
                Debug.Log($"{enemyName} created.");
            }
        }
    }

    private void CreateEnemyStates(state state)
    {
        string baseString = (state.name == "MeleeAttackState" || state.name == "RangedAttackState") ?

        "    public " + enemyName + "_" + state.name + "(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, " +
        "D_" + state.name + " stateData, " + enemyName + " enemy) : " + "base(etity, stateMachine, animBoolName, attackPosition, stateData)" :

        "    public " + enemyName + "_" + state.name + "(Entity etity, FiniteStateMachine stateMachine, string animBoolName, " +
        "D_" + state.name + " stateData, " + enemyName + " enemy) : " + "base(etity, stateMachine, animBoolName, stateData)";

        using (StreamWriter outfile =
                            new StreamWriter($"{path}/{enemyName}_{state.name}.cs"))
        {
            outfile.WriteLine("using System.Collections;");
            outfile.WriteLine("using System.Collections.Generic;");
            outfile.WriteLine("using UnityEngine;");
            outfile.WriteLine("");
            outfile.WriteLine("public class " + enemyName + "_" + state.name + " : " + state.name);
            outfile.WriteLine("{");
            outfile.WriteLine("    private " + enemyName + " enemy;");
            outfile.WriteLine(" ");
            outfile.WriteLine(baseString);
            outfile.Write(textToWrite);//The extra text.
        }
    }

    private void CreateEnemyBase()
    {
        using (StreamWriter outfile =
                            new StreamWriter($"{path}/{enemyName}.cs"))
        {
            // Write the headers
            outfile.WriteLine("using System.Collections;");
            outfile.WriteLine("using System.Collections.Generic;");
            outfile.WriteLine("using UnityEngine;");
            outfile.WriteLine("");
            outfile.WriteLine("public class " + enemyName + " : Entity");
            outfile.WriteLine("{");
            // For each state, write the public reference
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i].value)
                {
                    outfile.WriteLine("    public " + enemyName + "_" + states[i].name + " " + StringHelper.FirstCharToLowerCase(states[i].name) + " { get; private set; }");
                }
            }
            outfile.WriteLine("");
            // For each state, write the serializefield state data
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i].value)
                {
                    outfile.WriteLine("    [SerializeField]");
                    outfile.WriteLine("    private D_" + states[i].name + " " + StringHelper.FirstCharToLowerCase(states[i].name) + "Data;");
                }
            }
            outfile.WriteLine("");
            // If melee attack state, write meleeAttackPosition. If ranged, write ranged attack position.
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i].name == "MeleeAttackState" && states[i].value)
                {
                    outfile.WriteLine("    [SerializeField]");
                    outfile.WriteLine("    private Transform meleeAttackPosition;");
                }

                if (states[i].name == "RangedAttackState" && states[i].value)
                {
                    outfile.WriteLine("    [SerializeField]");
                    outfile.WriteLine("    private Transform rangedAttackPosition;");
                }
            }
            outfile.WriteLine("");
            outfile.WriteLine("    public override void Awake()");
            outfile.WriteLine("{");
            outfile.WriteLine("        base.Awake();");
            outfile.WriteLine("");
            // Write the awake assignments
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i].value)
                {
                    string lowerCaseFirst = StringHelper.FirstCharToLowerCase(states[i].name);
                    string animName = lowerCaseFirst.Substring(0, states[i].name.Length - 5);
                    if (states[i].name == "RangedAttackState" || states[i].name == "MeleeAttackState")
                    {
                        outfile.WriteLine("        " + lowerCaseFirst + " = new " + enemyName + "_" + states[i].name + "(this, stateMachine, \"" + animName + "\", " + animName + "Position, " + lowerCaseFirst + "Data, this);");
                    }
                    else outfile.WriteLine("        " + lowerCaseFirst + " = new " + enemyName + "_" + states[i].name + "(this, stateMachine, \"" + animName + "\", " + lowerCaseFirst + "Data, this);");
                }
            }
            outfile.WriteLine("}");
            outfile.Write(textToWrite);
        }
    }

    // Write the new states below.
    private void CreateEnemyData()
    {
        D_Entity BaseData = CreateInstance<D_Entity>();
        AssetDatabase.CreateAsset(BaseData, $"{path}/Data/" + enemyName + "_" + "BaseData.asset");

        D_MoveState MoveState = CreateInstance<D_MoveState>();
        D_IdleState IdleState = CreateInstance<D_IdleState>();
        D_PlayerDetectedState PlayerDetectedState = CreateInstance<D_PlayerDetectedState>();
        D_ChargeState ChargeState = CreateInstance<D_ChargeState>();
        D_MeleeAttackState MeleeAttackState = CreateInstance<D_MeleeAttackState>();
        D_LookForPlayerState LookForPlayerState = CreateInstance<D_LookForPlayerState>();
        D_StunState StunState = CreateInstance<D_StunState>();
        D_DeadState DeadState = CreateInstance<D_DeadState>();
        D_DodgeState DodgeState = CreateInstance<D_DodgeState>();
        D_RangedAttackState RangedAttackState = CreateInstance<D_RangedAttackState>();
        D_TeleportState TeleportState = CreateInstance<D_TeleportState>();

        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].value)
            {
                if (states[i].name == "MoveState") AssetDatabase.CreateAsset(MoveState, $"{path}/Data/" + enemyName + "_" + states[i].name + ".asset");
                else if (states[i].name == "IdleState") AssetDatabase.CreateAsset(IdleState, $"{path}/Data/" + enemyName + "_" + states[i].name + ".asset");
                else if (states[i].name == "PlayerDetectedState") AssetDatabase.CreateAsset(PlayerDetectedState, $"{path}/Data/" + enemyName + "_" + states[i].name + ".asset");
                else if (states[i].name == "ChargeState") AssetDatabase.CreateAsset(ChargeState, $"{path}/Data/" + enemyName + "_" + states[i].name + ".asset");
                else if (states[i].name == "MeleeAttackState") AssetDatabase.CreateAsset(MeleeAttackState, $"{path}/Data/" + enemyName + "_" + states[i].name + ".asset");
                else if (states[i].name == "LookForPlayerState") AssetDatabase.CreateAsset(LookForPlayerState, $"{path}/Data/" + enemyName + "_" + states[i].name + ".asset");
                else if (states[i].name == "StunState") AssetDatabase.CreateAsset(StunState, $"{path}/Data/" + enemyName + "_" + states[i].name + ".asset");
                else if (states[i].name == "DeadState") AssetDatabase.CreateAsset(DeadState, $"{path}/Data/" + enemyName + "_" + states[i].name + ".asset");
                else if (states[i].name == "DodgeState") AssetDatabase.CreateAsset(DodgeState, $"{path}/Data/" + enemyName + "_" + states[i].name + ".asset");
                else if (states[i].name == "RangedAttackState") AssetDatabase.CreateAsset(RangedAttackState, $"{path}/Data/" + enemyName + "_" + states[i].name + ".asset");
                else if (states[i].name == "TeleportState") AssetDatabase.CreateAsset(TeleportState, $"{path}/Data/" + enemyName + "_" + states[i].name + ".asset");
                else Debug.LogWarning(states[i].name + " has no state data object!");
            }
        }
    }
}

public struct state
{
    public string name;
    public bool value;

    public state(string name, bool value)
    {
        this.name = name;
        this.value = value;
    }
}
