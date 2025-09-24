using System.Collections;

namespace Suzdaltsev.Domain;

public class ConcurrentList<T> : IEnumerable<T>, ICollection<T>
{
    private readonly List<T> _list = new List<T>();
    private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }

    public int Count
    {
        get
        {
            try
            {
                _lock.EnterReadLock();
                return _list.Count;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
    
    public bool IsReadOnly => false;

    public void Add(T item)
    {
        try
        {
            _lock.EnterWriteLock();
            _list.Add(item);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public void Clear()
    {
        try
        {
            _lock.EnterWriteLock();
            _list.Clear();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public bool Contains(T item)
    {
        try
        {
            _lock.EnterReadLock();
            return _list.Contains(item);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        try
        {
            _lock.EnterReadLock();
            _list.CopyTo(array, arrayIndex);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }


    public IEnumerator<T> GetEnumerator()
    {
        try
        {
            _lock.EnterReadLock();
            return _list.GetEnumerator();
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        try
        {
            _lock.EnterReadLock();
            return _list.GetEnumerator();
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
}