// Copyright (c) Microsoft Corporation and contributors.  All Rights Reserved.  See License.txt in the project root for license information.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static TorchSharp.torch;


namespace TorchSharp.torchvision
{
    internal class Erase : ITransform
    {
        internal Erase(int top, int left, int height, int width, Tensor value, bool inplace = false)
        {
            this.top = top;
            this.left = left;
            this.height = height;
            this.width = width;
            this.inplace = inplace;
            this.value = value;
        }

        public Tensor forward(Tensor img)
        {
            if (!inplace)
                img = img.clone();

            img[TensorIndex.Ellipsis, top..(top + height), left..(left + width)] = value;
            return img;
        }

        private int top, left, height, width;
        private Tensor value;
        private bool inplace;
    }

    public static partial class transforms
    {
        /// <summary>
        /// Crop the image.
        /// </summary>
        /// <returns></returns>
        /// <remarks>The image will not be cropped outside its boundaries.</remarks>
        static public ITransform Erase(int top, int left, int height, int width, Tensor value, bool inplace = false)
        {
            return new Erase(top, left, height, width, value, inplace);
        }
    }
}
