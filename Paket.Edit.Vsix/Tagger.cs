namespace Paket.Edit.Vsix
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Tagging;

    internal sealed class Tagger : ITagger<IOutliningRegionTag>
    {
        private readonly ITextBuffer buffer;

        public Tagger(ITextBuffer buffer)
        {
            this.buffer = buffer;
            buffer.Changed += this.OnBufferChanged;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IOutliningRegionTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            var group = new Group("meh", 1, 5);
            var startLine = this.buffer.CurrentSnapshot.GetLineFromLineNumber(group.StartLine);
            var endLine = this.buffer.CurrentSnapshot.GetLineFromLineNumber(group.EndLine);
            yield return new TagSpan<IOutliningRegionTag>(
                new SnapshotSpan(startLine.Start, endLine.End),
                new OutliningRegionTag(false, true, group.Name, null));
        }

        private void OnBufferChanged(object sender, TextContentChangedEventArgs e)
        {
            this.TagsChanged?.Invoke(this, new SnapshotSpanEventArgs(new SnapshotSpan(this.buffer.CurrentSnapshot, 0, this.buffer.CurrentSnapshot.Length)));
        }

    }
}
