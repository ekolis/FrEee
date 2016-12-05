using FrEee.Game.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrEee.Game.Objects.LogMessages
{
    /// <summary>
    /// A log message which displays a message including a picture from some object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class PictorialLogMessage<T> : LogMessage, IPictorialLogMessage<T> where T : IPictorial
    {
        public PictorialLogMessage(string text, T context)
            : base(text)
        {
            Context = context;
        }

        public PictorialLogMessage(string text, int turn, T context)
            : base(text, turn)
        {
            Context = context;
        }

        /// <summary>
        /// The context for the log message.
        /// </summary>
        public T Context { get; set; }

        public override Image Picture
        {
            get { return Context.Portrait; }
        }

        public ImageSource PictureAsImageSource
        {
            get
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Picture.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                    memory.Position = 0;
                    BitmapImage bitmapimage = new BitmapImage();
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = memory;
                    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit();

                    return bitmapimage;
                }
            }
        }
    }
}
