using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShader
{
   public static Shader OnHoverAndPick = Shader.Find("Shader Graphs/Outline");

   public static Shader Standard = Shader.Find("Universal Render Pipeline/Lit");

   public static Shader ChangeShaderOfObject(Shader shaderToChange,GameObject ObjectToChange)
    {
       return GetRendererOfObject(ObjectToChange).material.shader = shaderToChange;
    }

   private static Renderer GetRendererOfObject(GameObject Object)
    {
        Renderer RenderOfObject;

        Object.TryGetComponent(out RenderOfObject);

        return RenderOfObject;
    }
}
