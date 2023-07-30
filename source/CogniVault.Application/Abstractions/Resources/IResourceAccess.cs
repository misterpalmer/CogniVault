using CogniVault.Application.Constants;

namespace CogniVault.Application.Abstractions;

public interface IResourceAccess
{
    
}

// public class ResourceAccess : IResourceAccess
// {
//     private Semaphore readSemaphore = new Semaphore(10, 10); // Allow up to 10 concurrent reads.
//     private Semaphore writeSemaphore = new Semaphore(1, 1); // Allow only one concurrent write.

//     public void CheckAccess(FileSystemSecuredOperation operation)
//     {
//         if (operation == FileSystemSecuredOperation.Read)
//         {
//             readSemaphore.WaitOne();
//             try
//             {
//                 // Perform read operation.
//                 Console.WriteLine("Reading resource...");
//             }
//             finally
//             {
//                 // Always release the semaphore to prevent deadlocks.
//                 readSemaphore.Release();
//             }
//         }
//         else if (operation == FileSystemSecuredOperation.Write)
//         {
//             writeSemaphore.WaitOne();
//             try
//             {
//                 // Perform write operation.
//                 Console.WriteLine("Writing to resource...");
//             }
//             finally
//             {
//                 // Always release the semaphore to prevent deadlocks.
//                 writeSemaphore.Release();
//             }
//         }
//         else
//         {
//             throw new NotSupportedException($"Operation {operation} is not supported.");
//         }
//     }

//     public void CreateGlobalLock(string name)
//     {
//         // Create a new Mutex with the given name.
//         Mutex mutex = new Mutex(false, name);

//         try
//         {
//             // Wait for the Mutex to be available.
//             mutex.WaitOne();

//             // Perform some operation that requires exclusive access.
//             Console.WriteLine($"Performing operation with global lock {name}...");
//         }
//         finally
//         {
//             // Always release the Mutex to prevent deadlocks.
//             mutex.ReleaseMutex();
//         }
//     }
// }


// In this example, the CheckAccess method uses semaphores to limit the number of concurrent read and write operations, and the CreateGlobalLock method uses a mutex to ensure exclusive access to a resource.