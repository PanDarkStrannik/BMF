using System;
public interface IStanable
{
    event Action<IStanable> OnStanStart;
    event Action<IStanable> OnStanEnd;
    bool CanStan { get; }
    void Stan(StanData stanData);
}
