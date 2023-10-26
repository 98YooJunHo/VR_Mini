using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    public Material material1;
    public Material material2;
    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    public void ChangeColor()
    {
        StartCoroutine(ChangeMesh());
    }
    public IEnumerator ChangeMesh()
    {
        skinnedMeshRenderer.material = material2;
        
        yield return new WaitForSeconds(0.5f);

        skinnedMeshRenderer.material = material1;
    }
}
