using System.Collections;

namespace Scuffle.Core.Coroutine;

public class CoroutineRunner : ICoroutineRunner
{
    private readonly List<ICoroutine> _running = [];
    private readonly List<ICoroutine> _toAdd = [];

    public void Start(ICoroutine coroutine)
    {
        _toAdd.Add(coroutine);
    }

    public void Start(IEnumerator coroutine)
    {
        _toAdd.Add(new Coroutine(coroutine));
    }

    public void Update()
    {
        _running.AddRange(_toAdd);
        _toAdd.Clear();

        for (var i = _running.Count - 1; i >= 0; i--)
        {
            if (!_running[i].MoveNext())
            {
                _running.RemoveAt(i);
            }
        }
    }
}