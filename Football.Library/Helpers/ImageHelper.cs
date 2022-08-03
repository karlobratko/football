using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Football.Library.Helpers {
  public static class ImageHelper {
    private static readonly String DIR_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Resources/img-dynamic");

    public static Image LoadImage(String fileName) {
      String path = Path.Combine(DIR_PATH, HashName(fileName));
      using (var fs = new FileStream(path: path, mode: FileMode.Open)) {
        return Image.FromStream(stream: fs);
      }
    }

    public static String GetImagePath(String fileName) =>
      Path.Combine(DIR_PATH, HashName(fileName));

    public static Boolean ImageExists(String fileName) =>
      Directory.GetFiles(path: DIR_PATH)
               .Contains(value: Path.Combine(DIR_PATH, HashName(fileName)));

    public static void CopyImage(String srcPath, String dstFileName) =>
      File.Copy(sourceFileName: srcPath, 
                destFileName: Path.Combine(DIR_PATH, HashName(dstFileName)));

    public static void ReplaceImage(String srcPath, String dstFileName) {
      RemoveImage(fileName: dstFileName);
      CopyImage(srcPath, dstFileName);
    }

    public static void RemoveImage(String fileName) =>
      File.Delete(path: Path.Combine(DIR_PATH, HashName(fileName)));

    private static String HashName(String name) {
      using (var md5 = MD5.Create()) {
        md5.Initialize();

        var sb = new StringBuilder();
        md5.ComputeHash(Encoding.UTF8.GetBytes(name)).ToList().ForEach(b => sb.Append(b.ToString("x2")));

        return sb.ToString();
      }
    }
  }
}
