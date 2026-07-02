using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashTest : MonoBehaviour
{
    public Animator anim;
    public List<Slash> slashes;  

    private Fighter _fighter;

    private int _lastComboStep = 0;
    private bool _slashPlaying = false;

    void Start()
    {
        _fighter = GetComponent<Fighter>();
        DisableSlashes();
    }

    void Update()
    {
        if (_fighter == null) return;

        int currentStep = GetComboStep();

        if (currentStep != _lastComboStep && currentStep > 0 && !_slashPlaying)
        {
            StopAllCoroutines();
            DisableSlashes();
            StartCoroutine(SlashAttack(currentStep));
        }

        _lastComboStep = currentStep;
    }

    int GetComboStep()
    {
        return _fighter.ComboStep;
    }

    IEnumerator SlashAttack(int step)
    {
        _slashPlaying = true;

        int index = Mathf.Clamp(step - 1, 0, slashes.Count - 1);

        yield return new WaitForSeconds(slashes[index].delay);
        slashes[index].slashObj.SetActive(true);

        yield return new WaitForSeconds(0.3f);
        slashes[index].slashObj.SetActive(false);

        _slashPlaying = false;
    }

    void DisableSlashes()
    {
        for (int i = 0; i < slashes.Count; i++)
            slashes[i].slashObj.SetActive(false);
    }
}

[System.Serializable]
public class Slash
{
    public GameObject slashObj;
    public float delay;
}
