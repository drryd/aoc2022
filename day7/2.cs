const long TotalDiskSpaceAvailable = 70000000;
const long SpaceNeededForUpdate = 30000000;
var lines = System.IO.File.ReadAllLines("input.txt");
var DirectoryChangeCommandPrefix = "$ cd ";
var ListCommand = "$ ls";

var root = new Directory("/");
var currentDirectory = root;

foreach ( var line in lines )
{
    // cd
    if ( line.StartsWith( DirectoryChangeCommandPrefix ) )
    {
        var directoryChangedTo = line.Substring(DirectoryChangeCommandPrefix.Length);

        if ( directoryChangedTo == "/" )
        {
            currentDirectory = root;
        }
        else if ( directoryChangedTo == ".." )
        {
            if ( !currentDirectory.IsRoot )
            {
                currentDirectory = currentDirectory.Parent;
            }
        }
        else
        {
            if ( currentDirectory.Subdirectories.ContainsKey(directoryChangedTo) )
            {
                currentDirectory = currentDirectory.Subdirectories[directoryChangedTo];
            }
            else
            {
                throw new Exception( "Expected no cd's into unseen directories" );
            }
        }

        continue;
    }
    
    // ls
    if ( line == ListCommand )
    {
        continue;
    }

    // If we didn't see cd or ls, we must be listing files.
    var tokens = line.Split( " " );
    if ( tokens[0] == "dir" )
    {
        if ( !currentDirectory.DoesSubdirectoryExist( tokens[1] ) )
        {
            currentDirectory.CreateSubdirectory( tokens[1] );
        }
        else
        {
            throw new Exception("Subdirectory already exists");
        }
    }

    if ( long.TryParse( tokens[0], out long fileSize ) )
    {
        if ( !currentDirectory.DoesFileExist( tokens[1] ) )
        {
            currentDirectory.CreateFile( tokens[1], fileSize );
        }
        else
        {
            throw new Exception("File already exists");
        }
    }
}

Console.WriteLine( smallestDirectoryToDeleteForUpdate( root ) );

long smallestDirectoryToDeleteForUpdate(Directory directory)
{
    var totalUnusedSpace = TotalDiskSpaceAvailable - root.Size;
    var minimumSpaceToClearForUpdate = SpaceNeededForUpdate - totalUnusedSpace;
    var smallestDirectoryThatFreesEnoughSpace = directory.Size;
    Queue<Directory> unexploredDirectories = new();
    unexploredDirectories.Enqueue(directory);

    while ( unexploredDirectories.Count > 0 )
    {
        var currDir = unexploredDirectories.Dequeue();

        if ( currDir.Size >= minimumSpaceToClearForUpdate )
        {
            smallestDirectoryThatFreesEnoughSpace = Math.Min(smallestDirectoryThatFreesEnoughSpace, currDir.Size);
        }

        foreach ( var sd in currDir.Subdirectories.Values )
        {
            unexploredDirectories.Enqueue(sd);
        }
    }

    return smallestDirectoryThatFreesEnoughSpace;
}

class Directory
{
    public string Name { get; }
    public Dictionary<string, Directory> Subdirectories = new();
    public List<File> Files = new();
    public Directory Parent { get; }
    public long Size
    {
        get
        {
            return Files.Sum(f => f.Size) + Subdirectories.Sum(s => s.Value.Size);
        }
    }
    public bool IsRoot { get { return Name == "/"; } }

    public bool DoesFileExist(string name) { return this.Files.Exists(f => f.Name == name); }

    public bool DoesSubdirectoryExist(string name) { return this.Subdirectories.Keys.Contains(name); }

    public Directory(string name, Directory parent = null)
    {
        Name = name;
        Parent = parent;
    }

    public void CreateSubdirectory(string name)
    {
        var subdirectory = new Directory(name, this);
        Subdirectories[name] = subdirectory;
    }

    public void CreateFile(string name, long size)
    {
        Files.Add(new File(name, size));
    }
}

class File
{
    public string Name { get; }
    public long Size { get; }

    public File(string name, long size)
    {
        Name = name;
        Size = size;
    }
}