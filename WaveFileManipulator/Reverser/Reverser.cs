namespace WaveFileManipulator
{
    internal class Reverser : IReverser
    {        
        public byte[] Reverse(Metadata metadata, byte[] forwardsArrayWithOnlyHeaders, byte[] forwardsArrayWithOnlyAudioData)
        {            
            const int BitsPerByte = 8;
            int bytesPerSample = metadata.BitsPerSample.Value / BitsPerByte;
            byte[] reversedArrayWithOnlyAudioData = SamplesManipulator.Reverse(bytesPerSample, forwardsArrayWithOnlyAudioData);
            byte[] reversedWavFileStreamByteArray = forwardsArrayWithOnlyHeaders.Combine(reversedArrayWithOnlyAudioData);

            return reversedWavFileStreamByteArray;
        }
    }
}