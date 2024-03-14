// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.IO;

namespace SyncroSim.STSim.Shared
{
    static class FileSystemUtilities
    {
        public static void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo SourceDirInfo = new DirectoryInfo(sourceDirectory);
            DirectoryInfo TargetDirInfo = new DirectoryInfo(targetDirectory);

            CopyDirectory(SourceDirInfo, TargetDirInfo);
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
                DirectoryInfo TargetDir = targetDirectory.CreateSubdirectory(SourceSubDir.Name);
                CopyDirectory(SourceSubDir, TargetDir);
            }
        }
    }
}
