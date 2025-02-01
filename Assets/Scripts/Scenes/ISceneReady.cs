using System;

public interface ISceneReady
{
    bool IsReady { get; }
    event Action OnSceneReady;
}