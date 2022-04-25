using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcCam : MonoBehaviour
{
    public Material postProcMat;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, postProcMat);
    }
}
