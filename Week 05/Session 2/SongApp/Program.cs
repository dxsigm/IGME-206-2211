using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SongLib;

namespace SongApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TapeSong tapeSong = new TapeSong();
            tapeSong.Name = "Happy";

            CDSong cdSong = new CDSong();
            cdSong.Name = "Bridge Over Troubled Water";

            List<Song> songList = new List<Song>();
            songList.Add(tapeSong);
            songList.Add(cdSong);

            SortedList<string, object> songGameSortedList = new SortedList<string, object>();
            songGameSortedList[tapeSong.Name] = tapeSong;
            songGameSortedList[cdSong.Name] = cdSong;
            songGameSortedList["david"] = "david schuh";

            Game chess = new Game();
            chess.Name = "chess";
            songGameSortedList[chess.Name] = chess;

            chess.Play();
            tapeSong.Play();

            IPlay iPlay;

            iPlay = chess;
            iPlay.Play();

            iPlay = tapeSong;
            iPlay.Play();

        }

        public static void PlayThisObject(object obj)
        {
            if( obj.GetType() == typeof(VinylSong))
            {
                VinylSong vinylSong;
                vinylSong = (VinylSong) obj;

                vinylSong.Clean();
            }

            IPlay iPlay;
            iPlay = (IPlay)obj;
            iPlay.Play();

            if( obj.GetType() == typeof(VinylSong) ||
                obj.GetType() == typeof(TapeSong) ||
                obj.GetType() == typeof(MP3Song) ||
                obj.GetType() == typeof(CDSong) )
            {
                Song song;
                song = (Song)obj;
                song.Dance();
                song.Sing();
                song.Copy();
            }
        }
    }
}
