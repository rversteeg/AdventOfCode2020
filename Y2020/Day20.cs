using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day20 : PuzzleSolutionWithParsedInput<Day20.Image[]>
    {
        public Day20() : base(20, 2020) { }

        public override object SolvePart1(Image[] input)
        {
            return input
                .SelectMany(x => x.GetInvariantBorders().ToList(), (image, border) => (image, border))
                .GroupBy(x => x.border)
                .Where(x=>x.Count() == 1)
                .GroupBy(x=>x.First().image.Id)
                .Where(x=>x.Count() == 2)
                .Select(x=>x.Key)
                .Aggregate(1L, (seed, val) => seed * val);
        }

        public override object SolvePart2(Image[] input)
        {
            var borderGroupings = input.Select(image => (image, image.GetInvariantBorders().ToList()))
                .SelectMany(x => x.Item2, (imageWithBorder, border) => (imageWithBorder, border))
                .GroupBy(x => x.border)
                .ToList();

            var imageLookup = borderGroupings.ToLookup(x => x.Key, x => x.ToList());

            var corners = borderGroupings
                .Where(x => x.Count() == 1)
                .GroupBy(x => x.First().imageWithBorder.image, x => x.Key)
                .Where(x => x.Count() == 2)
                .ToList();

            var startCorner = 
                corners.First(x => x.Count() == 2);

            var gridSize = (int)Math.Sqrt(input.Length);

            var images = new Image[gridSize, gridSize];

            var maxCorner = startCorner.Max(x => x.Side);

            var nrRotates = maxCorner == 3 
                ? startCorner.Min(x => x.Side) == 0 ? 0 : 1
                : 4 - maxCorner;

            var start = startCorner.Key;

            for (int r = 0; r < nrRotates; r++) // Rotate so corners with no matches are on 3,0
                start = start.Rotate();

            images[0, 0] = start;

            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    if (x == 0 && y == 0) //Skip position 0,0
                        continue;

                    if (x == 0) // Look at top edge ( edge 2 against 0 )
                    {
                        var imageToMatch = images[x, y - 1];
                        var edgeToMatch = imageToMatch.GetInvariantBorders().First(b => b.Side == 2);
                        var matches = imageLookup[edgeToMatch]
                            .SelectMany(grp => grp)
                            .First(image => image.imageWithBorder.image.Id != imageToMatch.Id);


                        var resultImage = matches.imageWithBorder.image;

                        if (edgeToMatch.Flipped == matches.border.Flipped)
                            resultImage = resultImage.Flip(matches.border.Side % 2 == 0, matches.border.Side % 2 != 0);

                        nrRotates = (4- matches.border.Side)%4;

                        for (int r = 0; r < nrRotates; r++)
                            resultImage = resultImage.Rotate();

                        images[x, y] = resultImage;
                    }
                    else // Look at right edge ( edge 1 against edge 3 )
                    {
                        var imageToMatch = images[x-1, y];
                        var edgeToMatch = imageToMatch.GetInvariantBorders().First(b => b.Side == 1);
                        var matches = imageLookup[edgeToMatch]
                            .SelectMany(grp => grp)
                            .First(image => image.imageWithBorder.image.Id != imageToMatch.Id);

                        var resultImage = matches.imageWithBorder.image;

                        if (edgeToMatch.Flipped == matches.border.Flipped)
                            resultImage = resultImage.Flip(matches.border.Side % 2 == 0, matches.border.Side % 2 != 0);

                        nrRotates = 3 - matches.border.Side;

                        for (int r = 0; r < nrRotates; r++)
                            resultImage = resultImage.Rotate();

                        images[x, y] = resultImage;
                    }
                }
            }

            var finalImage = Merge(images);

            var monsterCount =
                (from variation in finalImage.Variations()
                    let count = CountMonsters(variation)
                    select (variation, count)).OrderByDescending(x => x.count).First();

            var seaCount = finalImage.CountSea();

            return seaCount - monsterCount.count * MonsterCoords.Count;
        }

        private static Image Merge(Image[,] images)
        {
            var partSize = images[0, 0].Pixels.GetLength(0) - 2;
            var gridSize = images.GetLength(0);
            var size =  gridSize * partSize;
            var pixels = new bool[size, size];

            for (int xGrid=0; xGrid < gridSize; xGrid++)
            for (int yGrid=0; yGrid < gridSize; yGrid++) 
            {
                for(int x = 0; x < partSize; x++)
                for (int y = 0; y < partSize; y++)
                    pixels[xGrid * partSize + x, yGrid*partSize+y] = images[xGrid, yGrid].Pixels[x + 1,y + 1];
            }

            return new Image(0, pixels);
        }

        private static readonly string[] MonsterTemplate = @"                  # 
#    ##    ##    ###
 #  #  #  #  #  #   ".Split(Environment.NewLine).ToArray();

        private static readonly int MonsterWidth = MonsterTemplate[0].Length;
        private static readonly int MonsterHeight = MonsterTemplate.Length;

        private static readonly IList<(int x, int y)> MonsterCoords = (from x in Enumerable.Range(0, MonsterWidth)
            from y in Enumerable.Range(0, MonsterHeight)
            where MonsterTemplate[y][x] == '#'
            select (x, y)).ToList();

        private static int CountMonsters(Image image)
        {
            var num = 0;

            for (int x = 0; x < image.Pixels.GetLength(0) - MonsterWidth; x++)
            for (int y = 0; y < image.Pixels.GetLength(1) - MonsterHeight; y++)
            {
                if (MonsterCoords.All(coords => image.Pixels[coords.x+x, coords.y+y]))
                    num++;
            }

            return num;
        }

        protected override Image[] Parse()
        {
            return 
                ( from imageText in ReadAllInputText().Split($"{Environment.NewLine}{Environment.NewLine}")
                let parts = imageText.Split(Environment.NewLine)
                select new Image(int.Parse(parts[0].Substring(5,4)), ReadGrid(parts.Skip(1).ToArray())) ).ToArray();
        }

        private bool[,] ReadGrid(string[] input)
        {
            var result = new bool[input.Length, input.Length];
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input.Length; x++)
                {
                    result[x, y] = input[y][x] == '#';
                }
            }

            return result;
        }

        public record Image(int Id, bool[,] Pixels)
        {
            public IEnumerable<Border> GetInvariantBorders()
            {
                var length = Pixels.GetLength(0);
                yield return Border.ToInvariantBorder(Enumerable.Range(0, length).Select(idx => Pixels[idx, 0]).ToArray(), 0);
                yield return Border.ToInvariantBorder(Enumerable.Range(0, length).Select(idx => Pixels[length - 1, idx]).ToArray(), 1);
                yield return Border.ToInvariantBorder(Enumerable.Range(0, length).Select(idx => Pixels[length-idx-1, length-1]).ToArray(), 2);
                yield return Border.ToInvariantBorder(Enumerable.Range(0, length).Select(idx => Pixels[0, length-idx-1]).ToArray(), 3);
            }

            public Image Rotate()
            {
                var size = Pixels.GetLength(0);
                var pixels = new bool[size, size];
                for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    pixels[size-y-1, x] = Pixels[x, y];

                return new Image(Id, pixels);
            }

            public Image Flip(bool horizontal, bool vertical)
            {
                var size = Pixels.GetLength(0);
                var pixels = new bool[size, size];
                for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    pixels[horizontal ? size - x - 1 : x, vertical ? size - y - 1 : y] = Pixels[x, y];

                return new Image(Id, pixels);
            }

            public IEnumerable<Image> Variations()
            {
                yield return this;
                var current = this;
                for (int i = 0; i < 3; i++)
                {
                    current = current.Rotate();
                    yield return current;
                }

                current = current.Flip(true, false);
                yield return current;

                for (int i = 0; i < 3; i++)
                {
                    current = current.Rotate();
                    yield return current;
                }
            }

            public int CountSea()
            {
                return (from x in Enumerable.Range(0, Pixels.GetLength(0))
                    from y in Enumerable.Range(0, Pixels.GetLength(1))
                    where Pixels[x, y]
                    select true).Count();
            }
        }

        public sealed record Border(int Value, int Side, bool Flipped)
        {
            public bool Equals(Border other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Value == other.Value;
            }

            public override int GetHashCode()
            {
                return Value;
            }

            public static Border ToInvariantBorder(bool[] pixels, int side)
            {
                int value = 0; int valueSwapped = 0;

                for (int bit = 0; bit < pixels.Length; bit++)
                {
                    if (pixels[bit])
                    {
                        value += 1 << bit;
                        valueSwapped += 1 << (pixels.Length - 1 - bit);
                    }
                }

                return new Border(Math.Min(value, valueSwapped), side, value > valueSwapped);
            }
        }
    }
}
