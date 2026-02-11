namespace Scuffle.Core;

public interface ICoroutineRunner
{
    void Start(ICoroutine coroutine);
    void Update();
}