using UnityEngine;

public class CameraRenderMode : MonoBehaviour
{
   public Material material;

   void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
   {
    
       material.SetVector("_ScreenResolution", new Vector4(sourceTexture.width, sourceTexture.height, 0.0f, 0.0f));
       Graphics.Blit(sourceTexture, destTexture, material);
      
   }
}
    