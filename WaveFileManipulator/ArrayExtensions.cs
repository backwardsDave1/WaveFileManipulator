using System;

namespace WaveFileManipulator
{
    public static class ArrayExtensions
    {
        public static T[] Combine<T>(this T[] firstArray, T[] secondArray)
        {
            T[] reversedWavFileStreamByteArray = new T[firstArray.Length + secondArray.Length];
            Array.Copy(firstArray, reversedWavFileStreamByteArray, firstArray.Length);
            Array.Copy(secondArray, 0, reversedWavFileStreamByteArray, firstArray.Length, secondArray.Length);
            return reversedWavFileStreamByteArray;
        }
    }
}