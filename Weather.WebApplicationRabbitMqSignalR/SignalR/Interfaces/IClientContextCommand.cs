using Microsoft.AspNetCore.SignalR;

namespace Weather.WebApplicationRabbitMqSignalR.SignalR.Interfaces;

/// <summary>
/// Обёртка для выполнения запросов SignalR
/// </summary>
public interface IClientContextCommand
{
    /// <summary>
    /// Invokes a method on the connection(s) represented by the <see cref="IClientProxy"/> instance
    /// </summary>
    /// <param name="clientProxy">The <see cref="IClientProxy"/></param>
    /// <param name="method">The name of method to invoke.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default
    /// value is <see cref="CancellationToken.None"/> </param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous invoke </returns>
    Task SendAsync(IClientProxy clientProxy, string method, CancellationToken cancellationToken = default);

    /// <summary>
    /// Invokes a method on the connection(s) represented by the <see cref="IClientProxy"/> instance
    /// </summary>
    /// <param name="clientProxy">The <see cref="IClientProxy"/></param>
    /// <param name="method">The name of method to invoke.</param>
    /// <param name="arg1">The first argument</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default
    /// value is <see cref="CancellationToken.None"/> </param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous invoke </returns>
    Task SendAsync(IClientProxy clientProxy, string method, object arg1, CancellationToken cancellationToken = default);

    /// <summary>
    /// Invokes a method on the connection(s) represented by the <see cref="IClientProxy"/> instance
    /// </summary>
    /// <param name="clientProxy">The <see cref="IClientProxy"/></param>
    /// <param name="method">The name of method to invoke.</param>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default
    /// value is <see cref="CancellationToken.None"/> </param>    
    /// <returns>A <see cref="Task"/> that represents the asynchronous invoke </returns>
    Task SendAsync(IClientProxy clientProxy, string method, object arg1, object arg2, CancellationToken cancellationToken = default);/// <summary>
    
    /// <summary>
    /// Invokes a method on the connection(s) represented by the <see cref="IClientProxy"/> instance
    /// </summary>
    /// <param name="clientProxy">The <see cref="IClientProxy"/></param>
    /// <param name="method">The name of method to invoke.</param>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <param name="arg3">The third argument</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default
    /// value is <see cref="CancellationToken.None"/> </param>    
    /// <returns>A <see cref="Task"/> that represents the asynchronous invoke </returns>
    Task SendAsync(IClientProxy clientProxy, string method, object arg1, object arg2, object arg3, CancellationToken cancellationToken = default);/// <summary>
   
    /// <summary>
    /// Invokes a method on the connection(s) represented by the <see cref="IClientProxy"/> instance
    /// </summary>
    /// <param name="clientProxy">The <see cref="IClientProxy"/></param>
    /// <param name="method">The name of method to invoke.</param>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <param name="arg3">The third argument</param>
    /// <param name="arg4">The fourth argument</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default
    /// value is <see cref="CancellationToken.None"/> </param>    
    /// <returns>A <see cref="Task"/> that represents the asynchronous invoke </returns>
    Task SendAsync(IClientProxy clientProxy, string method, object arg1, object arg2, object arg3, object arg4, CancellationToken cancellationToken = default);/// <summary>
    
    /// <summary>
    /// Invokes a method on the connection(s) represented by the <see cref="IClientProxy"/> instance
    /// </summary>
    /// <param name="clientProxy">The <see cref="IClientProxy"/></param>
    /// <param name="method">The name of method to invoke.</param>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <param name="arg3">The third argument</param>
    /// <param name="arg4">The fourth argument</param>
    /// <param name="arg5">The fifth argument</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default
    /// value is <see cref="CancellationToken.None"/> </param>    
    /// <returns>A <see cref="Task"/> that represents the asynchronous invoke </returns>
    Task SendAsync(IClientProxy clientProxy, string method, object arg1, object arg2, object arg3, object arg4, object arg5, CancellationToken cancellationToken = default);/// <summary>
    
    /// <summary>
    /// Invokes a method on the connection(s) represented by the <see cref="IClientProxy"/> instance
    /// </summary>
    /// <param name="clientProxy">The <see cref="IClientProxy"/></param>
    /// <param name="method">The name of method to invoke.</param>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <param name="arg3">The third argument</param>
    /// <param name="arg4">The fourth argument</param>
    /// <param name="arg5">The fifth argument</param>
    /// <param name="arg6">The sixth argument</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default
    /// value is <see cref="CancellationToken.None"/> </param>    
    /// <returns>A <see cref="Task"/> that represents the asynchronous invoke </returns>
    Task SendAsync(IClientProxy clientProxy, string method, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, CancellationToken cancellationToken = default);/// <summary>
    
    /// <summary>
    /// Invokes a method on the connection(s) represented by the <see cref="IClientProxy"/> instance
    /// </summary>
    /// <param name="clientProxy">The <see cref="IClientProxy"/></param>
    /// <param name="method">The name of method to invoke.</param>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <param name="arg3">The third argument</param>
    /// <param name="arg4">The fourth argument</param>
    /// <param name="arg5">The fifth argument</param>
    /// <param name="arg6">The sixth argument</param>
    /// <param name="arg7">The seventh argument</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default
    /// value is <see cref="CancellationToken.None"/> </param>    
    /// <returns>A <see cref="Task"/> that represents the asynchronous invoke </returns>
    Task SendAsync(IClientProxy clientProxy, string method, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, CancellationToken cancellationToken = default);/// <summary>
    
    /// <summary>
    /// Invokes a method on the connection(s) represented by the <see cref="IClientProxy"/> instance
    /// </summary>
    /// <param name="clientProxy">The <see cref="IClientProxy"/></param>
    /// <param name="method">The name of method to invoke.</param>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <param name="arg3">The third argument</param>
    /// <param name="arg4">The fourth argument</param>
    /// <param name="arg5">The fifth argument</param>
    /// <param name="arg6">The sixth argument</param>
    /// <param name="arg7">The seventh argument</param>
    /// <param name="arg8">The eighth argument</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default
    /// value is <see cref="CancellationToken.None"/> </param>    
    /// <returns>A <see cref="Task"/> that represents the asynchronous invoke </returns>
    Task SendAsync(IClientProxy clientProxy, string method, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, CancellationToken cancellationToken = default);/// <summary>
    
    /// <summary>
    /// Invokes a method on the connection(s) represented by the <see cref="IClientProxy"/> instance
    /// </summary>
    /// <param name="clientProxy">The <see cref="IClientProxy"/></param>
    /// <param name="method">The name of method to invoke.</param>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <param name="arg3">The third argument</param>
    /// <param name="arg4">The fourth argument</param>
    /// <param name="arg5">The fifth argument</param>
    /// <param name="arg6">The sixth argument</param>
    /// <param name="arg7">The seventh argument</param>
    /// <param name="arg8">The eighth argument</param>
    /// <param name="arg9">The ninth argument</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default
    /// value is <see cref="CancellationToken.None"/> </param>    
    /// <returns>A <see cref="Task"/> that represents the asynchronous invoke </returns>
    Task SendAsync(IClientProxy clientProxy, string method, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, CancellationToken cancellationToken = default);

    /// <summary>
    /// Invokes a method on the connection(s) represented by the <see cref="IClientProxy"/> instance
    /// </summary>
    /// <param name="clientProxy">The <see cref="IClientProxy"/></param>
    /// <param name="method">The name of method to invoke.</param>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <param name="arg3">The third argument</param>
    /// <param name="arg4">The fourth argument</param>
    /// <param name="arg5">The fifth argument</param>
    /// <param name="arg6">The sixth argument</param>
    /// <param name="arg7">The seventh argument</param>
    /// <param name="arg8">The eighth argument</param>
    /// <param name="arg9">The ninth argument</param>
    /// <param name="arg10">The tenth argument</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default
    /// value is <see cref="CancellationToken.None"/> </param>    
    /// <returns>A <see cref="Task"/> that represents the asynchronous invoke </returns>
    Task SendAsync(IClientProxy clientProxy, string method, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, CancellationToken cancellationToken = default);
}