using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using WaveFileManipulator;

namespace WaveFileManipulatorTests
{
    [TestClass]
    public class ManipulatorTests
    {
        private const string FilePath = @"C:\file.wav";

        [TestMethod]
        public void Run()
        {
            var filePath = @"C:\file.wav";
            var manipulator = new Manipulator(filePath);
            //var reversedByteArray = manipulator.Reverse();
            var decreasedVolumeByteArray = manipulator.IncreaseVolume(0.5);
            
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            var fileDirectoryPath = Path.GetDirectoryName(filePath);
            var outputFilePath = Path.Combine(fileDirectoryPath, fileNameWithoutExtension + "Modified.wav");
            WriteWavFileByteArrayToFile(decreasedVolumeByteArray, outputFilePath);
        }

        [TestMethod]
        public void ReversedFileIsSameSizeAsOriginal()
        {
            //Arrange
            var manipulator = new Manipulator(FilePath);
            var expectedByteArray = new byte[35992];

            //Act
            var reversedByteArray = manipulator.Reverse();            
            
            //Assert
            Assert.AreEqual(expectedByteArray.Length, reversedByteArray.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void NonExistentFileThrowsException()
        {
            //Arrange
            var manipulator = new Manipulator(@"C:\thisFileDoesntExist.wav");

            //Act
            manipulator.Reverse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WrongFileExtensionThrowsException()
        {
            //Arrange
            var manipulator = new Manipulator(@"C:\thisFileDoesntExist.txt");

            //Act
            manipulator.Reverse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NonWaveFileContentFormatThrowsException()
        {
            //Arrange
            var manipulator = new Manipulator(@"C:\Users\David'\Desktop\WavFiles\16BitPCM\notWavFormat.wav");

            //Act
            manipulator.Reverse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooSmallFileThrowsException()
        {
            //Arrange
            var manipulator = new Manipulator(@"C:\Users\David'\Desktop\WavFiles\16BitPCM\tooSmall.wav");

            //Act
            manipulator.Reverse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArrayThrowsException()
        {
            //Arrange
            IEnumerable<byte> array = null;
            var manipulator = new Manipulator(array);            

            //Act
            manipulator.Reverse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullStringThrowsException()
        {
            //Arrange
            string array = null;
            var manipulator = new Manipulator(array);
            
            //Act
            manipulator.Reverse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyStringThrowsException()
        {
            //Arrange
            var manipulator = new Manipulator("");

            //Act
            manipulator.Reverse();
        }
        private class NewReverser : IReverser
        {            
            public byte[] Reverse(Metadata metadata, byte[] forwardsArrayWithOnlyHeaders, byte[] forwardsArrayWithOnlyAudioData)
            {
                return new byte[0];
            }
        }
        [TestMethod]
        public void DifferentIReverserImplementationWorks()
        {
            //Arrange
            var filePath = @"C:\Users\David'\Desktop\WavFiles\out.wav";
            var manipulator = new Manipulator(filePath, new NewReverser());

            //Act
            var reversedByteArray = manipulator.Reverse();

            //Assert
            Assert.AreEqual(0, reversedByteArray.Length);
        }

        private static void WriteWavFileByteArrayToFile(byte[] reversedWavFileStreamByteArray, string reversedWavFilePath)
        {
            using FileStream reversedFileStream = new FileStream(reversedWavFilePath, FileMode.Create, FileAccess.Write, FileShare.Write);
            reversedFileStream.Write(reversedWavFileStreamByteArray, 0, reversedWavFileStreamByteArray.Length);
        }
    }
}