using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace zadanie_muzyka
{
    public partial class MainWindow : Window
    {
        private List<Album> albums;
        private int albumCount = 0;
        private int currentIndex = 0;

        class Album
        {
            private string artistName;
            private string albumTitle;
            private string trackCount;
            private string releaseYear;
            private string downloadCount;

            public Album(string artist, string title, string tracks, string year, string downloads)
            {
                artistName = artist;
                albumTitle = title;
                trackCount = tracks;
                releaseYear = year;
                downloadCount = downloads;
            }

            public string ArtistName
            {
                get { return artistName; }
                set { artistName = value; }
            }
            public string AlbumTitle
            {
                get { return albumTitle; }
                set { albumTitle = value; }
            }
            public string TrackCount
            {
                get { return trackCount; }
                set { trackCount = value; }
            }
            public string ReleaseYear
            {
                get { return releaseYear; }
                set { releaseYear = value; }
            }
            public string DownloadCount
            {
                get { return downloadCount; }
                set { downloadCount = value; }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadAlbumData();
        }
        private void LoadAlbumData()
        {
            albums = new List<Album>();
            int lineCount = 0;
            using (StreamReader reader = new StreamReader("Data.txt"))
            {
                while (reader.ReadLine() != null) lineCount++;
            }

            string[] dataLines = new string[lineCount];
            using (StreamReader reader = new StreamReader("Data.txt"))
            {
                for (int i = 0; i < lineCount; i++) dataLines[i] = reader.ReadLine();
            }

            int lineIndex = 0;
            string artist = "";
            string title = "";
            string tracks = "";
            string year = "";
            string downloads = "";

            for (int i = 0; i < lineCount; i++)
            {
                lineIndex++;
                switch (lineIndex)
                {
                    case 1: artist = dataLines[i]; break;
                    case 2: title = dataLines[i]; break;
                    case 3: tracks = dataLines[i]; break;
                    case 4: year = dataLines[i]; break;
                    case 5: downloads = dataLines[i]; break;
                    case 6:
                        albums.Add(new Album(artist, title, tracks, year, downloads));
                        lineIndex = 0;
                        break;
                }
            }
        }
        private void ShowPreviousAlbum(object sender, RoutedEventArgs e)
        {
            if (albums.Count == 0)
            {
                return;
            }

            if (currentIndex == 0)
            {
                currentIndex = albums.Count - 1;
            }
            else
            {
                currentIndex--;
            }

            UpdateAlbumInfo();
        }
        private void ShowNextAlbum(object sender, RoutedEventArgs e)
        {
            if (albums.Count == 0)
            {
                return;
            }

            if (currentIndex == albums.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            UpdateAlbumInfo();
        }
        private void IncreaseDownloadCount(object sender, RoutedEventArgs e)
        {
            int downloads = int.Parse(albums[currentIndex].DownloadCount);
            downloads++;
            albums[currentIndex].DownloadCount = downloads.ToString();
            UpdateAlbumInfo();
        }

        private void UpdateAlbumInfo()
        {
            if (currentIndex >= 0 && currentIndex < albums.Count)
            {
                var currentAlbum = albums[currentIndex];
                artist.Text = currentAlbum.ArtistName;
                title.Text = currentAlbum.AlbumTitle;
                number_views.Text = currentAlbum.TrackCount;
                year.Text = currentAlbum.ReleaseYear;
                download_count.Text = currentAlbum.DownloadCount;
            }
            else
            {
                artist.Text = string.Empty;
                title.Text = string.Empty;
                number_views.Text = string.Empty;
                year.Text = string.Empty;
                download_count.Text = string.Empty;
            }
        }
    }
}
