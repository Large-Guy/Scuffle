using System.Collections;

namespace Scuffle.Core.Coroutine;

public class Coroutine : ICoroutine
{
    private readonly IEnumerator _enumerator;
    private ICoroutine? _current = null;

    public Coroutine(IEnumerator enumerator)
    {
        _enumerator = enumerator;
    }

    public bool MoveNext()
    {
        if (_current != null)
        {
            if (_current.MoveNext())
                return true;

            _current = null;
        }

        if (!_enumerator.MoveNext())
            return false;

        var yielded = _enumerator.Current;

        switch (yielded)
        {
            case ICoroutine coroutine:
            {
                _current = coroutine;
                return true;
            }
            case IEnumerator enumerator:
            {
                _current = new Coroutine(enumerator);
                return true;
            }
            case null:
            {
                return true;
            }
            default:
            {
                return true;
            }
        }
    }
}