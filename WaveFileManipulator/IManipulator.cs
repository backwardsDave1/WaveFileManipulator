namespace WaveFileManipulator
{
    public interface IManipulator
    {
        byte[] Reverse();
        byte[] IncreaseVolume(double factor);
    }
}