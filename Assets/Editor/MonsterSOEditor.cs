using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterSO))]
public class MonsterSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        MonsterSO scriptableObject = (MonsterSO)target;

        scriptableObject.skin = EditorGUILayout.ColorField("Skin Color", scriptableObject.skin);
        scriptableObject.health = EditorGUILayout.IntField("Health", scriptableObject.health);
        scriptableObject.damage = EditorGUILayout.IntField("Damage", scriptableObject.damage);
        scriptableObject.cooldownAttack = EditorGUILayout.IntField("Cooldown Attack", scriptableObject.cooldownAttack);
        scriptableObject.moveSpeed = EditorGUILayout.FloatField("Move Speed", scriptableObject.moveSpeed);
        scriptableObject.experience = EditorGUILayout.IntField("Experience", scriptableObject.experience);
        scriptableObject.haveAbility = EditorGUILayout.Toggle("Have Ability", scriptableObject.haveAbility);

        if (scriptableObject.haveAbility)
        {
            scriptableObject.cooldownTime = EditorGUILayout.FloatField("Cooldown Time", scriptableObject.cooldownTime);
            scriptableObject.durationTime = EditorGUILayout.FloatField("Duration Time", scriptableObject.durationTime);
            scriptableObject.detectionRange = EditorGUILayout.FloatField("Detection Range", scriptableObject.detectionRange);
            scriptableObject.ability = (MonsterAbilities)EditorGUILayout.EnumPopup("Ability", scriptableObject.ability);
            scriptableObject.vfxExecute = (GameObject)EditorGUILayout.ObjectField("VFX Prefab", scriptableObject.vfxExecute, typeof(GameObject), false);

            switch (scriptableObject.ability)
            {
                case MonsterAbilities.HurryUp:
                    scriptableObject.abilitySpeedBuff = EditorGUILayout.FloatField("Speed Buff Amount", scriptableObject.abilitySpeedBuff);
                    scriptableObject.abilityRadius = EditorGUILayout.FloatField("Radius", scriptableObject.abilityRadius);
                    break;

                case MonsterAbilities.Exploded:
                    scriptableObject.abilityDamage = EditorGUILayout.IntField("Damage Amount", scriptableObject.abilityDamage);
                    scriptableObject.abilityRadius = EditorGUILayout.FloatField("Radius", scriptableObject.abilityRadius);
                    break;

                case MonsterAbilities.BlueLine:
                    scriptableObject.abilityDamage = EditorGUILayout.IntField("Damage Amount", scriptableObject.abilityDamage);
                    scriptableObject.abilityRadius = EditorGUILayout.FloatField("Radius", scriptableObject.abilityRadius);
                    break;
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
