using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Zondoko();
            Console.ReadKey();
        }

        private static void Zondoko()
        {
            var random = new Random(Environment.TickCount);
            var K = "キ・ヨ・シ！";
            var PATTERN = new string[] { "ずん", "ずん", "ずん", "ずん", "どこ" };

            Observable.Interval(TimeSpan.FromMilliseconds(100))
                .Select(_ => random.Next() % 2 == 0 ? "ずん" : "どこ")
                .Scan(new List<string>(), (queue, x) =>
                {
                    queue.Add(x);
                    while (queue.Count > PATTERN.Count()) { queue.RemoveAt(0); }
                    return queue;
                })
                .SelectMany(queue => queue.SequenceEqual(PATTERN) ?
                    new string[] {
                        queue.Last(),   // Queueの最後
                        K,              // + キ・ヨ・シ！
                        string.Empty,   // + 空文字(終了判定用)
                    }.ToObservable() :
                    Observable.Return(queue.Last()))
                .TakeWhile(x => !string.IsNullOrEmpty(x))
                .Subscribe(
                   x => Console.WriteLine(x),
                   () => Console.WriteLine("complete!!"));
        }
    }
}
