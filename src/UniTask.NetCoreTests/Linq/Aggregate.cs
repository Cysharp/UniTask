using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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


        public static IEnumerable<object[]> array2 = new object[][]
        {
            new object[]{new int[] { } },
            new object[]{new int[] { 5 } },
            new object[]{new int[] { 5, 10, 100 } },
            new object[]{new int[] { 10, 5,100 } },
            new object[]{new int[] { 100, 10, 5 } },

            new object[]{new int?[] { } },
            new object[]{new int?[] { 5 } },
            new object[]{new int?[] { null, null, null } },
            new object[]{new int?[] { null, 5, 10, 100 } },
            new object[]{new int?[] { 10, 5,100, null } },
            new object[]{new int?[] { 100, 10, 5 } },

            new object[]{new X[] { } },
            new object[]{new X[] { new X(5) } },
            new object[]{new X[] { new X(5), new X(10), new X(100) } },
            new object[]{new X[] { new X(10),new X( 5),new X(100) } },
            new object[]{new X[] { new X(100), new X(10),new X(5) } },

            new object[]{new XX[] { } },
            new object[]{new XX[] { new XX(new X(5)) } },
            new object[]{new XX[] { new XX(new X(5)), new XX(new X(10)), new XX(new X(100)) } },
            new object[]{new XX[] { new XX(new X(10)),new XX(new X( 5)),new XX(new X(100)) } },
            new object[]{new XX[] { new XX(new X(100)), new XX(new X(10)),new XX(new X(5)) } },
        };

        [Theory]
        [MemberData(nameof(array2))]
        public async Task Min<T>(T arr)
        {
            switch (arr)
            {
                case int[] array:
                    {
                        {
                            if (array.Length == 0)
                            {
                                await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().MinAsync());
                                Assert.Throws<InvalidOperationException>(() => array.Min());
                            }
                            else
                            {
                                var xs = await array.ToUniTaskAsyncEnumerable().MinAsync();
                                var ys = array.Min();
                                xs.Should().Be(ys);
                            }
                        }
                        {
                            if (array.Length == 0)
                            {
                                await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().MinAsync(x => x * 2));
                                Assert.Throws<InvalidOperationException>(() => array.Min(x => x * 2));
                            }
                            else
                            {
                                var xs = await array.ToUniTaskAsyncEnumerable().MinAsync(x => x * 2);
                                var ys = array.Min(x => x * 2);
                                xs.Should().Be(ys);
                            }
                        }
                    }
                    break;
                case int?[] array:
                    {
                        {
                            var xs = await array.ToUniTaskAsyncEnumerable().MinAsync();
                            var ys = array.Min();
                            xs.Should().Be(ys);
                        }
                        {
                            var xs = await array.ToUniTaskAsyncEnumerable().MinAsync(x => x);
                            var ys = array.Min(x => x);
                            xs.Should().Be(ys);
                        }
                    }
                    break;
                case X[] array:
                    {
                        {
                            var xs = await array.ToUniTaskAsyncEnumerable().MinAsync();
                            var ys = array.Min();
                            xs.Should().Be(ys);
                        }
                        {

                            if (array.Length == 0)
                            {
                                await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().MinAsync(x => x.Value));
                                Assert.Throws<InvalidOperationException>(() => array.Min(x => x.Value));
                            }
                            else
                            {
                                var xs = await array.ToUniTaskAsyncEnumerable().MinAsync(x => x.Value);
                                var ys = array.Min(x => x.Value);
                                xs.Should().Be(ys);
                            }
                        }
                    }
                    break;
                case XX[] array:
                    {
                        var xs = await array.ToUniTaskAsyncEnumerable().MinAsync(x => x.Value);
                        var ys = array.Min(x => x.Value);
                        xs.Should().Be(ys);
                    }
                    break;
                default:
                    break;
            }
        }



        [Theory]
        [MemberData(nameof(array2))]
        public async Task Max<T>(T arr)
        {
            switch (arr)
            {
                case int[] array:
                    {
                        {
                            if (array.Length == 0)
                            {
                                await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().MaxAsync());
                                Assert.Throws<InvalidOperationException>(() => array.Max());
                            }
                            else
                            {
                                var xs = await array.ToUniTaskAsyncEnumerable().MaxAsync();
                                var ys = array.Max();
                                xs.Should().Be(ys);
                            }
                        }
                        {
                            if (array.Length == 0)
                            {
                                await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().MaxAsync(x => x * 2));
                                Assert.Throws<InvalidOperationException>(() => array.Max(x => x * 2));
                            }
                            else
                            {
                                var xs = await array.ToUniTaskAsyncEnumerable().MaxAsync(x => x * 2);
                                var ys = array.Max(x => x * 2);
                                xs.Should().Be(ys);
                            }
                        }
                    }
                    break;
                case int?[] array:
                    {
                        {
                            var xs = await array.ToUniTaskAsyncEnumerable().MaxAsync();
                            var ys = array.Max();
                            xs.Should().Be(ys);
                        }
                        {
                            var xs = await array.ToUniTaskAsyncEnumerable().MaxAsync(x => x);
                            var ys = array.Max(x => x);
                            xs.Should().Be(ys);
                        }
                    }
                    break;
                case X[] array:
                    {
                        {
                            var xs = await array.ToUniTaskAsyncEnumerable().MaxAsync();
                            var ys = array.Max();
                            xs.Should().Be(ys);
                        }
                        {

                            if (array.Length == 0)
                            {
                                await Assert.ThrowsAsync<InvalidOperationException>(async () => await array.ToUniTaskAsyncEnumerable().MaxAsync(x => x.Value));
                                Assert.Throws<InvalidOperationException>(() => array.Max(x => x.Value));
                            }
                            else
                            {
                                var xs = await array.ToUniTaskAsyncEnumerable().MaxAsync(x => x.Value);
                                var ys = array.Max(x => x.Value);
                                xs.Should().Be(ys);
                            }
                        }
                    }
                    break;
                case XX[] array:
                    {
                        var xs = await array.ToUniTaskAsyncEnumerable().MaxAsync(x => x.Value);
                        var ys = array.Max(x => x.Value);
                        xs.Should().Be(ys);
                    }
                    break;
                default:
                    break;
            }
        }

        public class XX
        {
            public readonly X Value;

            public XX(X value)
            {
                this.Value = value;
            }
        }

        public class X : IComparable<X>
        {
            public readonly int Value;

            public X(int value)
            {
                Value = value;
            }

            public int CompareTo([AllowNull] X other)
            {
                return Comparer<int>.Default.Compare(Value, other.Value);
            }
        }
    }
}
