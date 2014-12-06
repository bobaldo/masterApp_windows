using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MasterApp14
{

    #region Classi e struct
    public class Stack<T>
    {
        private T[] data;
        private int sp;

        public Stack()
        {
            sp = 0;
            data = new T[16];
        }

        public int Count
        {
            get { return sp; }
        }

        public int Capacity
        {
            get { return data.Length; }
            set
            {
                if (value <= 0) throw new Exception("troppo piccolo!!!!");
                var t = new T[value];
                Array.Copy(data, t, sp);
                data = t;
            }
        }

        public T this[int idx]
        {
            get
            {
                if (idx >= sp) throw new Exception("boh");
                return data[idx];
            }
        }

        public T Top
        {
            get { return sp == 0 ? default(T) : data[sp - 1]; }
        }

        private void EnsureCapacity()
        {
            if (data.Length <= sp)
            {
                var t = new T[2 * data.Length];
                Array.Copy(data, t, data.Length);
                Array.Clear(data, 0, data.Length);
                data = t;
            }
            else if (sp > 10 && data.Length / 2 > sp)
            {
                var t = new T[data.Length / 2];
                Array.Copy(data, t, sp);
                Array.Clear(data, 0, sp - 1);
                data = t;
            }
        }

        public T Pop()
        {
            if (sp == 0) throw new Exception("VUOTO!!!!");
            var v = data[--sp];
            data[sp] = default(T);
            EnsureCapacity();
            return v;
        }


        public void Push(T v)
        {
            EnsureCapacity();
            data[sp++] = v;
        }

        public static Stack<T> operator +(Stack<T> a, Stack<T> b)
        {

            var ret = new Stack<T>();
            ret.Capacity = a.Count + b.Count;

            Array.Copy(a.data, ret.data, a.Count);
            return ret;
        }
    }
    #endregion

    #region Comprehension

    public static class Exampale
    {
        public static IEnumerable<int> N()
        {
            var n = 0;
            while (true)
            {
                yield return n++;
            }
        }

        //extension method
        public static IEnumerable<T> Odd<T>(this IEnumerable<T> e)
        {
            var flip = false;
            foreach (var v in e)
            {
                if (flip) yield return v;
                flip = !flip;
            }
        }
    }
    
    #endregion


    class Program
    {
        static void Main(string[] args)
        {
            var ss = new Stack<string>();
            var si = new Stack<int>();

            ss.Push("Ciao");
            //ss.Push(2);

            for (int i = 0; i < 10; i++)
                si.Push(i);

            Console.WriteLine(ss.Top);
            Console.WriteLine(ss.Pop());
            //si.Count = 5;
            si.Capacity = 10;

            Console.WriteLine(si[3]);

            var sc1 = new Stack<int> { Capacity = 1024 };

            var scp = new Stack<float> { Capacity = 1024 };
            var ss1 = si + sc1;

            //Thread t = new Thread(delegate() { while (true) { } }); //invocare thred con codice tramite delegate
            //ThreadPool.QueueUserWorkItem(delegate(object o) { while (true) { } });

            var N = Exampale.N();
            //foreach (var v in  N.Take(50))
            //    Console.WriteLine(v);

            //foreach (var v in Exampale.Odd(N.Take(100)))
            //    Console.WriteLine(v);


            foreach (var v in N.Take(100).Odd())
                Console.WriteLine(v);

            var q = from v in N.Take(100) where v < 10 select v.GetHashCode();


            Console.ReadLine();
        }
    }
}