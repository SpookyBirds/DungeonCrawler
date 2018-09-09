using UnityEngine.UI;
using UnityEngine;

public class EntityHealthbar : Entity {

    //[SerializeField] [Tooltip("Main Collider will be disabled as soon as hp hit 0, not after the death animation finishes")]
    //private Collider mainCollider;

    [SerializeField] [Tooltip("GUI will be disabled as soon as hp hit 0, not after the death animation finishes")]
    private GameObject GUI;

    private Collider mainCollider;

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
        mainCollider = GetComponent<Collider>();
    }

    protected override void Update()
    {
        if (animator.GetBool("Death"))
        {
            mainCollider.enabled = false;
            GUI.SetActive(false);
        }

        if (animator.GetBool("Remove"))
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealthbar()
    {
        healthbar.fillAmount = Health / startingHealth;
    }
}
