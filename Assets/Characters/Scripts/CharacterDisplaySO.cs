using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterDisplay", menuName = "Character Display")]
public class CharacterDisplaySO : ScriptableObject
{
    public Sprite Avatar;

    public string Name;

    public string Description;

    public bool IsUnlock;
}
