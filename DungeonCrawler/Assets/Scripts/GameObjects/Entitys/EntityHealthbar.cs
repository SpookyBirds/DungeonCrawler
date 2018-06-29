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

    public void UpdateHealthbar()
    {
        healthbar.fillAmount = Health / startingHealth;
    }
}
