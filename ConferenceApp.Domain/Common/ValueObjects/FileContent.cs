namespace ConferenceApp.Domain.Common.ValueObjects
{
    public record class FileContent
    {
        public byte[] Content { get; }
        public string FileName { get; }
        public string ContentType { get; }
        public long Size => Content?.Length ?? 0;

        private FileContent() { }

        public FileContent(byte[] content, string fileName, string contentType)
        {
            if (content?.Length > 10 * 1024 * 1024) // 10MB limit
                throw new ArgumentException("File size exceeds 10MB limit");

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name is required");

            Content = content;
            FileName = fileName;
            ContentType = contentType ?? "application/octet-stream";
        }

        public bool IsPdf => ContentType == "application/pdf";
        public bool IsWord => ContentType.Contains("word") || FileName.EndsWith(".docx");
    }
}
