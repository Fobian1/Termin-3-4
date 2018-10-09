using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmExpriment {
    public static class Mergesorter {
        public static void DoMergeSort(this int[] numbers) {
            var sortedNumbers = MergeSort(numbers);
            for (int i = 0; i < sortedNumbers.Length; i++) {
                numbers[i] = sortedNumbers[i];
            }
        }
        private static int[] MergeSort(int[] numbers) {
            if (numbers.Length <= 1)
                return numbers;

            var left = new List<int>();
            var right = new List<int>();

            for (int i = 0; i < numbers.Length; i++) {
                if (i % 2 > 0) {
                    left.Add(numbers[i]);
                } else {
                    right.Add(numbers[i]);
                }
            }

            left = MergeSort(left.ToArray()).ToList();
            right = MergeSort(right.ToArray()).ToList();

            return Merge(left, right);
        }

        private static int[] Merge(List<int> left, List<int> right) {
            var result = new List<int>();

            while (NotEmpty(left) && NotEmpty(right)) {
                if (left.First() <= right.First())
                    MoveValueFromSourceToResult(left, result);
                else
                    MoveValueFromSourceToResult(right, result);
            }

            while (NotEmpty(left)) {
                MoveValueFromSourceToResult(left, result);
            }
            while (NotEmpty(right))
                MoveValueFromSourceToResult(right, result);

            return result.ToArray();
        }

        public static bool NotEmpty(List<int> list) {
            return list.Count > 0;
        }

        public static void MoveValueFromSourceToResult(List<int> list, List<int> result) {
            result.Add(list.First());
            list.Remove(0);
        }
    }
}
