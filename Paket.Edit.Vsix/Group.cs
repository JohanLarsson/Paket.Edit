namespace Paket.Edit.Vsix
{
    public struct Group
    {
        public Group(string name, int startLine, int endLine)
        {
            this.Name = name;
            this.StartLine = startLine;
            this.EndLine = endLine;
        }

        public string Name { get; }

        public int StartLine { get;  }

        public int EndLine { get;  }
    }
}
