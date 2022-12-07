namespace Advent.Days
{
    internal class Day07
    {
        private record File(string Name, int Size);

        private class FileSystem : Directory
        {
            public Directory? Current { get; private set; }

            public FileSystem(string input) : base(null, "/")
            {
                Current = this;

                input.Split("$ ", StringSplitOptions.RemoveEmptyEntries).Skip(1)
                    .Select(x => x.Split("\r\n", StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => (Cmd: x[0].Split(' '), Args: x.Skip(1))).ToList()
                    .ForEach(x => GetAction(x.Cmd, x.Args).Invoke());
            }

            private void Open(string dir) 
                => Current = Current?.Directories.First(x => x.Name == dir);

            private void Back() 
                => Current = Current?.Parent;

            private void Add(IEnumerable<string> list) 
                => list.Select(x => x.Split(' ')).ToList().ForEach(x => Add(x).Invoke());

            private void AddDirectory(string name) 
                => Current?.Directories.Add(new Directory(Current, name));

            private void AddFile(string name, int size) 
                => Current?.Files.Add(new File(name, size));

            private Action Add(string[] args) => args switch
            {
                ["dir", var name] =>        () => AddDirectory(name),
                [var size, var name] =>     () => AddFile(name, int.Parse(size)),
                _ =>                        throw new Exception(),
            };

            private Action GetAction(string[] cmd, IEnumerable<string> args) => cmd switch
            {
                ["cd", ".."] =>             () => Back(),
                ["cd", var dir] =>          () => Open(dir),
                ["ls"] =>                   () => Add(args),
                _ =>                        throw new Exception(),
            };
        }

        private class Directory
        {
            public List<Directory> Directories = new List<Directory>();
            public List<File> Files = new List<File>();
            public Directory? Parent { get; init; }
            public string Name { get; init; }
            public int Size => Directories.Sum(x => x.Size) + Files.Sum(x => x.Size);

            public Directory(Directory? parent, string name)
            {
                Name = name;
                Parent = parent;
            }

            public IEnumerable<Directory> GetDirectories() 
                => GetDirectories(this);

            private IEnumerable<Directory> GetDirectories(Directory root)
            {
                yield return root;

                foreach (var directory in root.Directories)
                    foreach (var child in GetDirectories(directory))
                        yield return child;
            }
        }

        public int First(string input) => new FileSystem(input).GetDirectories()
            .Where(x => x.Size <= 100000)
            .Sum(x => x.Size);

        internal int Second(string input)
        {
            FileSystem fs = new (input);
            return fs.GetDirectories()
                .Where(x => (70000000 - fs.Size + x.Size) >= 30000000)
                .Select(x => x.Size)
                .Min();
        }
    }
}