using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreTests.Linq
{
    public class Aggregate
    {
        [Theory]
        [InlineData(0, 10)]
        [InlineData(0, 1)]
        [InlineData(10, 0)]
        [InlineData(1, 11)]
        public async Task Sum(int start, int count)
        {
            {
                var xs = await UniTaskAsyncEnumerable.Range(start, count).SumAsync();
                var ys = Enumerable.Range(start, count).Sum();
                xs.Should().Be(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(start, count).SumAsync(x => x * 2);
                var ys = Enumerable.Range(start, count).Sum(x => x * 2);
                xs.Should().Be(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(start, count).SumAwaitAsync(x => UniTask.Run(() => x));
                var ys = Enumerable.Range(start, count).Sum(x => x);
                xs.Should().Be(ys);
            }
            {
                var xs = await UniTaskAsyncEnumerable.Range(start, count).SumAwaitCancellationAsync((x, _) => UniTask.Run(() => x));
                var ys = Enumerable.Range(start, count).Sum(x => x);
                xs.Should().Be(ys);
            }
        }

        public static IEnumerable<object[]> array1 = new object[][]
        {
            new object[]{new int[] { 1, 10, 100 } },
            new object[]{new int?[] { 1, null, 100 } },
            new object[]{new float[] { 1, 10, 100 } },
            new object[]{new float?[] { 1, null, 100 } },
            new object[]{new double[] { 1, 10, 100 } },
            new object[]{new double?[] { 1, null, 100 } },
            new object[]{new decimal[] { 1, 10, 100 } },
            new object[]{new decimal?[] { 1, null, 100 } },
        };

        [Theory]
        [MemberData(nameof(array1))]
        public async Task Average<T>(T arr)
        {
            switch (arr)
            {
                case int[] array:
                    {
                        var xs = await array.ToUniTaskAsyncEnumerable().AverageAsync();
                        var ys = array.Average();
                        xs.Should().Be(ys);
                    }
                    break;
                case int?[] array:
                    {
                        var xs = await array.ToUniTaskAsyncEnumerable().AverageAsync();
                        var ys = array.Average();
                        xs.Should().Be(ys);
                    }
                    break;
                case float[] array:
                    {
                        var xs = await array.ToUniTaskAsyncEnumerable().AverageAsync();
                        var ys = array.Average();
                        xs.Should().Be(ys);
                    }
                    break;
                case float?[] array:
                    {
                        var xs = await array.ToUniTaskAsyncEnumerable().AverageAsync();
                        var ys = array.Average();
                        xs.Should().Be(ys);
                    }
                    break;
                case double[] array:
                    {
                        var xs = await array.ToUniTaskAsyncEnumerable().AverageAsync();
                        var ys = array.Average();
                        xs.Should().Be(ys);
                    }
                    break;
                case double?[] array:
                    {
                        var xs = await array.ToUniTaskAsyncEnumerable().AverageAsync();
                        var ys = array.Average();
                        xs.Should().Be(ys);
                    }
                    break;
                case decimal[] array:
                    {
                        var xs = await array.ToUniTaskAsyncEnumerable().AverageAsync();
                        var ys = array.Average();
                        xs.Should().Be(ys);
                    }
                    break;
                case decimal?[] array:
                    {
                        var xs = await array.ToUniTaskAsyncEnumerable().AverageAsync();
                        var ys = array.Average();
                        xs.Should().Be(ys);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
