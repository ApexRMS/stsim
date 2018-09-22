// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.IO;

namespace SyncroSim.STSim.Shared
{
    static class FileSystemUtilities
    {
        public static void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyDirectory(diSource, diTarget);
        }

        public static void CopyDirectory(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory)
        {
            if (!Directory.Exists(targetDirectory.FullName))
            {
                Directory.CreateDirectory(targetDirectory.FullName);
            }

            foreach (FileInfo SourceFile in sourceDirectory.GetFiles())
            {
                string f = Path.Combine(targetDirectory.FullName, SourceFile.Name);

                if (!File.Exists(f))
                {
                    SourceFile.CopyTo(f, false);
                }
            }

            foreach (DirectoryInfo SourceSubDir in sourceDirectory.GetDirectories())
            {
                DirectoryInfo targetDi = targetDirectory.CreateSubdirectory(SourceSubDir.Name);
                CopyDirectory(SourceSubDir, targetDi);
            }
        }
    }
}
