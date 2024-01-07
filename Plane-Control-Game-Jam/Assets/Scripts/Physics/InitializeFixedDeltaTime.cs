using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Decides the fixed delta time setting to avoid jittering in physics.
/// 
/// Jitter happens when the number of physics ticks (fixed updates) isn't 
/// consistent between frames.
/// 
/// With vsync, if performance isn't an issue, the time per frame should be the
/// screen's refresh interval. To reduce jitter, aim for 2 fixed updates per frame.
/// </summary>
public class InitializeFixedDeltaTime : MonoBehaviour
{
    // Earlier in script execution order.

    private void Awake()
    {
        // This isn't the exact refresh rate b/c it gives an int, e.g. if it says 60 Hz, the monitor
        // might actually be 59.94 Hz. We can fix this if we upgrade to 2022.2 and use refreshRateRatio.
        // Might not be a big deal having 1 jitter every 15 seconds or so.
        
        // I can't test this issue because my monitor is exactly 60 Hz. If it feels bad on some machines,
        // and we don't want to upgrade versions, we could switch to a manual physics simulation loop and 
        // fix the issue.

        double frameRate = Screen.currentResolution.refreshRate;

        int physicsTicksPerFrame = frameRate < 120 ? 2 : 1;
        double physicsRate = frameRate * physicsTicksPerFrame;
        Time.fixedDeltaTime = (float)(1 / physicsRate);
    }
}

/*

/// <summary>
/// Rather than letting Unity decide how many physics ticks to do each frame, do that manually.
/// The point is to avoid jitter, which happens when the number of physics ticks isn't the same
/// each frame.
/// </summary>
public class PhysicsSimulationLoop : MonoBehaviour
{
    // Earlier in execution order.

    [SerializeField]
    private int _ticksPerFrame = 2;
    [SerializeField]
    private int _maxTicksInOneFrame = 10;

    private double _ticksAccumulator = .5;

    private void Awake()
    {
        // This isn't the exact refresh rate b/c it gives an int, e.g. if it says 60 Hz, the monitor
        // might actually be 59.94 Hz. We can fix this if we upgrade to 2022.2 and use refreshRateRatio.
        // But e.g. 1 jitter every 10 seconds might not be noticeable.
        double frameRate = Screen.currentResolution.refreshRate;

        double physicsRate = _ticksPerFrame * frameRate;
        Time.fixedDeltaTime = (float)(1 / physicsRate);

        Physics.autoSimulation = false;
    }

    private void Update()
    {
        _ticksAccumulator += Time.deltaTime / Time.fixedDeltaTime;
        int ticks = (int)_ticksAccumulator;
        _ticksAccumulator -= ticks;

        // The Time.fixedDeltaTime may be slightly different from what's needed to 
        // ensure constant ticks per frame, so adjust _ticksAccumulator towards .5
        if (_ticksAccumulator < .5)
        {
            _ticksAccumulator = System.Math.Min(.5, _ticksAccumulator + .01);
        }
        else
        {
            _ticksAccumulator = System.Math.Max(.5, _ticksAccumulator - .01);
        }

        ticks = System.Math.Min(ticks, _maxTicksInOneFrame);

        Debug.Log(ticks);

        for (int i = 0; i < ticks; i++)
        {
            Physics.Simulate(Time.fixedDeltaTime);
        }
    }
}
*/