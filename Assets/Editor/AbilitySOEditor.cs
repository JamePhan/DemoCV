using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityStatsSO))]
public class AbilitySOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        AbilityStatsSO scriptableObject = (AbilityStatsSO)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("AbilityType"));

        switch (scriptableObject.AbilityType)
        {
            case AbilityType.Damage:
                scriptableObject.abilityDamage = EditorGUILayout.IntField("Damage", scriptableObject.abilityDamage);
                scriptableObject.abilityRadiusDamage = EditorGUILayout.IntField("Radius", scriptableObject.abilityRadiusDamage);
                break;

            case AbilityType.BuffShield:
                scriptableObject.abilityShieldAmount = EditorGUILayout.IntField("Shield Amount", scriptableObject.abilityShieldAmount);
                break;

            case AbilityType.BuffSpeed:
                scriptableObject.abilitySpeedAmount = EditorGUILayout.FloatField("Speed Amount", scriptableObject.abilitySpeedAmount);
                break;

            case AbilityType.CrowdControl:
                scriptableObject.abilityRadiusCC = EditorGUILayout.FloatField("Radius", scriptableObject.abilityRadiusCC);
                break;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }


}
