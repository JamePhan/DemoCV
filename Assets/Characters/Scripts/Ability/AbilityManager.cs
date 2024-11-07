using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public delegate void AbilityHolderDelegate();
    public static event AbilityHolderDelegate abilityReadyDelegete;
    public static event AbilityHolderDelegate abilityActiveDelegete;
    public static event AbilityHolderDelegate abilityCooldownDelegete;

    public Button btn_ability_1;
    public Button btn_ability_2;
    public Image img_skill1;
    public Image img_skill2;
    public GameObject reload_1;
    public GameObject reload_2;
    public Image img_reload_1;
    public Image img_reload_2;
    public TextMeshProUGUI text_reload_1;
    public TextMeshProUGUI text_reload_2;
    
    public Ability ability_1;
    public Ability ability_2;

    public Character character;
    public AbilityFactory abiFactory1;
    public AbilityFactory abiFactory2;

    public void Init(Character character)
    {
        this.character = character;
        abiFactory1 = new AbilityFactory(img_skill1, btn_ability_1, reload_1, img_reload_1, text_reload_1);
        Ability ability_1 = abiFactory1.CreateAbility(character.Ability1.ToString());
        abiFactory2 = new AbilityFactory(img_skill2, btn_ability_2, reload_2, img_reload_2, text_reload_2);
        Ability ability_2 = abiFactory2.CreateAbility(character.Ability2.ToString());

    }

    private void FixedUpdate()
    {
        AbilitySystem();
    }

    private void AbilitySystem()
    {
        if (abilityReadyDelegete != null) abilityReadyDelegete();
        if (abilityActiveDelegete != null) abilityActiveDelegete();
        if (abilityCooldownDelegete != null) abilityCooldownDelegete();
    }
}
