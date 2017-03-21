
namespace Spike.Providers.Common
{
    public static class SampleWorker
    {
        public static bool IsSample(int position)
        {
            var sample = (position > 10000)
                ? 10000
                : (position > 1000)
                    ? 1000
                    : (position > 100)
                        ? 100
                        : (position > 10) ? 10 : 1;

            return position % sample == 0;
        }
    }
}
