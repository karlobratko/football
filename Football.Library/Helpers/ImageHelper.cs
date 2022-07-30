using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Football.Library.Helpers {
  public static class ImageHelper {
    private static readonly String DIR_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Resources/img-dynamic");

    private static String Hash(String fileName) {
      using (var md5 = MD5.Create()) {
        md5.Initialize();

        var sb = new StringBuilder();
        md5.ComputeHash(Encoding.UTF8.GetBytes(fileName)).ToList().ForEach(b => sb.Append(b.ToString("x2")));

        return sb.ToString();
      }
    }

    public static Image LoadImage(String fileName) {
      String path = Path.Combine(DIR_PATH, Hash(fileName));
      using (var fs = new FileStream(path, FileMode.Open)) {
        return Image.FromStream(fs);
      }
    }

    public static String GetImagePath(String fileName) =>
      Path.Combine(DIR_PATH, Hash(fileName));

    public static Boolean ImageExists(String fileName) =>
      Directory.GetFiles(DIR_PATH).Contains(Path.Combine(DIR_PATH, Hash(fileName)));

    public static void CopyImage(String original, String fileName) =>
      File.Copy(original, Path.Combine(DIR_PATH, Hash(fileName)));

    public static void ReplaceImage(String fileName, String path) {
      RemoveImage(fileName);
      CopyImage(path, fileName);
    }
    public static void RemoveImage(String fileName) =>
      File.Delete(Path.Combine(DIR_PATH, Hash(fileName)));
  }
}
