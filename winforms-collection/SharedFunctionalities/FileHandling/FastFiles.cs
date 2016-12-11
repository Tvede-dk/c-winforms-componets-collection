using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;

namespace SharedFunctionalities.FileHandling {
    /// <summary>
    /// Idea is to use memory mapped files for operations to maximize performance. and introduce more async code execution.
    /// </summary>
    public class FastFiles {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="overrideIfExists"></param>
        public async static Task MoveFileAsync(string src, string dest, bool overrideIfExists) {
            if ((overrideIfExists || !File.Exists(dest)) && File.Exists(src)) {
                var destFile = MemoryMappedFile.CreateFromFile(src, FileMode.Create);
                var srcFile = MemoryMappedFile.CreateFromFile(src, FileMode.Open);
                if (destFile != null && srcFile != null) { //make sure that every file is accessable
                    using (var destBuff = destFile.CreateViewStream()) { // and make sure that c# handles the handles.
                        using (var srcBuff = srcFile.CreateViewStream()) { //this also works for exceptions. ;) 
                            await srcBuff.CopyToAsync(destBuff);
                        }
                    }
                }
            }
        }
    }
}
