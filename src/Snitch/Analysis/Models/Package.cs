using System;
using System.Diagnostics;
using Semver;

namespace Snitch.Analysis
{
    [DebuggerDisplay("{Name,nq} ({Version,nq})")]
    internal sealed class Package
    {
        public string Name { get; }
        public SemVersion Version { get; }

        public Package(string name, string version)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Version = ParseSemanticVersion(version ?? throw new ArgumentNullException(nameof(version)));
        }

        private SemVersion ParseSemanticVersion(string version)
        {
            if (!SemVersion.TryParse(version, out var semanticVersion))
            {
                if (!System.Version.TryParse(version, out var notSemanticVersion))
                {
                    throw new ArgumentException($"Version '{version}' for package '{Name}' is not valid.", nameof(version));
                }

                semanticVersion = SemVersion.Parse(notSemanticVersion.ToString(3));
            }

            return semanticVersion;
        }
    }
}
