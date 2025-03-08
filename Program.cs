using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkApp
{
    public class DictionaryBenchmark
    {
        private Dictionary<int, int> _dictionary = new();
        private int _existingKey;
        private int _nonExistingKey;

        [GlobalSetup]
        public void Setup()
        {
            _dictionary = new Dictionary<int, int>();
            _existingKey = 50000;
            _nonExistingKey = 999999;

            for (int i = 0; i < 100000; i++)
            {
                _dictionary[i] = i;
            }
        }

        [Benchmark]
        public int GetOrAdd_New()
        {
            return _dictionary.GetOrAdd(_nonExistingKey, 123);
        }

        [Benchmark]
        public int GetOrAdd_Existing()
        {
            return _dictionary.GetOrAdd(_existingKey, 123);
        }

        [Benchmark]
        public bool TryUpdate_Existing()
        {
            return _dictionary.TryUpdate(_existingKey, 456);
        }

        [Benchmark]
        public bool TryUpdate_New()
        {
            return _dictionary.TryUpdate(_nonExistingKey, 456);
        }

        [Benchmark]
        public int ContainsKey_Add()
        {
            if (!_dictionary.ContainsKey(_nonExistingKey))
            {
                _dictionary[_nonExistingKey] = 123;
            }
            return _dictionary[_nonExistingKey];
        }

        [Benchmark]
        public int TryGetValue_Add()
        {
            if (!_dictionary.TryGetValue(_nonExistingKey, out var value))
            {
                value = 123;
                _dictionary[_nonExistingKey] = value;
            }
            return value;
        }
    }

    public static class DictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(
            this Dictionary<TKey, TValue> dict, TKey key, TValue value)
            where TKey : notnull
        {
            ref var val = ref CollectionsMarshal.GetValueRefOrAddDefault(dict, key, out var exists);
            if (exists)
            {
                return val;
            }
            val = value;
            return value;
        }

        public static bool TryUpdate<TKey, TValue>(
            this Dictionary<TKey, TValue> dict, TKey key, TValue value)
            where TKey : notnull
        {
            ref var val = ref CollectionsMarshal.GetValueRefOrNullRef(dict, key);
            if (Unsafe.IsNullRef(ref val))
            {
                return false;
            }

            val = value;
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<DictionaryBenchmark>();
        }
    }
}
