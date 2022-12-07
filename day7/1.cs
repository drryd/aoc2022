// var lines = System.IO.File.ReadAllLines("input.txt");
// var DirectoryChangeCommandPrefix = "$ cd ";
// var ListCommand = "$ ls";

// var root = new Directory("/");
// var currentDirectory = root;

// foreach ( var line in lines )
// {
//     // cd
//     if ( line.StartsWith( DirectoryChangeCommandPrefix ) )
//     {
//         var directoryChangedTo = line.Substring(DirectoryChangeCommandPrefix.Length);

//         if ( directoryChangedTo == "/" )
//         {
//             currentDirectory = root;
//         }
//         else if ( directoryChangedTo == ".." )
//         {
//             if ( !currentDirectory.IsRoot )
//             {
//                 currentDirectory = currentDirectory.Parent;
//             }
//         }
//         else
//         {
//             if ( currentDirectory.Subdirectories.ContainsKey(directoryChangedTo) )
//             {
//                 currentDirectory = currentDirectory.Subdirectories[directoryChangedTo];
//             }
//             else
//             {
//                 throw new Exception( "Expected no cd's into unseen directories" );
//             }
//         }

//         continue;
//     }
    
//     // ls
//     if ( line == ListCommand )
//     {
//         continue;
//     }

//     // If we didn't see cd or ls, we must be listing files.
//     var tokens = line.Split( " " );
//     if ( tokens[0] == "dir" )
//     {
//         currentDirectory.CreateSubdirectory( tokens[1] );
//     }

//     if ( long.TryParse( tokens[0], out long fileSize ) )
//     {
//         currentDirectory.CreateFile( tokens[1], fileSize );
//     }
// }

// Console.WriteLine( sumDirectoriesWithSizeLTE( root, 100000 ) );

// long sumDirectoriesWithSizeLTE(Directory directory, long maximumSize)
// {
//     long sum = 0;
//     Queue<Directory> unexploredDirectories = new();
//     unexploredDirectories.Enqueue(directory);

//     while ( unexploredDirectories.Count > 0 )
//     {
//         var currDir = unexploredDirectories.Dequeue();

//         if ( currDir.Size <= maximumSize)
//         {
//             sum += currDir.Size;
//         }

//         foreach ( var sd in currDir.Subdirectories.Values )
//         {
//             unexploredDirectories.Enqueue(sd);
//         }
//     }

//     return sum;
// }

// class Directory
// {
//     public string Name { get; }
//     public Dictionary<string, Directory> Subdirectories = new();
//     public List<File> Files = new();
//     public Directory Parent { get; }
//     public long Size
//     {
//         get
//         {
//             return Files.Sum(f => f.Size) + Subdirectories.Sum(s => s.Value.Size);
//         }
//     }
//     public bool IsRoot { get { return Name == "/"; } }

//     public Directory(string name, Directory parent = null)
//     {
//         Name = name;
//         Parent = parent;
//     }

//     public void CreateSubdirectory(string name)
//     {
//         var subdirectory = new Directory(name, this);
//         Subdirectories[name] = subdirectory;
//     }

//     public void CreateFile(string name, long size)
//     {
//         Files.Add(new File(name, size));
//     }
// }

// class File
// {
//     public string Name { get; }
//     public long Size { get; }

//     public File(string name, long size)
//     {
//         Name = name;
//         Size = size;
//     }
// }