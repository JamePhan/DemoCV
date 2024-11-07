using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability")]
public class AbilitySO : ScriptableObject
{
    public Sprite skillSprite;

    public string nameAbility;

    public float cooldownTime;

    public float durationTime;

    public string keyBoard;

    public GameObject objectSpawm;

    public GameObject effectVFX;
}