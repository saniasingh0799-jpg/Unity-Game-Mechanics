using UnityEngine;
using System.Collections;

public class _Invisibility : MonoBehaviour
{
    public Material invisibleMaterial;  
    public float invisibleTime = 10f;

    private Renderer playerRenderer;
    private Material originalMaterial;  
    void Start()
    {
        playerRenderer = GetComponent<Renderer>();

        
        originalMaterial = playerRenderer.material;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(MakeInvisible());
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          

    IEnumerator MakeInvisible()
    {
        // Assign invisible material
        playerRenderer.material = invisibleMaterial;

        yield return new WaitForSeconds(invisibleTime);

        // Restore original material
        playerRenderer.material = originalMaterial;
    }
}