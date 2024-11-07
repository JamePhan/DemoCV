using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay
{
    public Sprite Avatar { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsUnlock { get; set; }

    public CharacterDisplay() { }

    public CharacterDisplay(Sprite img, string name, string des, bool unlock)
    {
        this.Avatar = img;
        this.Name = name;
        this.Description = des;
        this.IsUnlock = unlock;
    }
}
