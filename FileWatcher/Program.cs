using log4net;
using System;
using System.IO;

namespace FileWatcher
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            var path = Console.ReadLine();
            if (!Directory.Exists(path))
            {
                Logger.Error("Directory not found.");
                return;
            }
            // instantiate the object
            var fileSystemWatcher = new FileSystemWatcher();

            // Associate event handlers with the events
            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;

            // min 4kb, max 64kb
            fileSystemWatcher.InternalBufferSize = 64 * 1000;

            // tell the watcher where to look
            fileSystemWatcher.Path = path
                ;

            // You must add this line - this allows events to fire.
            fileSystemWatcher.EnableRaisingEvents = true;

            Logger.Info("Listening...");
            Logger.Info("(Press any key to exit.)");

            Console.ReadLine();
        }

        private static void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            Logger.Info($"A new file has been renamed from {e.OldName} to {e.Name}");
        }

        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Logger.Info($"A new file has been deleted - {e.Name}");
        }

        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Logger.Info($"A new file has been changed - {e.Name}");
        }

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Logger.Info($"A new file has been created - {e.Name}");
        }
    }
}
