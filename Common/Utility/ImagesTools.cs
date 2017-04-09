using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using System.IO;

namespace Common.Utility
{
    /// <summary>
    /// 保存图片, 如果图片之前已上传, 删除旧图片
    /// </summary>
    public class ImagesTools
    {

        public const string HW = "HW";
        public const string H = "H";
        public const string W = "W";
        public const string Cut = "CUT";
        /// <summary>
        /// 缩略图
        /// </summary>
        public const string small_img = "_s.";

        /// <summary>
        /// 多个图片路径之间的分隔符
        /// </summary>
        public const char split_char = '*';

        /// <summary>
        /// 一次保存多个图片到指定路径中
        /// folder 文件夹名称前后都加上 /
        /// 如果是上传文件, 那么 smallImg 必须是 false
        /// </summary>
        /// <param name="path">服务器的物理文件路径,例如; Server.MapPath("~"); </param>
        /// <param name="folder">文件夹名称,例如: /mvc_up_files/ </param>
        /// <param name="smallImg">是否生成缩略图, 图片质量默认75,宽高最大值200</param>
        /// <param name="imgs">上传的图片数组</param>
        /// <returns>图片名称的集合,包含 folder 名称,以 * 分割</returns>
        public static string save(string path = null, string folder = "/upload/imgs/", bool smallImg = true, params HttpPostedFileWrapper[] imgs)
        {
            StringBuilder sb_img = new StringBuilder();
            if (path == null) { path = System.Web.HttpContext.Current.Server.MapPath("~"); }
            path = path + folder;
            path = path.Replace("//", "/");
            if (imgs != null && imgs.Length > 0)
            {
                Random r = new Random();
                StringBuilder sb = new StringBuilder(); // 完整的路径,包含文件名
                StringBuilder sbName = new StringBuilder(); // 相对路径,包含文件名
                StringBuilder temp = new StringBuilder();   // 随机数
                foreach (var item in imgs)
                {
                    if (item != null)
                    {
                        sb.Clear();
                        sbName.Clear();
                        var tick = DateTime.Now.Ticks;
                        sb.Append(path);
                        sb.Append(tick);
                        sbName.Append(folder);
                        sbName.Append(tick);
                        temp.Clear();
                        for (int i = 0; i < 6; i++)
                        {
                            temp.Append(r.Next(0, 10));
                        }
                        sb.Append(temp);
                        sbName.Append(temp);
                        string ext = item.FileName.Substring(item.FileName.LastIndexOf('.'));
                        sb.Append(ext);
                        sbName.Append(ext);
                        sb_img.Append(sbName.Append(split_char).ToString());
                        string file = sb.ToString().Replace("//", "/"); // 文件名的绝对路径
                        item.SaveAs(file);
                        if (smallImg)
                        {
                            SmallImage(file, file.Replace(".", small_img), 200, 200, (long)75);
                        }
                    }
                }
                if (sb_img.Length > 0)
                {
                    return sb_img.ToString().Substring(0, sb_img.Length - 1);
                }
            }
            return "";
        }

        /// <summary>
        /// 如果上传新的图片, 则在之前先删除旧图片, 路径以 * 隔开
        /// </summary>
        /// <param name="imgs"></param>
        /// <returns></returns>
        public static int delete(string imgs)
        {
            int count = 0;
            if (imgs != null && imgs.Length > 0)
            {
                string[] imgPathList = imgs.Split(split_char);
                if (imgPathList != null && imgPathList.Length > 0)
                {
                    var path = HttpContext.Current.Server.MapPath("~"); // 服务器的当前根目录

                    foreach (var item in imgPathList)
                    {
                        if (item != null && item.Length > 0)
                        {
                            var imgPath = (path + item).Replace("//", "/");
                            var smallImgPath = imgPath.Replace(".", small_img);
                            if (File.Exists(imgPath))
                            {
                                File.Delete(imgPath);
                                count++;
                            }
                            if (File.Exists(smallImgPath))
                            {
                                File.Delete(smallImgPath);
                                count++;
                            }
                        }
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 自动压缩图片, 最大宽高是200, 按比例缩放,自动根据宽高大小判断缩放模式
        /// </summary>
        /// <param name="fileName">原始文件名</param>
        /// <param name="newFile">缩略图文件名</param>
        /// <param name="maxHeight">最大高度</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="qualityNum">图片质量,默认75</param>
        private static void SmallImage(string fileName, string newFile, int maxHeight, int maxWidth, long qualityNum)
        {
            Image img = Image.FromFile(fileName);
            string mode = W;
            if (img.Width >= img.Height)
            {
                mode = W;
            }
            else
            {
                mode = H;
            }
            System.Drawing.Imaging.ImageFormat thisFormat = img.RawFormat;
            int towidth = maxWidth;
            int toheight = maxHeight;
            int x = 0;
            int y = 0;
            int ow = img.Width;
            int oh = img.Height;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = img.Height * maxWidth / img.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = img.Width * maxHeight / img.Height;
                    break;
                case "CUT"://指定高宽裁减（不变形）                
                    if ((double)img.Width / (double)img.Height > (double)towidth / (double)toheight)
                    {
                        oh = img.Height;
                        ow = img.Height * towidth / toheight;
                        y = 0;
                        x = (img.Width - ow) / 2;
                    }
                    else
                    {
                        ow = img.Width;
                        oh = img.Width * maxHeight / towidth;
                        x = 0;
                        y = (img.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            Bitmap outBmp = new Bitmap(towidth, toheight);
            Graphics g = Graphics.FromImage(outBmp);
            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, new Rectangle(0, 0, towidth, toheight), x, y, ow, oh, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时,设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = qualityNum;//图片质量1-100
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象.
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            for (int index = 0; index < arrayICI.Length; index++)
            {
                if (arrayICI[index].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[index];
                    //设置JPEG编码
                    break;
                }
            }
            if (jpegICI != null)
            {
                outBmp.Save(newFile, jpegICI, encoderParams);
            }
            else
            {
                outBmp.Save(newFile, thisFormat);
            }
            img.Dispose();
            outBmp.Dispose();
        }
    }
}
