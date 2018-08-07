using UnityEngine.UI;

public class EntityHealthbar : Entity {

    public Image healthbar;

    public override float Health
    {
        get { return base.Health; }

        protected set
        {
            base.Health = value;
            UpdateHealthbar();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        UpdateHealthbar();
    }


    public void UpdateHealthbar()
    {
        healthbar.fillAmount = Health / startingHealth;
    }
}
