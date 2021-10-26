// <copyright file="TagCollection.cs" company="Irene P. Smith">
// Copyright (c) Irene P. Smith. All rights reserved.
// </copyright>

namespace TagManager.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class TagCollection
    {
        private List<SnippetTag> Tags;

        private static readonly string TagStart = "snippet-start:[";

        private static readonly string TagEnd = "]";

        public TagCollection()
        {
            this.Tags = new List<SnippetTag>();

            Language = new LanguageInfo
            {
                Name = "C#",
                CommentStart = "// ",
                CodeFileExtension = "cs",
            };
        }

        public static LanguageInfo Language { get; set; }

        public static string WorkingDirectory { get; set; }

        public List<SnippetTag> GetTags(string workingDirectory, List<SnippetTag> tags)
        {
            if (tags.Count > 0)
            {
                tags.Clear();
            }

            WorkingDirectory = workingDirectory;

            // Traverse the working directory and its children to
            // find all the snippets in its code files.
            var root = new DirectoryInfo(workingDirectory);
            WalkDirectoryTree(root, tags);
            return tags;
        }

        static void WalkDirectoryTree(DirectoryInfo root, List<SnippetTag> tags)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles($"*.{Language.CodeFileExtension}");
            }

            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                Console.WriteLine(e.Message);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    Console.WriteLine($"Now processing: {fi.FullName}");
                    var lines = File.ReadLines(fi.FullName).Where(l => l.Contains($"{Language.CommentStart}{TagStart}"));
                    foreach (var l in lines)
                    {
                        var tag = new SnippetTag
                        {
                            FilePath = fi.FullName,
                            Tag = l,
                        };

                        tags.Add(ExtractTag(tag));
                    }
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo, tags);
                }
            }
        }

        static SnippetTag ExtractTag(SnippetTag newTag)
        {
            var workingTag = newTag.Tag.Trim();
            var startPos = Language.CommentStart.Length + TagStart.Length;
            var endPos = (workingTag.Length - 1) - startPos;
            workingTag = workingTag.Substring(startPos, endPos);
            newTag.Tag = workingTag;
            return newTag;
        }
    }
}
