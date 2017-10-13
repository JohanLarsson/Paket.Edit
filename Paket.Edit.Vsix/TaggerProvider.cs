﻿namespace Paket.Edit.Vsix
{
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(IOutliningRegionTag))]
    [ContentType("lock")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class TaggerProvider : ITaggerProvider
    {
        [Export]
        [Name("lock")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition LockContentTypeDefinition;

        [Export]
        [FileExtension(".lock")]
        [ContentType("lock")]
        internal static FileExtensionToContentTypeDefinition LockFileExtensionDefinition;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            if (typeof(T) == typeof(IOutliningRegionTag))
            {
                return (ITagger<T>)buffer.Properties.GetOrCreateSingletonProperty(typeof(Tagger), () => (object)new Tagger(buffer));
            }

            return null;
        }
    }
}
