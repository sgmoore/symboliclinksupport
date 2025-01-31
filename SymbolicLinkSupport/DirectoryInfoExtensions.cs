﻿using System;
using System.IO;

namespace SymbolicLinkSupport
{
    /// <summary>
    /// A class which provides extensions for <see cref="DirectoryInfo"/> to handle symbolic directory links.
    /// </summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Creates a symbolic link to this directory at the specified path.
        /// </summary>
        /// <param name="directoryInfo">the source directory for the symbolic link.</param>
        /// <param name="path">the path of the symbolic link.</param>
        /// <param name="makeTargetPathRelative">whether the target should be made relative to the symbolic link. Default <c>false</c>.</param>
        public static void CreateSymbolicLink(this DirectoryInfo directoryInfo, string path, bool makeTargetPathRelative)
        {
            SymbolicLink.CreateDirectoryLink(path, directoryInfo.FullName, makeTargetPathRelative);
        }

        /// <summary>
        /// Creates a symbolic link to this directory at the specified path.
        /// </summary>
        /// <param name="directoryInfo">the source directory for the symbolic link.</param>
        /// <param name="path">the path of the symbolic link.</param>
        public static void CreateSymbolicLink(this DirectoryInfo directoryInfo, string path)
        {
            directoryInfo.CreateSymbolicLink(path, false);
        }        

        /// <summary>
        /// Determines whether this directory is a symbolic link.
        /// </summary>
        /// <param name="directoryInfo">the directory in question.</param>
        /// <returns><code>true</code> if the directory is, indeed, a symbolic link, <code>false</code> otherwise.</returns>
        public static bool IsSymbolicLink(this DirectoryInfo directoryInfo)
        {
            return SymbolicLink.GetTarget(directoryInfo.FullName) != null;
        }

        /// <summary>
        /// Determines whether the target of this symbolic link still exists.
        /// </summary>
        /// <param name="directoryInfo">The symbolic link in question.</param>
        /// <returns><code>true</code> if this symbolic link is valid, <code>false</code> otherwise.</returns>
        /// <exception cref="System.ArgumentException">If the directory is not a symbolic link.</exception>
        public static bool IsSymbolicLinkValid(this DirectoryInfo directoryInfo)
        {
            return Directory.Exists(directoryInfo.GetSymbolicLinkTarget());
        }

        /// <summary>
        /// Returns the full path to the target of this symbolic link.
        /// </summary>
        /// <param name="directoryInfo">The symbolic link in question.</param>
        /// <returns>The path to the target of the symbolic link.</returns>
        /// <exception cref="System.ArgumentException">If the directory in question is not a symbolic link.</exception>
        public static string GetSymbolicLinkTarget(this DirectoryInfo directoryInfo)
        {
            var link = SymbolicLink.GetTarget(directoryInfo.FullName);

            if (link == null)
                throw new ArgumentException($"Directory \"{directoryInfo.FullName}\" is not a symbolic link.");

            return link;
        }

        /// <summary>
        /// Returns the full path to the target of this symbolic link or null if target is not a symbolic link
        /// </summary>
        /// <param name="directoryInfo">The folder in question.</param>
        /// <returns>The path to the target of the symbolic link or null if not symbolic kink.</returns>
        public static string GetPossibleSymbolicLinkTarget(this DirectoryInfo directoryInfo)
        {
            return SymbolicLink.GetTarget(directoryInfo.FullName);
        }
    }
}