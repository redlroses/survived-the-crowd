using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class RenderQueueSetter : MonoBehaviour
{
    [SerializeField] private List<Renderer> _renderers = new List<Renderer>();
    [SerializeField] private int _renderQueue;

    private void Start()
    {
        SetRenderQueue(_renderQueue);
    }

    private void SetRenderQueue(int renderQueue)
    {
        if (renderQueue < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(renderQueue));
        }

        foreach (var material in _renderers.SelectMany(render => render.materials))
        {
            material.renderQueue = _renderQueue;
        }
    }
}