using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongLib
{
    public interface ISong
    {
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

    }

    public class Class1
    {
    }
}
