﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace GGL
{
    public class LockBitmap
    {
        public int Width;
        public int Height;
        private Bitmap bmp;
        private Rectangle rect;
        private System.Drawing.Imaging.BitmapData bmpData;
        private IntPtr ptr;
        private int bytes;
        public byte[] Data;


        public LockBitmap(Bitmap input, bool byValue)
        {
            Width = input.Width;
            Height = input.Height; 

            if (byValue) bmp = new Bitmap(input);
            else bmp = input;
            rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            ptr = bmpData.Scan0;
            bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            Data = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, Data, 0, bytes);
        }
        public LockBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            bmp = new Bitmap(Width, Height);
            rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            ptr = bmpData.Scan0;
            bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            Data = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, Data, 0, bytes);
        }
        public Bitmap returnBitmap()
        {
            System.Runtime.InteropServices.Marshal.Copy(Data, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            return bmp;
        }
    }
    class ByteArray
    {
        public int Width;
        public int Height;
        public int Offset;
        private byte[] values;
        public ByteArray(int Width, int Height, int Offset)
        {
            this.Width = Width;
            this.Height = Height;
            this.Offset = Offset;
            values = new byte[Width * Height * Offset];
        }
        public byte[] getValue()
        {
            return values;
        }
    }
}
