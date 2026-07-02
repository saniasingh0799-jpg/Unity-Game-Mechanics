using UnityEngine;

public class Fighter : MonoBehaviour
{
    [Header("Combo Settings")]
    public float maxComboDelay = 0.5f;

    [Header("References")]
    public Animator animator;

    public bool IsAttacking => comboStep > 0;
    public int ComboStep => comboStep;

    private int comboStep = 0;
    private float attackTimer = 0f;
    private bool inputQueued = false;
    private bool isAttacking = false;

    private const float DUR1 = 0.583f;
    private const float DUR2 = 0.917f;
    private const float DUR3 = 1.083f;

    private static readonly int H1 = Animator.StringToHash("hit1");
    private static readonly int H2 = Animator.StringToHash("hit2");
    private static readonly int H3 = Animator.StringToHash("hit3");
    private static readonly int HIdle = Animator.StringToHash("idle");

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking && comboStep == 0)
                StartAttack(1);
            else if (isAttacking && comboStep < 3)
                inputQueued = true;
            else if (!isAttacking && comboStep > 0 && comboStep < 3)
                StartAttack(comboStep + 1);
        }

        TickTimers();
    }

    void StartAttack(int step)
    {
        comboStep = step;
        isAttacking = true;
        inputQueued = false;

        animator.ResetTrigger(H1);
        animator.ResetTrigger(H2);
        animator.ResetTrigger(H3);

        float dur;
        switch (step)
        {
            case 1: animator.SetTrigger(H1); dur = DUR1; break;
            case 2: animator.SetTrigger(H2); dur = DUR2; break;
            case 3: animator.SetTrigger(H3); dur = DUR3; break;
            default: dur = 1f; break;
        }

        attackTimer = dur;
    }

    void TickTimers()
    {
        if (comboStep == 0 || !isAttacking) return;

        attackTimer -= Time.deltaTime;
        if (attackTimer > 0f) return;

        isAttacking = false;

        if (inputQueued && comboStep < 3)
            StartAttack(comboStep + 1);
        else
            ResetCombo();
    }

    void ResetCombo()
    {
        comboStep = 0;
        isAttacking = false;
        inputQueued = false;
        attackTimer = 0f;

        animator.ResetTrigger(H1);
        animator.ResetTrigger(H2);
        animator.ResetTrigger(H3);
        animator.SetTrigger(HIdle);
    }
}
