using Board.DesignerModel;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Board.SystemModel
{
    public class RImage : PropertyChangeBase
    {

        public RImage() { }
        public RImage(FileInfo fi)
        {

            ImageName = fi.Name;
            Path = fi.FullName;
            ImageUri = new Uri(fi.FullName, UriKind.Absolute);
            Source = new BitmapImage(new Uri(fi.FullName, UriKind.Absolute));

            if (fi.Length < 1024)
            {
                FileSize = fi.Length + "B";
            }
            else if (fi.Length < (1024 * 1024))
            {
                FileSize = Math.Round(((double)fi.Length / 1024), 2).ToString() + "KB";
            }
            else
            {
                FileSize = Math.Round(((double)fi.Length / 1024 / 1024), 2).ToString() + "MB";
            }

        }
        public string _imageName { get; set; }
        public string ImageName
        {
            get { return this._imageName; }
            set
            {
                this._imageName = value;
                OnPropertyChanged("ImageName");
            }
        }

        public string _path { get; set; }
        public string Path
        {
            get { return this._path; }
            set
            {
                this._path = value;
                OnPropertyChanged("Path");
            }
        }


        public string _fileSize { get; set; }
        public string FileSize
        {
            get { return this._fileSize; }
            set
            {
                this._fileSize = value;
                OnPropertyChanged("FileSize");
            }
        }
        public Uri _imageUri { get; set; }
        public Uri ImageUri
        {
            get { return this._imageUri; }
            set
            {
                this._imageUri = value;
                OnPropertyChanged("ImageUri");
            }
        }

        public ImageSource _source { get; set; }
        public ImageSource Source
        {
            get { return this._source; }
            set
            {
                this._source = value;
                OnPropertyChanged("Source");
            }
        }

        public override string ToString()
        {
            return Path;
        }

    }
}
