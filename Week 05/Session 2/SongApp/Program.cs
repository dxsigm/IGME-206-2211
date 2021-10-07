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

            Game chess = new Game();
            chess.Name = "chess";

            List<Song> songList = new List<Song>();
            songList.Add(tapeSong);
            songList.Add(cdSong);



            //songList[0]
            //songList[1]

            SortedList<string, IPlay> songSortedList = new SortedList<string, IPlay>();
            
            songSortedList[tapeSong.Name] = tapeSong;
            songSortedList[cdSong.Name] = cdSong;
            songSortedList[chess.Name] = chess;

            foreach(KeyValuePair<string,IPlay> keyValuePair in songSortedList)
            {
                //if( keyValuePair.Value is TapeSong)
                if( keyValuePair.Value.GetType() == typeof(TapeSong))
                {
                    TapeSong tSong = (TapeSong)keyValuePair.Value;
                    Console.WriteLine($"Key = {keyValuePair.Key}   SongName = {tSong.Name}  Tape Side: {tSong.side}");

                }
                else
                {
                    CDSong cSong = (CDSong)keyValuePair.Value;
                    Console.WriteLine($"Key = {keyValuePair.Key}   SongName = {keyValuePair.Value.Name}");
                }
                
            }




            //songGameSortedList["david"] = "david schuh";

            
            //songGameSortedList[chess.Name] = chess;

            chess.Play();
            tapeSong.Play();

            IPlay iPlay;

            iPlay = chess;
            iPlay.Play();

            iPlay = tapeSong;
            iPlay.Play();

        }

        public static void PlayThisObject(IPlay obj)
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
