using Enx.Engine.Asset;
using ImageMagick;
using PommaLabs.MimeTypes;

namespace Enx.Engine.Graphics.Assets;

public class ImageLoader : IAssetLoader<Image>
{
    public IEnumerable<string> FileExtensions { get; }

    static ImageLoader()
    {
        MagickNET.Initialize();
    }

    public ImageLoader()
    {
        var extensions = new List<string>();
        foreach (var i in MagickNET.SupportedFormats)
            if (i.MimeType is not null)
                extensions.AddRange(MimeTypeMap.GetExtensions(i.MimeType));

        FileExtensions = extensions.Distinct().ToList();
    }

    public async Task<Image> Load(Stream stream)
    {
        var factory = new MagickImageFactory();
        using var img = await factory.CreateAsync(stream);
        return new ImageImpl(
            new(img.Width, img.Height),
            img.Compression != CompressionMethod.Undefined,
            img.ToByteArray(MagickFormat.Rgba));
    }
}
