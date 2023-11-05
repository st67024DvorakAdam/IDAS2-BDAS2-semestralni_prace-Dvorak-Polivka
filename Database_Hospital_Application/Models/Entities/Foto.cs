﻿using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Database_Hospital_Application.Models.Entities
{
    public class Foto
    {
        public int Id { get; set; }
        public BitmapImage Image { get; set; }
    }

    public static class FotoExtension
    {
        public static BitmapImage ConvertBytesToBitmapImage(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return null; // Pokud jsou data nulová nebo prázdná, vrátíme null
            }

            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
    }

}