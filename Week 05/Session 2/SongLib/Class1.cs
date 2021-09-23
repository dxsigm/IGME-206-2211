using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongLib
{
    public interface ISong
    {
        string Lyrics
        {
            get; set;
        }

        string Name
        {
            get; set;
        }

        void Play();
        void Sing();
        void Dance();
        void Copy();
    }

    public interface IPlay
    {
        string Name
        {
            get; set;
        }

        void Play();
    }

    public abstract class Song : ISong, IPlay
    {
        public int year;
        public string lyrics;
        public string Lyrics 
        { 
            get; 
            set;
        }

        public string composer;
        public string artist;

        public string Name
        {
            get;
            set;
        }

        public abstract void Play();

        public virtual void Copy()
        {
            // default code to implement Copy()
        }

        public void Dance()
        {
            // bust out my moves
        }

        public void Sing()
        {
            // la la la
        }
    }

    public class Game : IPlay
    {
        public int players;
        public string Name
        {
            get;
            set;
        }

        public void Play()
        {

        }

    }

    public class TapeSong : Song
    {
        public string tapeName;
        public int side;
        public int counter;

        public override void Play()
        {
            // load the tape on the correct side
            // fast forward to counter
            // press play
        }
    }

    public class VinylSong : Song
    {
        public string recordName;
        public int side;
        public int track;

        public override void Play()
        {
            // turn on turntable
            // put the record on the correct side
            // drop the needle on the correct track
        }

        public void Clean()
        {
            // clean the record
        }
    }

    public class CDSong : Song
    {
        public string cdName;
        public int track;

        public override void Play()
        {
            // insert cd
            // forward to the track
            // press play
        }

    }

    public class MP3Song : Song
    {
        public string filename;
        public override void Play()
        {
            // double click the filename
        }

        public override void Copy()
        {
            // copy and paste the mp3 file
        }

    }





    public class Class1
    {
    }
}
