using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Calculator.HTTP;

public class HttpServer : IDisposable
{
    private readonly int _port;
    private readonly TcpListener _listener;
    private readonly SemaphoreSlim _semaphore;
    private readonly CancellationTokenSource _cancellationTokenSource;

    private bool _disposed;


    public HttpServer(int port)
    {
        _port = port;
        _listener = new TcpListener(IPAddress.Any, port);
        _semaphore = new SemaphoreSlim(10);
        _cancellationTokenSource = new CancellationTokenSource();
    }

    ~HttpServer()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool isDirectCall)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(HttpServer));
        }

        _listener.Dispose();
        _semaphore.Dispose();
        _cancellationTokenSource.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Start()
    {
        if (!_disposed)
        {
            _listener.Start();
            Task.Run(ListenTask, _cancellationTokenSource.Token);
        }
        else
        {
            throw new ObjectDisposedException(nameof(HttpServer));
        }
    }

    public void Stop()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        _cancellationTokenSource.Cancel();
        _listener.Stop();
    }

}
